using AuthorizationApi.Domain.DataAccess;
using AuthorizationApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthorizationApi.Domain.Authorization
{
    public class LoginAuthentication
    {
        private readonly IRepository repository;
        private readonly AuthenticationCredential authenticationCredential;
        public LoginAuthentication(IRepository repository, AuthenticationCredential authenticationCredential)
        {
            this.repository = repository ?? throw new ArgumentNullException($"The {nameof(repository)} is null");
            this.authenticationCredential = authenticationCredential ?? throw new ArgumentNullException($"The {nameof(repository)} is null");
        }

        public JwtToken Handle()
        {
            User user = repository.GetUserByUsername(authenticationCredential.Username);

            if (user == null)
            {
                return null;
            }

            bool isHashEqualToPassword = HashingUtils.IsGivenHashIdenticalWithGivenString(user.Password, authenticationCredential.Password);
            if (!isHashEqualToPassword)
            {
                return null;
            }

            authenticationCredential.Password = null;

            List<Claim> claims = new List<Claim>();

            foreach (UserRole userRole in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            }

            claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));

            JWTContainerModel jWtContainerModel = new JWTContainerModel()
            {
                Claims = claims.ToArray()
            };

            JwtToken jwtToken = new JwtToken(jWtContainerModel);

            JwtToken result = jwtToken.IsValid ? jwtToken : null;

            return result;
        }
    }

    
}
