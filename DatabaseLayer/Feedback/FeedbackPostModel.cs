using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Feedback
{
    public class FeedbackPostModel
    {
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        public string BookId { get; set; }

    }
}
