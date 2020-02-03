using AuthorizationApi.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApi.Domain.DataAccess
{
    public class AuthorizationApiContext: DbContext
    {
        public AuthorizationApiContext(DbContextOptions<AuthorizationApiContext> options):base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    }
}
