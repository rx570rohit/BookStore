using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RepositoryLayer.Interfaces;
using System;
using System.Threading.Tasks;
using DatabaseLayer.Book;
using System.Collections.Generic;
using RepositoryLayer.Services.Entity;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_WebApp.Controllers
{
    public class BookController : Controller
    {

        IbookstoreContext context;
        readonly IBookBl bookbl;
        IWebHostEnvironment env;

        public BookController(IBookBl bookBl, IbookstoreContext context, IWebHostEnvironment env)
        {
            this.bookbl = bookBl;
            this.env=env;   

            this.context = context;
        }


        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(BookPostModel book)
        {
            try
            {

                var resp = await this.bookbl.AddBook(book);  
                if (resp != null)
                {

                    return this.Ok(new { Status = true, Message = " Book Record save ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Book Record not Save" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }

        [HttpPut("UpdateBook")]
      
        public async Task<IActionResult> UpdateBook( BookPostModel book)
        {
            try
            {

                var resp = await bookbl.UpdateBook(book);
                if (resp != null)
                {

                    return this.Ok(new { Status = true, Message = " Book Fecord Update Succeessfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Book Record not Update" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [HttpGet("getallbook")]
        
        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                var res = this.bookbl.GetAllBooks();
                return res;

            }

            catch (Exception e)
            {

                throw e;
            }
        }
        public static Image ImageFromByteArray(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            using (Image image = Image.FromStream(ms, true, true))
            {
                return (Image)image.Clone();
            }
        }

        [HttpDelete("DeleteBook")]
      
        public async Task<IActionResult> DeleteBook(string bookName,string AuthorName)
        {
            try
            {

                bool resp = await this.bookbl.DeleteBook(bookName,AuthorName);
                if (resp != false)
                {

                    return this.Ok(new{ Status = true, Message = " Book Delete Successfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Book Not Found" });
                }
            }
            catch (Exception e)
            {
                {
                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }

        }


    }
}
