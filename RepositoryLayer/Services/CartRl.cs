using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CartRl : ICartRl
    {
        Task<string> ICartRl.AddToCart(Carts cart)
        {
            return null;
        }

        object ICartRl.GetAllCart()
        {
            return null;
        }

        Task<bool> ICartRl.RemoveCart(Carts cart)
        {
            return null;
        }

        Task<string> ICartRl.UpdateCartQuantity(Carts cart)
        {
            return null;
        }
    }
}
