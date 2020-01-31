using AuthorizationApi.Domain.Model;
using System.Collections.Generic;

namespace AuthorizationApi.Domain.DataAccess
{
    interface IRepository
    {
        public void Add(User user);
        public IList<User> GetAllUsers();
    }
}
