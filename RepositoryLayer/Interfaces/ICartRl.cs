using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRl
    {
        Task<string> AddToCart(Carts cart);
        Task<bool> RemoveCart( Carts cart);
        object GetAllCart();
        Task<string> UpdateCartQuantity( Carts cart);
    }
}
