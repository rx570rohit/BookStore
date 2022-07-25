using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
 
    [BsonIgnoreExtraElements]
    public class Books
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public decimal Rating { get; set; }
        public int totalRating { get; set; }
        public int DiscountPrice { get; set; }
        public int ActualPrice { get; set; }
        public string Description { get; set; }
        public string BookImage { get; set; }
        public string BookQuantity { get; set; }

        
    }
}
