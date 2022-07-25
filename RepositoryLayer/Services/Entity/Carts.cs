using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
    [BsonIgnoreExtraElements]
    public class Carts
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string cartId{ get; set; }

     //  [ForeignKey("Users")]
        public string userId { get; set; }
        public virtual Users user { get; set; }

     //  [ForeignKey("Books")]
        public string BookId { get; set; }
        public virtual Books book { get; set; }
        public int Quantity { get; set; }


    }
}
