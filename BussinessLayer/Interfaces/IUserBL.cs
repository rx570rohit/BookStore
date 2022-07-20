using DataBaseLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface IUserBL
    {
        Task<userPostModel> AddUser(userPostModel userPostModel);

        public string LogInUser(string Email, string Password);

    }
}
