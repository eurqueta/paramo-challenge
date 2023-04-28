using Sat.Recruitment.BL.Business.Interfaces;
using Sat.Recruitment.BL.DTO;
using Sat.Recruitment.DAL;
using Sat.Recruitment.DAL.Interfaces;
using Sat.Recruitment.DAL.Models;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sat.Recruitment.BL
{
    public class UserService: IUserService
    {
        private readonly IUserRepository userRepository;

        public class Result
        {
            public bool IsSuccess { get; set; }
            public string Errors { get; set; }
        }

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Result CreateUser(UserDTO newUser,decimal money)
        {



            if (newUser.UserType == "Normal")
            {
                if (money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = money * percentage;
                    newUser.Money = newUser.Money + gif;
                }
                if (money < 100)
                {
                    if (money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                }
            }
            if (newUser.UserType == "SuperUser")
            {
                if (money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = money * percentage;
                    newUser.Money = newUser.Money + gif;
                }
            }
            if (newUser.UserType == "Premium")
            {
                if (money > 100)
                {
                    var gif = money * 2;
                    newUser.Money = newUser.Money + gif;
                }
            }

            User user = new User()
            {
                Address = newUser.Address,
                Email = newUser.Email,
                Money = newUser.Money,
                Name = newUser.Name,
                Phone = newUser.Phone,
                UserType = newUser.UserType,
            };

            var result = userRepository.CreateUser(user);

            BL.UserService.Result resultBL = new BL.UserService.Result()
            {
                Errors= result.Errors,
                IsSuccess  = result.IsSuccess,
            };

            return resultBL;
          

        }
    }
}