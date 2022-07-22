using BussinessLayer.Interfaces;
using DatabaseLayer.Book;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class BookBl : IBookBl
    {
       
        IBookRl bookrl;
        public BookBl(IBookRl bookrl)
        {
            this.bookrl = bookrl;   
        }

        public Task<Book> UpdateBook(BookPostModel book)
        {
            return bookrl.UpdateBook(book);

        }

        Task<Book> IBookBl.AddBook(BookPostModel book)
        {
            return bookrl.AddBook(book);

        }


        Task<bool> IBookBl.DeleteBook(string bookName, string AuthorName)
        {
            return bookrl.DeleteBook(bookName,AuthorName);

        }
        public IEnumerable<Book> GetAllBooks()
        {
            return bookrl.GetAllBooks();

        }
    }
}
