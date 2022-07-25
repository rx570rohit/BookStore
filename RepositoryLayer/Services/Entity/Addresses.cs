using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
    [BsonIgnoreExtraElements]
    public class Addresses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string addressID { get; set; }
        public string userId { get; set; }
        public virtual Users user { get; set; }
        public string fullAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public double pinCode { get; set; }


    }
}
