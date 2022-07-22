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

        public async Task<Book> AddBook(BookPostModel book)
        {
            try
            {
              //  byte[] binaryContent = File.ReadAllBytes("image.jpg");

                var ifExists = await context.mongoBookollections.FindAsync(x => x.AuthorName == book.AuthorName && x.BookName == book.BookName);
                if (ifExists != null)
                {
                    Book Books = new Book()
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
                    await context.mongoBookollections.InsertOneAsync(Books);
                    return Books;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteBook(string bookName, string authorName)
        {
            try
            {
                var v = await context.mongoBookollections.AsQueryable().Where(x => x.AuthorName == authorName && x.BookName == bookName).FirstOrDefaultAsync();
                string bookid = v.BookId;


                var ifExists = await context.mongoBookollections.FindOneAndDeleteAsync(x => x.BookId == bookid);

                return true;


            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }

       
            public async Task<Book> UpdateBook(BookPostModel book)
            {
                try
                {
                var bookInfo = await context.mongoBookollections.AsQueryable().Where(x => x.AuthorName == book.AuthorName && x.BookName == book.BookName).SingleOrDefaultAsync();
                var BookID = bookInfo.BookId;


                    if (BookID != null)
                    {
                        await context.mongoBookollections.UpdateOneAsync(x => x.BookId == BookID,
                            Builders<Book>.Update.Set(x => x.BookName, book.BookName)
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
                    else
                    {
                    Book Books = new Book()
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
                    await context.mongoBookollections.InsertOneAsync(Books);
                    return Books;

                    }
                }
                catch (ArgumentNullException e)
                {

                    throw new Exception(e.Message);
                }
            }

       


        public IEnumerable<Book> GetAllBooks()
        {
           var v = context.mongoBookollections.Find(FilterDefinition<Book>.Empty);
            return v.ToList();

        }

        //public async Task<BookModel> UpdateBook(BookModel book)
        //{
        //    try
        //    {
        //        var ifExists = await this.Book.Find(x => x.BookId == book.BookId).SingleOrDefaultAsync();
        //        if (ifExists != null)
        //        {
        //            await this.Book.UpdateOneAsync(x => x.BookId == book.BookId,
        //                Builders<BookModel>.Update.Set(x => x.BookName, book.BookName)
        //                .Set(x => x.Description, book.Description)
        //                .Set(x => x.AuthorName, book.AuthorName)
        //                .Set(x => x.Rating, book.Rating)
        //                .Set(x => x.totalRating, book.totalRating)
        //                .Set(x => x.DiscountPrice, book.DiscountPrice));
        //            return ifExists;

        //        }
        //        else
        //        {
        //            await this.Book.InsertOneAsync(book);
        //            return book;
        //        }
        //    }
        //    catch (ArgumentNullException e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}
    }
}
