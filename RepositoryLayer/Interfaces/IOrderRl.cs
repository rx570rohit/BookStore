using DatabaseLayer.Order;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRl
    {
        Task<Orders> AddToOrder(string userId, OrderPostModel order);
        Task<IEnumerable<Orders>> GetAllOrders(string userid);
        Task<bool> CancelOrder(string userId, string orderId);
        Task<Orders> UpdateOrderAddress(string userId, string orderId, Addresses address);
    }
}
