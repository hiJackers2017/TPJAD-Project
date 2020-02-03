using System.Security.Claims;

namespace AuthorizationApi.Domain
{
    public interface IAuthContainerModel
    {
        string SecurityAlgorithm { get; set; }

        int ExpireMinutes { get; set; }

        Claim[] Claims { get; set; }
    }
}
