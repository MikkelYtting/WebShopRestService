using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Data;
using WebShopRestService.DTOs;
using WebShopRestService.Models;
using WebShopRestService.Managers;

namespace WebShopRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCredentialsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly UserCredentialsManager _userCredentialsManager;

        public UserCredentialsController(MyDbContext context, UserCredentialsManager userCredentialsManager)
        {
            _context = context;
            _userCredentialsManager = userCredentialsManager;
        }

        // Adds a new user credential to the database.
        [HttpPost("register")]
        public async Task<ActionResult<UserCredential>> Register(UserCredential userCredential)
        {
            // Hash the password before saving it to the database
            userCredential.HashedPassword = _userCredentialsManager.HashPassword(userCredential.HashedPassword); //Hashpass metode fra manager

            _context.UserCredentials.Add(userCredential);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCredential", new { id = userCredential.UserId }, userCredential);
        }

        // Authenticates a user and returns a JWT token for authorized access.
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            // Attempt to retrieve the user by the provided username.
            var userCredential = await _context.UserCredentials
                                               .SingleOrDefaultAsync(u => u.Username == loginDto.Username);

            // If user is not found or password does not match, return unauthorized.
            if (userCredential == null || !_userCredentialsManager.VerifyPassword(loginDto.Password, userCredential.HashedPassword))
            {
                return Unauthorized("Invalid username or password.");
            }

            // If credentials are valid, generate a JWT token for the user with the role claim.
            var token = _userCredentialsManager.GenerateJwtToken(userCredential);
            return Ok(new { token = token, role = userCredential.Role.Name });
        }

        // Retrieves all user credentials.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCredential>>> GetUserCredentials()
        {
            if (_context.UserCredentials == null)
            {
                return NotFound();
            }
            return await _context.UserCredentials.ToListAsync();
        }

        // Retrieves a specific user credential by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCredential>> GetUserCredential(int id)
        {
            if (_context.UserCredentials == null)
            {
                return NotFound();
            }
            var userCredential = await _context.UserCredentials.FindAsync(id);

            if (userCredential == null)
            {
                return NotFound();
            }

            return userCredential;
        }

        // Updates a specific user credential.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCredential(int id, UserCredential userCredential)
        {
            if (id != userCredential.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userCredential).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCredentialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Deletes a specific user credential.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCredential(int id)
        {
            if (_context.UserCredentials == null)
            {
                return NotFound();
            }
            var userCredential = await _context.UserCredentials.FindAsync(id);
            if (userCredential == null)
            {
                return NotFound();
            }

            _context.UserCredentials.Remove(userCredential);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Checks if a user credential exists.
        private bool UserCredentialExists(int id)
        {
            return (_context.UserCredentials?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
