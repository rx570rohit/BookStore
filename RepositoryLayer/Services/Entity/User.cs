using DataBaseLayer.Users;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
    public class User
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }    
        public string FisrtName { get; set; }    
        public string LastName { get; set; }  
        public string EmailId { get; set; }  
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
 //       public string Address { get; set; }
       // public string PhoneNo { get; set; }

    }
}
