using Sat.Recruitment.DAL.Models;

namespace Sat.Recruitment.DAL.Interfaces
{
    public interface IUserRepository
    {
        public void CreateUser(User newUser);
    }
}
