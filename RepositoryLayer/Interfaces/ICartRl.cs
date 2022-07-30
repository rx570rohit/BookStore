using DatabaseLayer.Cart;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRl
    {
         Task<Carts> AddToCart(string UserId, CartPostModel cart);
         Task<bool> RemoveCart(string cartId);
         IEnumerable<Carts> GetAllCart(string userid);

         Task<bool> UpdateCartQuantity(string userId,string bookId, string cartId, int quantity);
    }
}
