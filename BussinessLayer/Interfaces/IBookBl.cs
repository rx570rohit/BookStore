using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Book;
using RepositoryLayer.Services.Entity;

namespace BussinessLayer.Interfaces
{
    public interface IBookBl
    {
        public Task<Books> AddBook(BookPostModel book);

        public Task<bool> DeleteBook(string bookName, string AuthorName);
        Task<Books> UpdateBook(BookPostModel book);
        Task<IEnumerable<Books>> GetAllBooks();
    }
}
