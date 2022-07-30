using DatabaseLayer.Address;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRl
    {
        Task<Addresses> AddAddress(string userId, AddressPostModel addressPostModel);

        Task<Addresses> UpdateAddress(string userId, string addressId, AddressPostModel addressPostModel);

        Task<bool> RemoveAddress(string userId,string addressId);
      
        Task<IEnumerable> GetAddresses(string userId);

    }
}
