using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
    public class WishList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string wishListID { get; set; }
       
     //  [ForeignKey("BookModel")]
        public string BookId { get; set; }
        public virtual Books Book { get; set; }
        
     //  [ForeignKey("RegisterModel")]
        public string userId { get; set; }
        public virtual Users user { get; set; }
    }
}
