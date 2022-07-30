using System;
using System.Collections.Generic;
using System.Text;
using DatabaseLayer.Feedback;
using RepositoryLayer.Services.Entity;
using System.Threading.Tasks;
using BussinessLayer.Interfaces;
using RepositoryLayer.Interfaces;

namespace BussinessLayer.Services
{
    public class FeedbackBl :IFeedbackBl
    {
        IFeedbackRl FeedbackRl;
        public FeedbackBl(IFeedbackRl FeedbackRl)
        {
                this.FeedbackRl = FeedbackRl;   
        }
        public async Task<Feedback> AddFeedback(string userId, FeedbackPostModel feedbackPostModel)
        {
            return await FeedbackRl.AddFeedback(userId, feedbackPostModel);
        }
        public Task<IEnumerable<Feedback>> GetFeedback()
        {
            return FeedbackRl.GetFeedback();    
        }
    }
}
