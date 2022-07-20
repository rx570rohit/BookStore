using BussinessLayer.Interfaces;
using DataBaseLayer.Users;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class UserBL : IUserBL
    {
        readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
    
        public async Task<userPostModel> AddUser(userPostModel userLoginModel)
        {
           return await this.userRL.AddUser(userLoginModel);
        }
    }
}
