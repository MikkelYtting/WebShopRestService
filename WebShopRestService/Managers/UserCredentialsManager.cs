using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebShopRestService.Data;
using WebShopRestService.Models;
using BCrypt.Net; // Ensure BCrypt.Net library is added to the project.

public class UserCredentialsManager
{
    private readonly string _secretKey; // The secret key used for encoding the JWT token.
    private readonly MyDbContext _context;

    public UserCredentialsManager(string secretKey, MyDbContext context)
    {
        _secretKey = secretKey;
        _context = context;
    }

    // Hashes a password using the BCrypt algorithm.
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Verifies that an input password matches the hashed password stored for a user.
    public bool VerifyPassword(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

    // Generates a JWT token for a given user that includes the user's role as a claim.
    public string GenerateJwtToken(UserCredential user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        // Ensure the Role property is eagerly loaded with the user entity.
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
