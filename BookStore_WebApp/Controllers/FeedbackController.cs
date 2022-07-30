using BussinessLayer.Interfaces;
using DatabaseLayer.Feedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_WebApp.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        IFeedbackBl feedback;
        public FeedbackController(IFeedbackBl feedback)
        {
            this.feedback=feedback;
        }
        [Authorize]
        [HttpPost("AddFeedBack")]
        public async Task<IActionResult> AddFeedBack(FeedbackPostModel feedbackPostModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;
                var data = await feedback.AddFeedback(userId, feedbackPostModel);
                if (data != null)
                {
                    return this.Ok(new { successCode = 200, message = "Your Feedback  Stored Successfully", Data = data } );
                }
                return this.BadRequest(new { successCode =401 ,message="Unable to Store Your Feedback"});
            }
            catch (Exception e)
            {
                throw e;
            }
        
        }
        [HttpGet("GetFeedback")]
        public async Task<IActionResult> GetFeedback()
        {
            try
            {
                var data = await feedback.GetFeedback();
                if (data != null)
                {
                    return this.Ok(new { successCode =200,message="Feedback Loaded Successfully",Data =data});
                }
                return this.BadRequest(new { SuccessCode =401,message ="No Feedback Found" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
