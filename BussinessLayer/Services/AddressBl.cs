using BussinessLayer.Interfaces;
using DatabaseLayer.Address;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class AddressBl : IAddressBl
    {
        IAddressRl addressRl;
        public AddressBl(IAddressRl addressRl)
        {
           this.addressRl = addressRl;      
        }
        public Task<Addresses> AddAddress(string userId, AddressPostModel addressPostModel)
        {
            return addressRl.AddAddress(userId,addressPostModel);      
        }
        public Task<Addresses> UpdateAddress(string userId, AddressPostModel addressPostModel)
        {
            return addressRl.UpdateAddress(userId,addressPostModel);
        }
        public Task<IEnumerable> GetAddresses(string userId)
        {
            return addressRl.GetAddresses(userId);
        }

        public Task<bool> RemoveAddress(string userId, AddressPostModel addressPostModel)
        {
            return addressRl.RemoveAddress(userId,addressPostModel);   
        }

        
    }
}
