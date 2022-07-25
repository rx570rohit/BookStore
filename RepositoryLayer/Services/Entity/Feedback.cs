using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string feedbackID { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int totalRating { get; set; }
        public string Description { get; set; }
        public string BookImage { get; set; }

    }
}
