﻿using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Book;
using RepositoryLayer.Services.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRl
    {    public Task<Books> AddBook(BookPostModel book);
       
        public Task<bool> DeleteBook(string bookId);
       
        Task<Books> UpdateBook(string bookId,BookPostModel book);
        Task<IEnumerable<Books>> GetAllBooks();

    }
}
