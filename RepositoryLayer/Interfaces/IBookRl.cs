using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Book;
using RepositoryLayer.Services.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRl
    {    public Task<Book> AddBook(BookPostModel book);
       
        public Task<bool> DeleteBook(string bookName, string AuthorName);
       
        Task<Book> UpdateBook(BookPostModel book);
        public IEnumerable<Book> GetAllBooks();

    }
}
