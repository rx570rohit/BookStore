using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface ICartBl
    {
        Task<string> AddToCart(Carts cart);
        Task<bool> RemoveCart(Carts cart);
        Task<string> UpdateCartQuantity(Carts cart);
        object GetAllCart();
    }
}
