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
        Task<Carts> UpdateCartQuantity(string userId, string bookName, string authorName, int quantity);
        IEnumerable<Carts> GetAllCart(string userid);
    }
}
