using BussinessLayer.Interfaces;
using DataBaseLayer.Users;
using Microsoft.AspNetCore.Mvc;
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

        IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;

        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register( userPostModel register)
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
    }
}
