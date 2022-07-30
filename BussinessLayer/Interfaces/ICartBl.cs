using DatabaseLayer.Cart;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface ICartBl
    {
        Task<Carts> AddToCart(string UserId, CartPostModel cart);
        Task<bool> RemoveCart(string cartId);
        Task<bool> UpdateCartQuantity(string userId,string bookId, string cartId, int quantity);
        IEnumerable<Carts> GetAllCart(string userid);
    }
}
