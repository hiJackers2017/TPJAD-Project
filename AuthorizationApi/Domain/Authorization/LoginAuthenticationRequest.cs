using AuthorizationApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApi.Domain.Authorization
{
    public class LoginAuthenticationRequest
    {
        public AuthenticationCredential AuthenticationCredential { get; set; }

        public void Validate()
        {
            if (AuthenticationCredential == null)
            {
                throw new ValidationException($"The {nameof(AuthenticationCredential)} is null");
            }
        }
    }
}
