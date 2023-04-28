using Moq;
using Sat.Recruitment.BL.Business;
using Sat.Recruitment.BL.DTO;
using Sat.Recruitment.BL.Exceptions;
using Sat.Recruitment.DAL;
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
    public class UserRepositoryUnitTest
    {
        [Fact]
        public void NewUserShouldCreate()
        {

            var userRepository = new UserRepository();

            User user = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Money = 124,
                UserType = "Normal"
            };


            var exception = Record.Exception(() => userRepository.CreateUser(user));


            Assert.Null(exception);
        }


        [Fact]
        public void DuplicateUserShouldFail()
        {
            var userRepository = new UserRepository();

            User user = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Money = 124,
                UserType = "Normal"
            };


            var exception = Assert.Throws<DALException>(() => userRepository.CreateUser(user));


            Assert.Equal("User is duplicated", exception.Message);
        }
    }
}
