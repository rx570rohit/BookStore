using DatabaseLayer.Cart;
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
    public class CartRl : ICartRl
    {
        IbookstoreContext context;

        private readonly IConfiguration Configuration;
        public CartRl(IbookstoreContext context, IConfiguration Configuration)
        {
                this.context = context; 
                this.Configuration= Configuration;  
        }
        public async Task<Carts> AddToCart(string UserId ,CartPostModel cart)
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
                    BookQuantity = book.BookQuantity,
                    ActualPrice = book.ActualPrice,
                    Description = book.Description,
                    DiscountPrice = book.DiscountPrice,
                    Rating = book.Rating,
                    totalRating = book.totalRating
                };

                Carts carts = new Carts()
                {
                    BookId = cart.BookId,
                    userId = UserId,
                    book = bookdetail,
                     user = userdetail,
                    Quantity = cart.quantity,
                };

                    await context.mongoCartCollections.InsertOneAsync(carts);
                
                return carts;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Carts> GetAllCart(string userid)
        {
            var v = context.mongoCartCollections.AsQueryable().Where(x=>x.userId == userid);
            if(v != null)
            {
                return v.ToList();
            }
            return null;
        }

        public async Task<bool> RemoveCart(string cartId)
        {
            var cartInfo = await context.mongoCartCollections.FindOneAndDeleteAsync(x => x.cartId == cartId);
            if (cartInfo != null)
            {
                return true;
            } 
            return false;
        }

        public async Task<bool> UpdateCartQuantity(string userId,string bookId, string cartId, int quantity)
        {
            try
            {
                //var cartInfo = await context.mongoCartCollections.AsQueryable().Where( x=>x.book.BookName==bookName && x.book.AuthorName==authorName && x.userId==userId).FirstOrDefaultAsync();
                //var cartID = cartInfo.cartId;
                var book = await context.mongoBookCollections.AsQueryable().Where(x => x.BookId == bookId).FirstOrDefaultAsync();
                int bookQuantity = book.BookQuantity;
                if (quantity <= bookQuantity)
                {
                    var res = await context.mongoCartCollections.UpdateOneAsync(x => x.cartId == cartId && x.BookId == bookId,
                          Builders<Carts>.Update.Set(x => x.Quantity, quantity));
                    if (res != null)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
