using BussinessLayer.Interfaces;
using DataBaseLayer.Users;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
    
        public async Task<Admin> AddAdmin(userPostModel userLoginModel)
        {
           return await this.adminRL.AddAdmin(userLoginModel);
        }

        public bool ForgotPassword(string email)
        {
            return this.adminRL.ForgotPassword(email);
        }

        public string LogInUser(string Email, string Password)
        {
            return this.adminRL.LogInUser(Email,Password);
        }

        public bool ResetPassword(string email, UserPasswordModel userPasswordModel)
        {
            return this.adminRL.ResetPassword( email, userPasswordModel);

        }
    }
}
