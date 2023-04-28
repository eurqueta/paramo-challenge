using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.BL.Business.Interfaces;
using Sat.Recruitment.BL.DTO;
using Sat.Recruitment.BL.Exceptions;
using Sat.Recruitment.DAL.Exceptions;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerUnitTest
    {
        [Fact]
        public void NewUserShouldCreate()
        {
            var userServiceMock = new Mock<IUserService>(); 
            userServiceMock.Setup(service => service.CreateUser(It.IsAny<UserDTO>()));

            var userController = new UsersController(userServiceMock.Object);

            var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void UserDuplicatedShouldFail()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.CreateUser(It.IsAny<UserDTO>()))
                .Throws(new BusinessException("") { Errors = "The user is duplicated" });


            var userController = new UsersController(userServiceMock.Object);

            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public void InternalErrorShouldHideMessage()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.CreateUser(It.IsAny<UserDTO>()))
                .Throws(new Exception("Error on database") );


            var userController = new UsersController(userServiceMock.Object);

            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("Internal Error", result.Errors);
        }

        [Fact]
        public void EmptyNameShouldFail()
        {
            var userServiceMock = new Mock<IUserService>();


            var userController = new UsersController(userServiceMock.Object);

            var result = userController.CreateUser(null, "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The name is required", result.Errors);
        }
    }
}
