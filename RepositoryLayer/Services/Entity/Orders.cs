using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
    [BsonIgnoreExtraElements]
    public class Orders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string orderID { get; set; }

        public string userID { get; set; }


        public virtual Carts Cart { get; set; } 
        public string addressID { get; set; }
        public virtual Addresses Address { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }

        // public int DiscountPrice { get; set; }
        // public int ActualPrice { get; set; }


    }
}
