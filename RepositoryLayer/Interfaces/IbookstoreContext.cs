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
        public IMongoCollection<User> mongoUserCollections { get; }
        public IMongoCollection<Book> mongoBookollections { get; }

    }
}
