using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.BL.Business;
using Sat.Recruitment.BL.DTO;
using Sat.Recruitment.BL.Exceptions;
using Sat.Recruitment.DAL.Exceptions;
using Sat.Recruitment.DAL.Interfaces;
using Sat.Recruitment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserServiceUnitTest
    {
        [Fact]
        public void NewUserShouldCreate()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(service => service.CreateUser(It.IsAny<User>()));

            var userService = new UserService(userRepositoryMock.Object);

            UserDTO user = new UserDTO()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Money = 124,
                UserType = "Normal"
            };


            var exception = Record.Exception(() => userService.CreateUser(user)); 


            Assert.Null(exception);
        }

        [Fact]
        public void DuplicateUserShouldFail()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository =>
                repository.CreateUser(It.IsAny<User>())

                ).Throws(() => new DALException("User is duplicated"));

            var userService = new UserService(userRepositoryMock.Object);

            UserDTO user = new UserDTO()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Money = 124,
                UserType = "Normal"
            };


            var exception = Assert.Throws<BusinessException>(() => userService.CreateUser(user));


            Assert.Equal("User is duplicated", exception.Errors);
        }
    }
}
