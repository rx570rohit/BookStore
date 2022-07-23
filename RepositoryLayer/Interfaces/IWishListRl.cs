using DatabaseLayer.Cart;
using DatabaseLayer.WishList;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRl
    {
         Task<WishList> AddToWishList(string UserId, WishListPostModel cart);
         Task<bool> RemoveWishList( WishList cart);
         IEnumerable<WishList> GetAllWishLists(string userid);

    }
}
