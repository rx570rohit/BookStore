using BussinessLayer.Interfaces;
using DatabaseLayer.Order;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class OrderBl :IOrderBl
    {
        IOrderRl orderRl;
        public OrderBl(IOrderRl orderRl)
        {
            this.orderRl = orderRl;
        }
        public Task<Orders> AddToOrder(string UserId, OrderPostModel order)
        {
            return orderRl.AddToOrder(UserId, order);
        }

        public Task<IEnumerable<Orders>> GetAllOrders(string userid)
        {
            return orderRl.GetAllOrders(userid);
        }

        public Task<bool> CancelOrder(string UserId, string orderId)
        {
            return orderRl.CancelOrder(UserId,orderId);
        }

        public Task<Orders> UpdateOrderAddress(string userId, string orderId, Addresses address)
        {
            return orderRl.UpdateOrderAddress(userId,orderId,address);
        }
    }
}
