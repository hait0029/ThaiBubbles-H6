﻿using System.ComponentModel;
using ThaiBubbles_H6.Helper;

namespace ThaiBubbles_H6.Repositories
{
    public class UserRepository : IUserRepositories
    {
        private DatabaseContext _context { get; set; }
        private IConfiguration _configuration; // For accessing JWT settings

        public async Task<User?> GetLoginByEmailAsync(string email)
        {
            // Include the Role when fetching the user
            return await _context.User
                .Include(u => u.Role) // This ensures that Role is loaded
                .FirstOrDefaultAsync(l => l.Email == email);
        }

        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            var user = await GetLoginByEmailAsync(email);
            // Check if the user exists and the password is correct
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
            new Claim("userID", user.UserID.ToString()), // Include userID here
            new Claim(ClaimTypes.Name, user.Email), // User's email as the identity
            new Claim("FirstName", user.FName),     // Custom claim for first name
            new Claim("LastName", user.LName),      // Custom claim for last name
            new Claim("PhoneNr", user.PhoneNr), // Phone number as a custom claim
            new Claim("Address", user.Address),      // Address as a custom claim
            new Claim("City", user.CityId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.RoleType) // Add RoleType to claims
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
            // Validate required fields
            if (string.IsNullOrEmpty(newLogin.Email) || string.IsNullOrEmpty(newLogin.Password))
            {
                throw new ArgumentException("Email and Password are required fields.");
            }

            var existingLogin = await _context.User.FirstOrDefaultAsync(e => e.Email == newLogin.Email);
            if (existingLogin != null)
            {
                throw new ArgumentException("Login already exists", nameof(newLogin.Email));
            }

            // Hash the password before saving
            // hashing example $2b$12$4F6ksTQcVqlsPsvL2RR7ge76z.UhPZdu43RCkKEDGoe43pVDFfxUi

            //$2b$: Indicates bcrypt is being used.
            //12: The cost factor(how computationally expensive the hashing is).
            //4F6ksTQcVqls...: This part contains the salt and the hash.
            string salt = BCrypt.Net.BCrypt.GenerateSalt(); // I made this change
            newLogin.Password = BCrypt.Net.BCrypt.HashPassword(newLogin.Password, salt);

            _context.User.Add(newLogin);
            await _context.SaveChangesAsync();

            return newLogin;
        }





        public UserRepository(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User.Include(e => e.Cities).Include(e => e.Role).ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.User.Include(u => u.Cities).Include(e => e.Role).FirstOrDefaultAsync(e => e.UserID == userId);

        }

      
        public async Task<User> UpdateUser(int userId, User updateUser)
        {
            // Retrieve the current user record from the database
            var existingUser = await GetUserById(userId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            // Update the existing user fields
            existingUser.Email = EncryptionHelper.Encrypt(updateUser.Email);
            existingUser.FName = EncryptionHelper.Encrypt(updateUser.FName);
            existingUser.LName = EncryptionHelper.Encrypt(updateUser.LName);
            existingUser.PhoneNr = EncryptionHelper.Encrypt(updateUser.PhoneNr);
            existingUser.Address = EncryptionHelper.Encrypt(updateUser.Address);
            existingUser.CityId = updateUser.CityId;
            existingUser.RoleID = updateUser.RoleID;

            // Check if the password has changed and hash it
            if (!BCrypt.Net.BCrypt.Verify(updateUser.Password, existingUser.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updateUser.Password);
            }

            // Mark the entity as modified
            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingUser;
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

        public async Task<List<User>> SearchUsersAsync(string searchTerm)
        {
            return await _context.User
                .Include(p => p.Cities)  // Include User information
                .Where(p => p.FName.Contains(searchTerm) || p.LName.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
