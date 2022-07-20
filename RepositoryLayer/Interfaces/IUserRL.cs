using DataBaseLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
    public Task<userPostModel> AddUser(userPostModel userPostModel);

    }
}
