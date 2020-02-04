using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationApi.Domain.Authorization
{
    internal class RolesAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            IEnumerable<Claim> tokenClaims = context.User.FindAll(c => c.Type == ClaimTypes.Role);
            if (tokenClaims == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            string[] requiredRoles = requirement.Roles.ToString().Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (Claim claim in tokenClaims)
            {
                if (requiredRoles.Any(rr => rr == claim.Value))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
