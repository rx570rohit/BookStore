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

namespace RepositoryLayer.Services
{

    public class UserRL:IUserRL
    {

        IbookstoreContext context;

        private readonly IConfiguration Configuration;
        IConfiguration configuration;

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
            
        

















        public string GetJWTToken(string emailID)
        {
            if (emailID == null)
            {
                return null;
            }
            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("emailID", emailID),

                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                               new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateJwtToken(string email, string userId)
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
                    new Claim("UserId", userId.ToString())
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
        public void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.SendEmail(e.Message.ToString(), GetJWTToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }

      
    }
}
