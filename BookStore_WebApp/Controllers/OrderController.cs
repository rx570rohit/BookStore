using BussinessLayer.Interfaces;
using BussinessLayer.Services;
using DatabaseLayer.Address;
using DatabaseLayer.Cart;
using DatabaseLayer.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RepositoryLayer.Interfaces;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderBl orderbl;
        private readonly IAddressBl addressBl;

        IbookstoreContext context;
        public OrderController(IAddressBl addressBl,IbookstoreContext context ,IOrderBl orderbl)
        {
            this.orderbl = orderbl;
            this.addressBl = addressBl; 
            this.context = context;
        }
        [HttpPost("AddToOrder")]
       

        public async Task<IActionResult> AddToOrder(OrderPostModel order)
        {
            try
            {

                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var resp = await this.orderbl.AddToOrder(userId,order);

                if (resp != null)
                {

                    return this.Ok(new  { Status = true, Message = " Order Placed Successfully!!!!!!", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Order not Placed  " });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [HttpDelete("CancelOrder")]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;


                bool resp = await this.orderbl.CancelOrder(userId,orderId);
                if (resp != false)
                {

                    return this.Ok(new  { Status = true, Message = " your Order is Canceled Successfully!!!!!!", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "your Order is Canceled Successfully!!!!!!" });
                }
            }
            catch (Exception e)
            {
                {
                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }

        }

        [HttpPut("UpdateOrderAddress")]
        public async Task<IActionResult> UpdateOrderAddress( string bookName ,string orderId , AddressPostModel addressPostModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var usrAdd =  context.mongoAddressCollections.AsQueryable().Where(x => x.userId == userId).FirstOrDefault();
                if (usrAdd != null)
                {
                    var addresses = await addressBl.UpdateAddress(userId, addressPostModel);

                    var resp = await this.orderbl.UpdateOrderAddress(userId, orderId, addresses);
                    if (resp != null)
                    {

                        return this.Ok(new { Status = true, Message = " Address Is UpDate ", Data = resp });
                    }
                    else
                    {

                        return this.BadRequest(new { Status = false, Message = "address not updated " });
                    }
                }
                return this.BadRequest(new { Status = false, Message = "No Record Found" });

            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [HttpGet("GetAllOrders")]
        public Task<IEnumerable<Orders>> GetAllOrders()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var resp = this.orderbl.GetAllOrders(userId);
                return resp;
            }

            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
