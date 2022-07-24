using DataBaseLayer.Users;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface IAdminBL
    {
        Task<Admin> AddAdmin(userPostModel userPostModel);

        public string LogInUser(string Email, string Password);
        bool ForgotPassword(string email);
        bool ResetPassword(string email, UserPasswordModel userPasswordModel);
    }
}
