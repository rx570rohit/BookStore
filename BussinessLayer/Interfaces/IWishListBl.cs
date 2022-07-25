using DatabaseLayer.Cart;
using DatabaseLayer.WishList;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface IWishListBl
    {

        Task<WishList> AddToWishList(string UserId, WishListPostModel cart);
        Task<bool> RemoveWishList(string wishListID);
        IEnumerable<WishList> GetAllWishLists(string userid);
    }
}
