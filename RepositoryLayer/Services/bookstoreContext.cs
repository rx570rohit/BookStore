using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class bookstoreContext: IbookstoreContext
    {

        public IMongoCollection<Users> Users;
        public IMongoCollection<Admin> Admins;

        public IMongoCollection<Books> Books;
        public IMongoCollection<Carts> carts;
        public IMongoCollection<WishList> WishLists;
        public IMongoCollection<Addresses> Addresses;
        public IMongoCollection<Orders> Orders;





        private IConfiguration Configuration;
        public bookstoreContext(IDBSetting db, IConfiguration configuration)
        {

            //MongoClientSettings settings = new MongoClientSettings();
            //settings.Server = new MongoServerAddress(db._host, db._port);

            //settings.UseTls = db._userTls;
            //settings.SslSettings = new SslSettings();
            //settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;


            //MongoIdentity identity = new MongoInternalIdentity(db._authDbName, db._userName);
            //MongoIdentityEvidence evidence = new PasswordEvidence(db._password);

            //settings.Credential = new MongoCredential(db._authMechanism, identity, evidence);

            //var client = new MongoClient(settings);

            //var database = client.GetDatabase(db.DatabaseName);

            this.Configuration = configuration;
            var userclient = new MongoClient(db.ConnectionString);
            var database = userclient.GetDatabase(db.DatabaseName);

            Users = database.GetCollection<Users>("Users");
            Admins = database.GetCollection<Admin>("Admins");

            Books = database.GetCollection<Books>("Books");
            carts = database.GetCollection<Carts>("carts");
            WishLists = database.GetCollection<WishList>("WishLists");
            Addresses = database.GetCollection<Addresses>("Addresses");
            Orders = database.GetCollection<Orders>("Orders");

        }
        public IMongoCollection<Users> mongoUserCollections
        {
            get
            {
                return Users;
            }
        }

        public IMongoCollection<Books> mongoBookCollections
        {
            get { return Books; }   
        }

        public IMongoCollection<Carts> mongoCartCollections
        {
            get { return carts; }
        }
        public IMongoCollection<WishList> mongoWishListCollections
        {
            get { return WishLists; }
        }
        public IMongoCollection<Addresses> mongoAddressCollections
        {
            get { return Addresses; }
        }

        public IMongoCollection<Orders> mongoOrdersCollections
        {
            get { return Orders; }
        }

        public IMongoCollection<Admin> mongoAdminCollections
        {
            get { return Admins; }
        }
    }
}
