using Sat.Recruitment.BL.Business.Interfaces;
using Sat.Recruitment.BL.DTO;
using Sat.Recruitment.BL.Exceptions;
using Sat.Recruitment.DAL.Exceptions;
using Sat.Recruitment.DAL.Interfaces;
using Sat.Recruitment.DAL.Models;
using System;

namespace Sat.Recruitment.BL.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;


        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void CreateUser(UserDTO newUser)
        {

            var money = newUser.Money;

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

            try
            {
                userRepository.CreateUser(user);

            }
            catch (DALException ex)
            {
                throw new BusinessException("Error in create User", ex) {
                    Errors = ex.Message
                };
            }


        }
    }
}