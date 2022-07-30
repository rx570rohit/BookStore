using BussinessLayer.Interfaces;
using DatabaseLayer.Cart;
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
        public Task<Carts> AddToCart(string UserId, CartPostModel cart)
        {
            return cartrl.AddToCart(UserId,cart);
        }

        public IEnumerable<Carts> GetAllCart(string userid)
        {
            return cartrl.GetAllCart(userid);
        }

        public Task<bool> RemoveCart(String cartId)
        {
            return cartrl.RemoveCart( cartId);
        }

        public Task<bool> UpdateCartQuantity(string userId, string bookId,string cartId, int quantity)
        {
            return cartrl.UpdateCartQuantity(userId, bookId,cartId, quantity);
        }
    }
}
