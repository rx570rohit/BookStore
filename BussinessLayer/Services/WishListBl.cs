using BussinessLayer.Interfaces;
using DatabaseLayer.Cart;
using DatabaseLayer.WishList;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public class WishListBl : IWishListBl
    {
        IWishListRl wlRl;
        public WishListBl(IWishListRl wishList)
        {
            this.wlRl = wishList;
        }
        public Task<WishList> AddToWishList(string UserId, WishListPostModel wishListPostModel)
        {
            return wlRl.AddToWishList(UserId,wishListPostModel);
        }

        public IEnumerable<WishList> GetAllWishLists(string userid)
        {
            return wlRl.GetAllWishLists(userid);
        }

        public Task<bool> RemoveWishList(string wishListID)
        {
            return wlRl.RemoveWishList(wishListID);
        }

    }
}
