using AuthorizationApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApi.Helpers
{
    public class AuthorizationApiContext : DbContext
    {
        public AuthorizationApiContext(DbContextOptions<AuthorizationApiContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
