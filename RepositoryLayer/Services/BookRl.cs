using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Book;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace RepositoryLayer.Services
{
    public class BookRl : IBookRl
    {
        IbookstoreContext context;
        IWebHostEnvironment env;

        public BookRl( IbookstoreContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env= env;
        }
        
        public async Task<Books> AddBook(BookPostModel book)
        {
            try
            {
              //  byte[] binaryContent = File.ReadAllBytes("image.jpg");

                var ifExists = await context.mongoBookCollections.FindAsync(x => x.AuthorName == book.AuthorName && x.BookName == book.BookName);
                if (ifExists != null)
                {
                    Books Books = new Books()
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
                    await context.mongoBookCollections.InsertOneAsync(Books);
                    return Books;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteBook(string bookId)
        {
            try
            {
                //var v = await context.mongoBookCollections.AsQueryable().Where(x => x.BookId==bookId).FirstOrDefaultAsync();
                //string bookid = v.BookId;
               

                var ifExists = await context.mongoBookCollections.FindOneAndDeleteAsync(x => x.BookId == bookId);
                if(ifExists!=null)
                return true;

                 return false;


            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }

       
            public async Task<Books> UpdateBook(string bookId, BookPostModel book)
            {
                try
                {
                var bookInfo = await context.mongoBookCollections.AsQueryable().Where(x => x.BookId==bookId).SingleOrDefaultAsync();
                var BookID = bookInfo.BookId;


                    if (BookID != null)
                    {
                        await context.mongoBookCollections.UpdateOneAsync(x => x.BookId == BookID,
                            Builders<Books>.Update.Set(x => x.BookName, book.BookName)
                            .Set(x => x.Description, book.Description)
                            .Set(x => x.AuthorName, book.AuthorName)
                            .Set(x=>x.ActualPrice, book.ActualPrice)    
                            .Set(x => x.DiscountPrice, book.DiscountPrice)
                            .Set(x => x.BookQuantity, book.BookQuantity)
                            .Set(x => x.Rating, book.Rating)
                            .Set(x => x.totalRating, book.totalRating)
                            .Set(x => x.DiscountPrice, book.DiscountPrice));
                        return bookInfo;
                    }
                    //else
                    //{
                    //Books Books = new Books()
                    //{
                    //    AuthorName = book.AuthorName,
                    //    BookName = book.BookName,
                    //    BookImage = book.BookImage,
                    //    BookQuantity = book.BookQuantity,
                    //    ActualPrice = book.ActualPrice,
                    //    Description = book.Description,
                    //    DiscountPrice = book.DiscountPrice,
                    //    Rating = book.Rating,
                    //    totalRating = book.totalRating
                    //};
                    //await context.mongoBookCollections.InsertOneAsync(Books);
                    return null;

                    
                }
                catch (ArgumentNullException e)
                {

                    throw new Exception(e.Message);
                }
            }



        public async Task<IEnumerable<Books>> GetAllBooks()
        {
           var v = context.mongoBookCollections.Find(FilterDefinition<Books>.Empty);
            return await v.ToListAsync();

        }

       
    }
}
