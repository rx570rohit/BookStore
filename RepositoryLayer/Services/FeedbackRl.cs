using DatabaseLayer.Feedback;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class FeedbackRl : IFeedbackRl
    {
        IbookstoreContext context;
        public FeedbackRl(IbookstoreContext context)
        {
         this.context = context;
        }
        public async Task<Feedback> AddFeedback(string userId,FeedbackPostModel feedbackPostModel)
        {
            try
            {
                var Book = await context.mongoBookCollections.AsQueryable().Where(x => x.BookId == feedbackPostModel.BookId).FirstOrDefaultAsync();
                var User = await context.mongoUserCollections.AsQueryable().Where(x => x.UserId == userId).FirstOrDefaultAsync();
                Feedback feedback = new Feedback()
                {
                    FisrtName =User.FisrtName,
                    LastName = User.LastName,
                    AuthorName = Book.AuthorName,
                    BookImage = Book.BookImage,
                    BookName =  Book.BookName,
                    Created = DateTime.Now,
                    Description = Book.Description,
                    Rating = Book.Rating,
                    totalRating = Book.totalRating,
                    Comment = feedbackPostModel.Comment,
                    BookId = feedbackPostModel.BookId,
                };
                await context.mongoFeedbackCollections.InsertOneAsync(feedback);
                return feedback;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<IEnumerable<Feedback>> GetFeedback()
        {
            try
            {
                var feedback = await context.mongoFeedbackCollections.Find(FilterDefinition<Feedback>.Empty).ToListAsync();
               

                if (feedback != null)
                    return feedback;
                return null;    
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
