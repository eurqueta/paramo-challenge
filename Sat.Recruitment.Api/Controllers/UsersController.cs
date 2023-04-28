using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.BL.Business.Interfaces;
using Sat.Recruitment.BL.DTO;
using Sat.Recruitment.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Sat.Recruitment.DAL.UserRepository;

namespace Sat.Recruitment.Api.Controllers
{


    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = "";

            ValidateErrors(name, email, address, phone, money, ref errors);

            if (errors != null && errors != "")
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };

            var newUser = new UserDTO
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };

            try
            {
                userService.CreateUser(newUser);

                return new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }  catch (BusinessException ex) 
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                };
            } catch (Exception)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "Internal Error"
                };
            }
          


            
        }

        //Validate errors
        private void ValidateErrors(string name, string email, string address, string phone, string money, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (!IsValidEmail(email))
                //Validate if Email is not valid
                errors = errors + " The email is not valid";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
            if (money == null)
                //Validate if money is null
                errors = errors + " The money is required";
            if (!IsValidDecimal(money))
                //Validate if money is decimal
                errors = errors + " The money is invalid";
        }

        private bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private bool IsValidDecimal(string value)
        {
            try
            {
                decimal.Parse(value);
                return true;
            }catch(FormatException)
            {
                return false;
            }
        }
    }

}
