using DatabaseLayer.Feedback;
using RepositoryLayer.Services.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface IFeedbackBl
    {
        Task<Feedback> AddFeedback(string userId, FeedbackPostModel feedbackPostModel);
        Task<IEnumerable<Feedback>> GetFeedback();
    }
}
