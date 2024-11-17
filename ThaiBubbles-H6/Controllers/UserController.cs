using ThaiBubbles_H6.Helper;

namespace ThaiBubbles_H6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepositories _userRepo;
        private readonly DatabaseContext _context;

        public UserController(IUserRepositories temp, DatabaseContext context)
        {
            _userRepo = temp;
            _context = context;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login logins)
        {
            try
            {
                // Encrypt the email to match with the encrypted email in the database
                var encryptedEmail = EncryptionHelper.Encrypt(logins.Email);

                // Find the user by the encrypted email
                var user = await _context.User
                    .FirstOrDefaultAsync(u => u.Email == encryptedEmail);

                // If user doesn't exist, return Unauthorized
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Verify the provided password with the stored hashed password
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(logins.Password, user.Password);
                if (!isPasswordValid)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Call your repository to generate the JWT token
                var token = await _userRepo.AuthenticateAsync(user.Email, logins.Password);

                // If token is null, return Unauthorized
                if (token == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Return the token if authentication is successful
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error if something goes wrong
                return StatusCode(500, $"An error occurred while logging in: {ex.Message}");
            }
        }


        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] User registerLog)
        {
            try
            {
                // Check if email already exists

                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == EncryptionHelper.Encrypt(registerLog.Email));
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);

                var role = await _context.Role.FirstOrDefaultAsync(r => r.RoleType == "Admin") ?? new Role { RoleType = "Admin" };
                if (role.RoleID == 0)
                {
                    _context.Role.Add(role);
                    await _context.SaveChangesAsync();
                }

                var newUser = new User
                {
                    Email = EncryptionHelper.Encrypt(registerLog.Email),
                    Password = hashedPassword,
                    FName = EncryptionHelper.Encrypt(registerLog.FName),
                    LName = EncryptionHelper.Encrypt(registerLog.LName),
                    PhoneNr = EncryptionHelper.Encrypt(registerLog.PhoneNr),
                    Address = EncryptionHelper.Encrypt(registerLog.Address),
                    CityId = registerLog.CityId,
                    RoleID = role.RoleID
                };
                // Add user to database
                _context.User.Add(newUser);
                await _context.SaveChangesAsync();

                // Optionally load the City data for response
                await _context.Entry(newUser).Reference(u => u.Cities).LoadAsync();

                return CreatedAtAction("Register", new { userId = newUser.UserID }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the User: {ex.Message}");
            }
        }
        [HttpPost("registerCrud")]
        public async Task<IActionResult> RegisterCrud([FromBody] User registerLog)
        {
            try
            {
                // Encrypt the email before checking if it exists
                var encryptedEmail = EncryptionHelper.Encrypt(registerLog.Email);
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == encryptedEmail);
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

                // Hash Password (password will be sent in plain text by the frontend)
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);

                // Create new user object
                var newUser = new User
                {
                    Email = encryptedEmail,
                    Password = hashedPassword,
                    FName = EncryptionHelper.Encrypt(registerLog.FName),
                    LName = EncryptionHelper.Encrypt(registerLog.LName),
                    PhoneNr = EncryptionHelper.Encrypt(registerLog.PhoneNr),
                    Address = EncryptionHelper.Encrypt(registerLog.Address),
                    CityId = registerLog.CityId, // Set CityId from the request
                    RoleID = registerLog.RoleID // Assign the role ID here
                };

                // Add user to database
                _context.User.Add(newUser);
                await _context.SaveChangesAsync();

                // Optionally load the City data for response
                await _context.Entry(newUser).Reference(u => u.Cities).LoadAsync();

                return CreatedAtAction("Register", new { userId = newUser.UserID }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the User: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User registerLog)
        {
            try
            {
                // Encrypt the email before checking if it exists
                var encryptedEmail = EncryptionHelper.Encrypt(registerLog.Email);
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == encryptedEmail);
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

                // Hash Password (password will be sent in plain text by the frontend)
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);

                // Find or create the default "Customer" role
                var role = await _context.Role.FirstOrDefaultAsync(r => r.RoleType == "Customer");
                if (role == null)
                {
                    role = new Role { RoleType = "Customer" };
                    _context.Role.Add(role);
                    await _context.SaveChangesAsync();
                }

                // Encrypt all user data (don't decrypt here)
                var newUser = new User
                {
                    Email = encryptedEmail,
                    Password = hashedPassword,
                    FName = EncryptionHelper.Encrypt(registerLog.FName),
                    LName = EncryptionHelper.Encrypt(registerLog.LName),
                    PhoneNr = EncryptionHelper.Encrypt(registerLog.PhoneNr),
                    Address = EncryptionHelper.Encrypt(registerLog.Address),
                    CityId = registerLog.CityId,
                    RoleID = role.RoleID
                };

                // Save the new user to the database
                _context.User.Add(newUser);
                await _context.SaveChangesAsync();

                // Optionally load the City data for response
                await _context.Entry(newUser).Reference(u => u.Cities).LoadAsync();

                // Return the new user object with encrypted data (no need to decrypt here)
                var responseUser = new
                {
                    UserID = newUser.UserID,
                    Email = encryptedEmail,
                    RoleType = role.RoleType
                };

                return CreatedAtAction("Register", new { userId = newUser.UserID }, responseUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the User: {ex.Message}");
            }
        }




        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string searchTerm)
        {
            var users = await _userRepo.SearchUsersAsync(searchTerm);
            return Ok(users);
        }




        [HttpGet]
        public async Task<ActionResult> getUsers()
        {
            try
            {
                // Fetch all users from the repository
                var users = await _userRepo.GetAllUsers();

                if (users == null || !users.Any())
                {
                    return Problem("No users were found.");
                }

                // Decrypt sensitive fields for each user
                foreach (var user in users)
                {
                    // Decrypt the sensitive fields
                    user.Email = EncryptionHelper.Decrypt(user.Email);
                    user.FName = EncryptionHelper.Decrypt(user.FName);
                    user.LName = EncryptionHelper.Decrypt(user.LName);
                    user.PhoneNr = EncryptionHelper.Decrypt(user.PhoneNr);
                    user.Address = EncryptionHelper.Decrypt(user.Address);

                
                }

                // Return the decrypted users list
                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            try
            {
                // Fetch the user by ID from the database
                var user = await _userRepo.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"User with userId {userId} was not found.");
                }

                // Decrypt the sensitive fields before returning them
                user.Email = EncryptionHelper.Decrypt(user.Email);
                user.FName = EncryptionHelper.Decrypt(user.FName);
                user.LName = EncryptionHelper.Decrypt(user.LName);
                user.PhoneNr = EncryptionHelper.Decrypt(user.PhoneNr);
                user.Address = EncryptionHelper.Decrypt(user.Address);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }





        [HttpPut("{userId}")]
        public async Task<ActionResult> PutUser(int userId, User user)
        {
            try
            {
                var userResult = await _userRepo.UpdateUser(userId, user);

                if (userResult == null)
                {
                    return NotFound($"User with id {userId} was not found");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(user);

        }

        

        [HttpDelete("{userId}")]

        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                var user = await _userRepo.DeleteUser(userId);

                if (user == null)
                {
                    return NotFound($"User with id {userId} was not found");
                }
                return Ok(user);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
    }
}
