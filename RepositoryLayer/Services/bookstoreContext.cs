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

        public IMongoCollection<User> Users;
        public IMongoCollection<Book> Books;

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

            Users = database.GetCollection<User>("Users");
            Books = database.GetCollection<Book>("Books");


        }
        public IMongoCollection<User> mongoUserCollections
        {
            get
            {
                return Users;
            }
        }

        public IMongoCollection<Book> mongoBookollections
        {
            get { return Books; }   
        }
    }
}
