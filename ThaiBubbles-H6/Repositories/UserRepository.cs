﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ThaiBubbles_H6.Repositories
{
    public class UserRepository : IUserRepositories
    {
        private DatabaseContext _context { get; set; }
        private IConfiguration _configuration; // For accessing JWT settings

        public async Task<User?> GetLoginByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(l => l.Email == email);
        }

        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            var user = await GetLoginByEmailAsync(email);

            if (user == null || !VerifyPassword(password, user.Password))
                return null;

            return GenerateJwtToken(user);
        }

        // Method to add a new login
        public async Task AddLoginAsync(User login)
        {
            await _context.User.AddAsync(login);
            await _context.SaveChangesAsync();
        }


        // Method to update an existing login
        public async Task UpdateLoginAsync(User login)
        {
            _context.User.Update(login);
            await _context.SaveChangesAsync();
        }
        // Private method to generate the JWT token
        private string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.Email), // User's email as the identity
            new Claim("FirstName", user.FName),     // Custom claim for first name
            new Claim("LastName", user.LName),      // Custom claim for last name
            new Claim("PhoneNr", user.PhoneNr.ToString()), // Phone number as a custom claim
            new Claim("Address", user.Address),      // Address as a custom claim
            new Claim("City", user.CityId.ToString())
        };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Private method to verify the password hash
        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public async Task<User> CreateLogin(User newLogin)
        {
            var existingLogin = await _context.Login.FirstOrDefaultAsync(e => e.Email == newLogin.Email);

            if (existingLogin != null)
            {
                throw new ArgumentException("Login already exists", nameof(newLogin.Email));
            }

            _context.User.Add(newLogin);
            await _context.SaveChangesAsync();

            return newLogin;
        }









        public UserRepository(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<User> CreateUser(User newUser)
        {
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User.Include(e => e.Cities).ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(e => e.UserID == userId);
        }

        public async Task<User> UpdateUser(int userId, User updateUser)
        {
            User user = await GetUserById(userId);
            if (user != null && updateUser != null)
            {
                user.UserID = updateUser.UserID;
                user.FName = updateUser.FName;
                user.LName = updateUser.LName;
                user.PhoneNr = updateUser.PhoneNr;
                user.Address = updateUser.Address;

            }



            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return await GetUserById(userId);
        }
        public async Task<User> DeleteUser(int userId)
        {
            User user = await GetUserById(userId);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}
