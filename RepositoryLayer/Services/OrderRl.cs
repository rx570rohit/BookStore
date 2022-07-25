using DatabaseLayer.Order;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class OrderRl : IOrderRl
    {
        IbookstoreContext context;    
        public OrderRl(IbookstoreContext context)
        {
            this.context = context; 
        }
        public async Task<Orders> AddToOrder(string userId, OrderPostModel order)
        {
            var userAddress = await context.mongoAddressCollections.AsQueryable().Where(x => x.addressID== order.addressID).SingleOrDefaultAsync();
            
            var userCart = await context.mongoCartCollections.AsQueryable().Where(x => x.cartId == order.CartId).SingleOrDefaultAsync();
            
            if (userAddress != null && userCart != null)
            {
                Orders orders = new Orders()
                {
                    userID = userId,    
                 
                    Cart = userCart,
                    Address = userAddress,
                    Price = order.Price,
                    Quantity = order.Quantity,
                    
                };
                await context.mongoOrdersCollections.InsertOneAsync(orders);
                return orders;  
            }
            return null;
        }

        public  async Task<bool> CancelOrder(string userId, string orderId)
        {

            var cartInfo = await context.mongoOrdersCollections.FindOneAndDeleteAsync(x => x.orderID == orderId && x.userID==userId);
            if (cartInfo != null)
            {
                return true;
            }
            return false;
        }

        public  async Task<IEnumerable<Orders>> GetAllOrders(string userid)
        {
            var orders = await context.mongoOrdersCollections.AsQueryable().Where(x => x.userID == userid).ToListAsync();
            if (orders != null)
            {
                return orders;  
            }
            return null;
        }

        public   Task<Orders> UpdateOrderAddress(string userId,string orderId, Addresses address)
        {
            var res = context.mongoOrdersCollections.FindOneAndUpdateAsync(x => x.userID == userId && x.orderID == orderId,
            Builders<Orders>.Update.Set(x => x.Address, address)
            );

            return res;

        }
    }
}
