namespace SaleTrackerBackend.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration configuration)
    {
        _config = configuration;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    }

    public string GenerateToken(IdentityUser user)
    {
        if (user is null || user.UserName is null)
        {
            throw new Exception("User is null");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var jwtObject = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtObject);
    }
}