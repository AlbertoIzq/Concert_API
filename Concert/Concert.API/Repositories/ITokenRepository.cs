using Concert.API.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace Concert.API.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser identityUser, List<string> roles);
    }
}
