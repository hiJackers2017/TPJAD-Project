using System;
using System.Collections.Generic;

namespace AuthorizationApi.Domain.Model
{
    public class User : IEquatable<User>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Image { get; set; }

        public string EmailAddress { get; set; }

        public ICollection<UserRole>? Roles { get; set; }

        public User()
        {
        }

        public User(string username)
        {
            UserName = username;
        }

        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User obj)
        {
            return obj != null &&
                   UserName.Equals(obj.UserName) &&
                   FirstName.Equals(obj.FirstName) &&
                   LastName.Equals(obj.LastName) &&
                   Password.Equals(obj.Password) &&
                   Image.Equals(obj.Image) &&
                   EmailAddress == obj.EmailAddress;
        }
    }
}
