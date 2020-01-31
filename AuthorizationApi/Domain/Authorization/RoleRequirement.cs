using Microsoft.AspNetCore.Authorization;

    internal class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string roles)
        {
            Roles = roles;
        }
        public string Roles { get; private set; }
    }

