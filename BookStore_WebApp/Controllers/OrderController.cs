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
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Experimental.System.Messaging;
using RepositoryLayer.Services;

namespace BookStore_WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBl orderbl;
        private readonly IAddressBl addressBl;
        private readonly ICartBl cartbl;
        IbookstoreContext context;
        public OrderController(IAddressBl addressBl,IbookstoreContext context, IOrderBl orderbl, ICartBl cartbl)
        {
            this.orderbl = orderbl;
            this.addressBl = addressBl;
            this.context = context;
            this.cartbl = cartbl;
        }
        [HttpPost("AddToOrder")]
       

        public async Task<IActionResult> AddToOrder(OrderPostModel order)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var userEmail =context.mongoUserCollections.AsQueryable().Where(x=>x.UserId==userId).FirstOrDefault();
                
                var email = userEmail.EmailId;


                //CartPostModel cart = new CartPostModel()
                //{
                //    BookId = order.bookId,  
                //    quantity=order.Quantity
                //};

                //if (book == null)
                //{
                //    await this.cartbl.AddToCart(userId, cart);

                //}
                //var cartid = context.mongoCartCollections.AsQueryable().Where(x => x.BookId == order.bookId &&x.userId==userId).FirstOrDefault().cartId;
                //OrderPostModel orderPost = new OrderPostModel()
                //{
                //    CartId= cartid,
                //    bookId=order.bookId,
                //    addressID=order.addressID,
                //    Price=order.Price,
                //    Quantity=order.Quantity,
                //};



                var book = await context.mongoBookCollections.AsQueryable().Where(x => x.BookId == order.bookId).FirstOrDefaultAsync();

                if (book.BookQuantity >= order.Quantity)
                {
                    var resp = await this.orderbl.AddToOrder(userId, order);


                    if (resp != null)
                    {

                        MessageQueue queue;

                        if (MessageQueue.Exists(@".\Private$\BookStore"))
                        {
                            queue = new MessageQueue(@".\Private$\BookStore");
                        }
                        else
                        {
                            queue = MessageQueue.Create(@".\Private$\BookStore");
                        }

                        Message MyMessage = new Message();
                        MyMessage.Formatter = new BinaryMessageFormatter();
                        MyMessage.Body ="\n OrderId "+ resp.orderID +
                        " \n  BookName : "+ resp.books.BookName +
                        "\n"+"Order Address : "+resp.Address.fullAddress +"\n City :"+resp.Address.city+"\n Pincode :"+ resp.Address.pinCode+
                        "\n Total Cost " +resp.Price;
                        MyMessage.Label = "Your Order Placed Order ID as ";
                        queue.Send(MyMessage);

                        Message msg = queue.Receive();
                        msg.Formatter = new BinaryMessageFormatter();
                        EmailServices2.SendEmail(email, msg.Body.ToString());
                        queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                        queue.BeginReceive();
                        queue.Close();
                       
                        //  await this.cartbl.RemoveCart(order.CartId);
                        return this.Ok(new { Status = true, Message = " Order Placed Successfully!!!!!!", Data = resp });

                    }
                    else
                    {

                        return this.BadRequest(new { Status = false, Message = "Order not Placed  " });
                    }
                }
                return this.BadRequest(new { Status = false, Message = " the quantity you required is not available  " });

            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices2.SendEmail(e.Message.ToString(), null);
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
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
        public async Task<IActionResult> UpdateOrderAddress( /*/ string bookName ,*/ string orderId ,string addressId, AddressPostModel addressPostModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var usrAdd =  context.mongoAddressCollections.AsQueryable().Where(x => x.userId == userId).FirstOrDefault();
                if (usrAdd != null)
                {
                    var addresses = await addressBl.UpdateAddress(userId,addressId,addressPostModel);

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
