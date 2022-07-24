using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IbookstoreContext
    {
        public IMongoCollection<Users> mongoUserCollections { get; }
        public IMongoCollection<Books> mongoBookCollections { get; }

        public IMongoCollection<Carts> mongoCartCollections { get; }

        public IMongoCollection<WishList> mongoWishListCollections { get; }

        public IMongoCollection<Addresses> mongoAddressCollections { get; }

        public IMongoCollection<Orders> mongoOrdersCollections { get; }



    }
}
