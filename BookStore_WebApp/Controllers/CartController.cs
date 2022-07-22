using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore_WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartBl cartbl;


        public CartController(ICartBl cart)
        {
            this.cartbl = cart;

        }
        [HttpPost]
        [Route("addtocart")]

        public async Task<IActionResult> AddToCart( Carts cart)
        {
            try
            {

                var resp = await this.cartbl.AddToCart(cart);
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
        [HttpDelete]
        [Route("removeFromCart")]
        public async Task<IActionResult> RemoveCart(Carts cart)
        {
            try
            {

                bool resp = await this.cartbl.RemoveCart(cart);
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
        [HttpPut]
        [Route("updateCart")]
        public async Task<IActionResult> UpdateCartQuantity([FromBody] Carts cart)
        {
            try
            {

                var resp = await this.cartbl.UpdateCartQuantity(cart);
                if (resp != null)
                {

                    return this.Ok(new  { Status = true, Message = " Data Is UpDate ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Record not Found" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [HttpGet]
        [Route("getallCart")]
        public IEnumerable<Carts> GetAllCart()
        {
            try
            {
                var resp = this.cartbl.GetAllCart();
                return (IEnumerable<Carts>)resp;
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}
