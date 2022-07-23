using DatabaseLayer.Address;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AddressRl :IAddressRl
    {
        IConfiguration configuration;
        IbookstoreContext context; 
        public AddressRl(IConfiguration configuration, IbookstoreContext context)
        {
            this.context = context;
            this.configuration = configuration; 
        }
        public async Task<Addresses> AddAddress(string userId,AddressPostModel addressPostModel)
        {
            try
            {
                var user = await context.mongoUserCollections.AsQueryable().Where(x => x.UserId == userId).SingleOrDefaultAsync();

                Users userdetail = new Users()
                {
                    FisrtName = user.FisrtName,
                    LastName = user.LastName,
                    EmailId = user.EmailId,
                    Password = null,
                    CreatedDate = user.CreatedDate,
                };
                Addresses addresses = new Addresses()
                {
                    fullAddress = addressPostModel.fullAddress,
                    state = addressPostModel.state,
                    user = userdetail,
                    city = addressPostModel.city,
                    pinCode = addressPostModel.pinCode

                };

                await context.mongoAddressCollections.InsertOneAsync(addresses);

                if (context.mongoAddressCollections.FindAsync(FilterDefinition<Addresses>.Empty) != null)
                {
                    return addresses;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Addresses> UpdateAddress(string userId,AddressPostModel addressPostModel)
        {
            try
            {
                //Builders<Carts>.Update.Set(x => x.Quantity, quantity));
                var user = await context.mongoUserCollections.AsQueryable().Where(x => x.UserId == userId).SingleOrDefaultAsync();

              var x =   await context.mongoAddressCollections.FindOneAndUpdateAsync(x=>x.fullAddress==addressPostModel.fullAddress &&
                x.pinCode==addressPostModel.pinCode,
                Builders<Addresses>.Update.Set(x=>x.fullAddress,addressPostModel.fullAddress)
                .Set(x=>x.pinCode, addressPostModel.pinCode)
                .Set(x=>x.state ,addressPostModel.state)
                .Set(x=>x.city,addressPostModel.city)
                );
                if (x != null)
                {
                    return x;
                }

                return null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<bool> RemoveAddress(string userId, AddressPostModel addressPostModel)
        {
            var x = await context.mongoAddressCollections.FindOneAndDeleteAsync(x=>x.userId==userId 
            && x.fullAddress==addressPostModel.fullAddress && x.pinCode==addressPostModel.pinCode);
            if (x != null)
            {
               return true;
            }
            return false;
        }
        public async Task<IEnumerable> GetAddresses(string userId)
        {

          var x = await context.mongoAddressCollections.AsQueryable().Where(x => x.userId == userId).ToListAsync();
            if (x != null)
            {
                return x;
            }
            return null;
        }

    }
}
