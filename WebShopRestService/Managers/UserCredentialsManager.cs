using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebShopRestService.Data;
using WebShopRestService.Models;
using BCrypt.Net;
using WebShopRestService.Configurations;

public class UserCredentialsManager
{
    private readonly JwtConfig _jwtConfig;
    private readonly MyDbContext _context;

    public UserCredentialsManager(IOptions<JwtConfig> jwtConfig, MyDbContext context)
    {
        _jwtConfig = jwtConfig.Value;
        _context = context;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

    public string GenerateJwtToken(UserCredential user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var userWithRole = _context.UserCredentials.Include(u => u.Role).SingleOrDefault(u => u.UserId == user.UserId);
        var roleName = userWithRole?.Role?.Name ?? string.Empty;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, roleName)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
