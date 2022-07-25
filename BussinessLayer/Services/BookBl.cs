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

        public Task<Books> UpdateBook(BookPostModel book)
        {
            return bookrl.UpdateBook(book);

        }

        Task<Books> IBookBl.AddBook(BookPostModel book)
        {
            return bookrl.AddBook(book);

        }


        Task<bool> IBookBl.DeleteBook(string bookName, string AuthorName)
        {
            return bookrl.DeleteBook(bookName,AuthorName);

        }
        public Task<IEnumerable<Books>> GetAllBooks()
        {
            return bookrl.GetAllBooks();

        }
    }
}
