using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AuthorizationApi.Domain
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public int ExpireMinutes { get; set; }

        public string SecurityAlgorithm { get; set; }

        public Claim[] Claims { get; set; }

        public JWTContainerModel()
        {
            ExpireMinutes = 60;
            SecurityAlgorithm = SecurityAlgorithms.HmacSha256Signature;
        }
    }
}
