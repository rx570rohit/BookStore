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
         Task<bool> RemoveCart( Carts cart);
         IEnumerable<Carts> GetAllCart(string userid);

         Task<Carts> UpdateCartQuantity(string userId, string bookName, string authorName, int quantity);
    }
}
