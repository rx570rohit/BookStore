using DatabaseLayer.Order;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface IOrderBl
    {
        Task<Orders> AddToOrder(string userId, OrderPostModel order);

        Task<IEnumerable<Orders>> GetAllOrders(string userid);

        Task<bool> CancelOrder(string UserId, string orderId);
        Task<Orders> UpdateOrderAddress(string userId, string orderId, Addresses address);


    }
}
