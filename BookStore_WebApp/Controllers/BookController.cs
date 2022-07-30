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
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace BookStore_WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger _logger;
        IbookstoreContext context;
        readonly IBookBl bookbl;
        private string cacheKey;
        private readonly IDistributedCache distributedCache;

        IConfiguration configuration;

        public BookController(ILogger<BookController> logger, IBookBl bookBl, IbookstoreContext context, IConfiguration configuration, IDistributedCache distributedCache)
        {
            this.bookbl = bookBl;
            this.configuration= configuration;  
            this.distributedCache=distributedCache; 
            this.context = context;
            cacheKey = configuration.GetSection("redis").GetSection("CacheKey").Value;
            this._logger = logger;  
        }


        [Authorize("Admin")]
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(BookPostModel book)
        {
            _logger.LogTrace("This is a trace log"); 
            _logger.LogDebug("This is a debug log");

            _logger.LogInformation("This is an information log");

            _logger.LogWarning("This is a warning log");


          

            _logger.LogCritical("This is a critical log");

            _logger.LogInformation("Log message in the AddBook() method");

            _logger.LogError("This server is down",DateTime.Now);

            try
            {
                

                var resp = await this.bookbl.AddBook(book);  
                if (resp != null)
                {

                    return this.Ok(new { Status = true, Message = " Books Record save ", Data = resp, _logger });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Books Record not Save" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [Authorize("Admin")]
        [HttpPut("UpdateBook")]
      
        public async Task<IActionResult> UpdateBook( string bookId,BookPostModel book)
        {
            try
            {

                var resp = await bookbl.UpdateBook(bookId,book);
                if (resp != null)
                {

                    return this.Ok(new { Status = true, Message = " Books Fecord Update Succeessfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Books Record not Update" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }

        [HttpGet("GetAllBooksUsingRedisCache")]
        public async Task<IActionResult> GetAllBooksUsingRedisCache()
        {
            _logger.LogInformation("Log message in the GetAllBooksUsingRedisCache() method");
            //var currentUser = HttpContext.User;
            //int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            // var cacheKey = "NotesList";
            string serializedNotesList;

            //      var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId);
            var BookList = await this.bookbl.GetAllBooks();
            if (BookList == null)
            {

                return this.BadRequest(new { status = 404, success = false, message = "Note Doesn't Exits" });

            }


            var redisNotesList = await this.distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                BookList = JsonConvert.DeserializeObject<List<Books>>(serializedNotesList);
            }
            else
            {
                BookList = await this.context.mongoBookCollections.FindAsync(FilterDefinition<Books>.Empty).Result.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(BookList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await this.distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }

            return this.Ok(new { status = 200, isSuccess = true, message = "All notes are loaded", data = BookList ,_logger});
        }

        [HttpGet("getallbook")]
        public Task<IEnumerable<Books>> GetAllBooks()
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

        [Authorize("Admin")]
        [HttpDelete("DeleteBook")]
      
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            try
            {

                bool resp = await this.bookbl.DeleteBook(bookId);
                if (resp != false)
                {

                    return this.Ok(new{ Status = true, Message = " Books Delete Successfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Books Not Found" });
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
