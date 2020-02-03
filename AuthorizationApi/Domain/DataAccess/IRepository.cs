using AuthorizationApi.Domain.Model;
using System.Collections.Generic;

namespace AuthorizationApi.Domain.DataAccess
{
    public interface IRepository
    {
        void Add(User user);
        IList<User> GetAllUsers();
        User GetUserByUsername(string username);
    }
}
