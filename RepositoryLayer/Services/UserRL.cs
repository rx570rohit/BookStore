using DataBaseLayer.Users;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{

    public class UserRL:IUserRL
    {

        private readonly IMongoCollection<User> Users;
        private readonly IMongoDatabase mongodb;
        private readonly IConfiguration Configuration;
        public UserRL(IDBSetting db, IConfiguration configuration)
        {

            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(db._host, db._port);

            settings.UseTls = db._userTls;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
 

            MongoIdentity identity = new MongoInternalIdentity(db._authDbName, db._userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(db._password);

            settings.Credential = new MongoCredential(db._authMechanism, identity, evidence);

            var client = new MongoClient(settings);

            var database = client.GetDatabase(db.DatabaseName);




            //this.Configuration = configuration;
            //var userclient = new MongoClient(db.ConnectionString);
            //var database = userclient.GetDatabase(db.DatabaseName);
            ////var database = userclient.GetDatabase("admin");
           
           

            Users = database.GetCollection<User>("Users");
        }
        public async Task<userPostModel> AddUser(userPostModel userPostModel)
        {
            User user = new User();
            user.FisrtName = userPostModel.FirstName;
            user.LastName = userPostModel.LastName;
            user.EmailId = userPostModel.Email;
            // user.Address = userPostModel.Address;
            user.CreatedDate = userPostModel.CreatedDate;
            user.Password=userPostModel.Password;

            try
            {
                var check = this.Users.AsQueryable().Where(x => x.EmailId == userPostModel.Email).FirstOrDefault();

                if (check == null)
                {
                    await this.Users.InsertOneAsync(user);
                    return userPostModel;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
