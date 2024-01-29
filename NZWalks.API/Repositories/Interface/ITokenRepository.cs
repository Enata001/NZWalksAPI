using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.Interface;

public interface ITokenRepository
{
    string CreateJwtToken(IdentityUser user, List<string> roles);
}