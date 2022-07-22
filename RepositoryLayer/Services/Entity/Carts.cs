using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
    public class Carts
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string cartID { get; set; }

        [ForeignKey("User")]
        public string userId { get; set; }
        public virtual User user { get; set; }

        [ForeignKey("Books")]
        public string BookId { get; set; }
        public virtual Book book { get; set; }
        public int Quantity { get; set; }


    }
}
