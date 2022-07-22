using BussinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class CartBl : ICartBl
    {
        ICartRl cartrl;
        public CartBl(ICartRl cartrl)
        {
            this.cartrl = cartrl;
        }
        Task<string> ICartBl.AddToCart(Carts cart)
        {
            return cartrl.AddToCart(cart);
        }

        object ICartBl.GetAllCart()
        {
            return cartrl.GetAllCart();
        }

        Task<bool> ICartBl.RemoveCart(Carts cart)
        {
            return cartrl.RemoveCart( cart);
        }

        Task<string> ICartBl.UpdateCartQuantity(Carts cart)
        {
            return cartrl.UpdateCartQuantity(cart);
        }
    }
}
