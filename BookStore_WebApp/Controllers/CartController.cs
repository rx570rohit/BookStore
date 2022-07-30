using BussinessLayer.Interfaces;
using DatabaseLayer.Cart;
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
    public class CartController : ControllerBase
    {
        private readonly ICartBl cartbl;


        public CartController(ICartBl cart)
        {
            this.cartbl = cart;
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(CartPostModel cart)
        {
            try
            {

                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;
                

                var resp = await this.cartbl.AddToCart(userId,cart);

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
        [HttpDelete("RemoveCart")]
        public async Task<IActionResult> RemoveCart(String cartId )
        {
            try
            {

                bool resp = await this.cartbl.RemoveCart(cartId);
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

        [HttpPut("UpdateCartQuantity")]
        public async Task<IActionResult> UpdateCartQuantity( string bookId,string cartId,int quantity)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;
               

                var resp = await this.cartbl.UpdateCartQuantity(userId,bookId,cartId,quantity);

                if (resp != false)
                {
                    
                    return this.Ok(new  { Status = true, Message = " Data iIs UpDate ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Record not Found or Quantity you entered is greater then available quantity" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [HttpGet("GetAllCart")]
        public IEnumerable<Carts> GetAllCart()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var resp = this.cartbl.GetAllCart(userId);
                return resp;
            }

            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
