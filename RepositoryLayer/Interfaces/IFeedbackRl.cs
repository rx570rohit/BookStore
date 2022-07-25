using DatabaseLayer.Feedback;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRl
    {
        Task<Feedback> AddFeedback(string userId, FeedbackPostModel feedbackPostModel);

        Task<IEnumerable<Feedback>> GetFeedback();
    }
}
