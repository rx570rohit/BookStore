using DataBaseLayer.Users;
using Microsoft.Extensions.Configuration;
using Experimental.System.Messaging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace RepositoryLayer.Services
{

    public class UserRL:IUserRL
    {

        IbookstoreContext context;

        private readonly IConfiguration Configuration;

        private readonly string _secret;


        public UserRL(IConfiguration configuration,IbookstoreContext ibc)
        {

            this.context = ibc;
           
            this.Configuration = configuration;
            
            //var userclient = new MongoClient(db.ConnectionString);
            
           // var database = userclient.GetDatabase(db.DatabaseName);
            this._secret = configuration.GetSection("JwtConfig").GetSection("SecretKey").Value;

            

            
           // Users = database.GetCollection<User>("Users");
            
        }
        public async Task<userPostModel> AddUser(userPostModel userPostModel)
        {
            User user = new User();
            user.FisrtName = userPostModel.FirstName;
            user.LastName = userPostModel.LastName;
            user.EmailId = userPostModel.Email;
            // user.Address = userPostModel.Address;
            user.CreatedDate = userPostModel.CreatedDate;
            user.Password= PwdEncryptDecryptService.EncryptPassword(userPostModel.Password);

            try
            {
                var check = context.mongoUserCollections.AsQueryable().Where(x => x.EmailId == userPostModel.Email).FirstOrDefault();

               // var check = context.mongoCollection(db,configuration).AsQueryable().Where(x => x.EmailId == userPostModel.Email).FirstOrDefault();

               // var check = this.Users.AsQueryable().Where(x => x.EmailId == userPostModel.Email).FirstOrDefault();

                if (check == null)
                {
                    // await this.Users.InsertOneAsync(user);
                     await context.mongoUserCollections.InsertOneAsync(user);

                    return userPostModel;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }
        public string LogInUser(string Email, string password)
        {
           
                try
                {
                    var user = context.mongoUserCollections.AsQueryable().Where(u => u.EmailId == Email).FirstOrDefault();
                    if (user != null)
                    {
                        string Password = PwdEncryptDecryptService.DecryptPassword(user.Password);

                        if (password == Password)
                        {
                        return  GenerateJwtToken(Email, user.UserId);
                        }
                        throw new Exception("Password is invalid");
                    }
                    throw new Exception("Email doesn't Exist");

                }
                catch (Exception e)
                {
                    throw e;
                }
               
        }

        public bool ForgotPassword(string email)
        {

            try
            {
                var user = context.mongoUserCollections.AsQueryable().Where(u => u.EmailId == email).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                else
                {
                    MessageQueue queue;

                    if (MessageQueue.Exists(@".\Private$\BookStore"))
                    {
                        queue = new MessageQueue(@".\Private$\BookStore");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\BookStore");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJwtToken(email, user.UserId);
                    MyMessage.Label = "Forget Password Email";
                    queue.Send(MyMessage);

                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailServices.SendEmail(email, msg.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                    queue.BeginReceive();
                    queue.Close();
                    return true;

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ResetPassword(string email, UserPasswordModel userPasswordModel)
        {
            try
            {
                var user = context.mongoUserCollections.AsQueryable().Where(u => u.EmailId == email).FirstOrDefault();

                if (user == null)
                {
                    return false;
                }
                if (userPasswordModel.Password == userPasswordModel.ConfirmPassword)
                {
                    user.Password = PwdEncryptDecryptService.EncryptPassword(userPasswordModel.Password);
                    //var filter = Builders<BsonDocument>.Filter.Eq("UserId", user.UserId);
                    //var update  = Builders<BsonDocument>.Update.Set("Password", user.Password);
                    context.mongoUserCollections.UpdateOneAsync(x => x.EmailId == email,
                        Builders<User>.Update.Set(x => x.Password, user.Password));
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }

        private string GenerateJwtToken(string email, string userId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this._secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", userId)
                    }),

                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GenerateToken(string Email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._secret);
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email,Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
