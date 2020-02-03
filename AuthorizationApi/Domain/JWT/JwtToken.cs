using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace AuthorizationApi.Domain
{
    public class JwtToken
    {
        private readonly string token;

        private readonly string secretKey = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==";

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(token))
                    return false;

                TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                    return true;
                }
                catch (Exception)
                {
                   return false;
                }
            }
        }

        public string UserName
        {
            get
            {
                return GetTokenClaims()
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                    ?.Value;
            }
        }

        public JwtToken(string token)
        {
            this.token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public JwtToken()
        {
            secretKey = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==";
        }

        public JwtToken(IAuthContainerModel model, string secretKey = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==")
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.Claims == null || model.Claims.Length == 0)
                throw new ArgumentException("Invalid model");

            this.secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));

            token = GenerateToken(model);
        }

        private string GenerateToken(IAuthContainerModel model)
        {
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(model.Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), model.SecurityAlgorithm)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }


        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(secretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private IEnumerable<Claim> GetTokenClaims()
        {
            if (string.IsNullOrEmpty(token))
                return null;

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return tokenValid.Claims;
            }
            catch (Exception ex)
            {
               return null;
            }
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        }
        public override string ToString()
        {
            return token;
        }
    }
}
