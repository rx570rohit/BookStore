using BussinessLayer.Interfaces;
using DataBaseLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entity;
using System;
using System.Threading.Tasks;

namespace BookStore_WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        IbookstoreContext context;

        IDBSetting db;
        IConfiguration configuration;

        IUserBL userBL;

        public UserController(IUserBL userBL, IDBSetting db, IConfiguration configuration, IbookstoreContext context)
        {
            this.userBL = userBL;
            this.db = db;
            this.configuration = configuration;

            this.context = context;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register(userPostModel register)
        {
            try
            {

                var res = await this.userBL.AddUser(register);
                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull" });
                }
                else { return this.BadRequest(new { success = false, message = "Email Already Exits" }); }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("LogIn/email/password")]

        public IActionResult LogIn(String Email, String Password)
        {
            try
            {
                //s
                var user = context.mongoUserCollections.AsQueryable().Where(u => u.EmailId == Email).FirstOrDefault();


                if (user == null)
                {
                    return this.BadRequest(new { success = false, message = "Email doesn't Exits" });
                }

                string password = PwdEncryptDecryptService.EncryptPassword(Password);

                var userdata1 = context.mongoUserCollections.Find<User>(u => u.EmailId == Email && u.Password == password).FirstOrDefault();
                if (userdata1 == null)
                {
                    return this.BadRequest(new { success = false, message = "Password is Invalid" });
                }

                string token = this.userBL.LogInUser(Email, Password);

                return this.Ok(new { success = true, message = "LogIn Successfull", data = token });
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        [HttpPost("Forgotpassword/{Email}")]
        public IActionResult ForgotPassword(String Email)
        {
            var user = fundooContext.Users.FirstOrDefault(u => u.Email == Email);
            if (user == null)
            {
                return this.BadRequest(new { success = false, message = "Email Not Found" });
            }
            string Password = PwdEncryptDecryptService.DecryptPassword(user.Password);

            var userdata1 = fundooContext.Users.FirstOrDefault(u => u.Email == Email);
            if (userdata1 == null)
            {
                return this.BadRequest(new { success = false, message = "Password " });
            }
            bool result = this.userBL.ForgotPassword(Email);
            return this.Ok(new { success = true, message = "Tokne sented successfully to respective email Id ", data = result });
        }

        [Authorize]
        [HttpPut("Resetpassword")]
        public IActionResult Resetpassword(UserPasswordModel userPasswordModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var result = fundooContext.Users.Where(u => u.UserId == UserID).FirstOrDefault();
                string Email = result.Email.ToString();
                if (userPasswordModel.Password != userPasswordModel.ConfirmPassword)
                {
                    return BadRequest(new { success = false, message = "Password and Confirm password must be same" });
                }
                bool res = this.userBL.ResetPassword(Email, userPasswordModel);
                if (res == false)
                {
                    return this.BadRequest(new { sucess = false, message = "Enter the valid Email" });
                }
                return this.Ok(new { succes = true, message = "Password change successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
