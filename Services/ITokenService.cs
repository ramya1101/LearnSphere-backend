using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearnSphere.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LearnSphere.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

    