using DatabaseLayer.Cart;
using DatabaseLayer.WishList;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class WishListRl : IWishListRl
    {
        IbookstoreContext context;

        private readonly IConfiguration Configuration;
        public WishListRl(IbookstoreContext context, IConfiguration Configuration)
        {
                this.context = context; 
                this.Configuration= Configuration;  
        }
        public async Task<WishList> AddToWishList(string UserId , WishListPostModel cart)
        {
            try
            {
                //    var excart = context.mongoCartCollections.AsQueryable().Where(x => x.BookId == cart.BookId &&x.userId==UserId).FirstOrDefaultAsync();
                //    var userid = excart.Result.userId;
                //    var bookname = excart.Result.book.BookName;
                //    var authorName = excart.Result.book.AuthorName;

                //    if (excart != null)
                //    {
                //       await UpdateCartQuantity(userid,bookname,authorName,cart.quantity);
                //    }
                //    else
                //book = (Books)context.mongoBookCollections.Find(x => x.BookId == cart.BookId),
                //                user = (Users)context.mongoUserCollections.Find(x => x.UserId == UserId),


                var book = await context.mongoBookCollections.AsQueryable().Where(x=>x.BookId==cart.BookId).SingleOrDefaultAsync();

                var user = await context.mongoUserCollections.AsQueryable().Where(x => x.UserId == UserId).SingleOrDefaultAsync();

                Users userdetail = new Users()
                {
                    FisrtName = user.FisrtName,
                    LastName = user.LastName,
                    EmailId = user.EmailId,
                    Password = null,
                    CreatedDate = user.CreatedDate,
                };

                Books bookdetail = new Books()
                {
                    AuthorName = book.AuthorName,
                    BookName = book.BookName,
                    BookImage = book.BookImage,
                    ActualPrice = book.ActualPrice,
                    Description = book.Description,
                    DiscountPrice = book.DiscountPrice,
                    Rating = book.Rating,
                    totalRating = book.totalRating
                };

                WishList wishList = new WishList()
                {
                    BookId = cart.BookId,
                    userId = UserId,
                    Book  = bookdetail,
                     user = userdetail,
                };

                    await context.mongoWishListCollections.InsertOneAsync(wishList);
                
                return wishList;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<WishList> GetAllWishLists(string userid)
        {
            var v = context.mongoWishListCollections.AsQueryable().Where(x=>x.userId == userid);
            if(v != null)
            {
                return v.ToList();
            }
            return null;
        }

        public async Task<bool> RemoveWishList(WishList wishList)
        {
            var cartInfo = await context.mongoWishListCollections.FindOneAndDeleteAsync(x => x.wishListID == wishList.wishListID);
            if (cartInfo != null)
            {
                return true;
            } 
            return false;
        }
    }
}
