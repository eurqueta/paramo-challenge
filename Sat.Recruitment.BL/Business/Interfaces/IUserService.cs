using Sat.Recruitment.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using static Sat.Recruitment.BL.UserService;

namespace Sat.Recruitment.BL.Business.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO newUser);
    }
}
