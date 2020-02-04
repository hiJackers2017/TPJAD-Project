using System.Collections.Generic;
using AuthorizationApi.Domain.DataAccess;
using AuthorizationApi.Domain.Model;

namespace AuthorizationApi.Domain.Repository
{
    public class Repository : IRepository
    {
        private static readonly List<User> users = new List<User>
        {
            new User
            {
                UserName = "admin",
                Password = "K2vYl/hxaJfCa7bQjgYIzqsa+Ap4c4pai4Bj8IEzoopVBgKw",
                FirstName = "Marius",
                LastName = "Popovici",
                Image = "",
                Roles = new List<UserRole> { new UserRole("Administrator") }
            }
        };

        public void Add(User user)
        {
            users.Add(user);
        }

        public IList<User> GetAllUsers()
        {
            return users;
        }

        public User GetUserByUsername(string username)
        {
            User user = new User();

            foreach (User u in users)
            {
                if (u.UserName.Equals(username))
                {
                   user = u;
                }
            }

            return user;
        }
    }
}
