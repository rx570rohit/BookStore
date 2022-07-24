using BussinessLayer.Interfaces;
using DatabaseLayer.Cart;
using DatabaseLayer.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBl wishListBl;


        public WishListController(IWishListBl wish)
        {
            this.wishListBl = wish;

        }
        [HttpPost("AddToWishList")]
       

        public async Task<IActionResult> AddToWishList(WishListPostModel wishList)
        {
            try
            {

                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var resp = await this.wishListBl.AddToWishList(userId,wishList);

                if (resp != null)
                {

                    return this.Ok(new  { Status = true, Message = " Add to Cart Successfully!!!!!!", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Record Not Found" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [HttpDelete("RemoveWishList")]
        public async Task<IActionResult> RemoveWishList(string wishListID)
        {
            try
            {

                bool resp = await this.wishListBl.RemoveWishList(wishListID);
                if (resp != false)
                {

                    return this.Ok(new  { Status = true, Message = " Remove the cart!!!!!!", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Not Found any Data!!!!!!" });
                }
            }
            catch (Exception e)
            {
                {
                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }

        }

        [HttpGet("GetAllWishLists")]
        public IEnumerable<WishList> GetAllWishLists()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var resp = this.wishListBl.GetAllWishLists(userId);
                return resp;
            }

            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
