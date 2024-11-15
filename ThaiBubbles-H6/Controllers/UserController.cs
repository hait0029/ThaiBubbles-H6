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
            var token = await _userRepo.AuthenticateAsync(logins.Email, logins.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new { Token = token });
        }

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] User registerLog)
        {
            try
            {
                // Check if email already exists
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == registerLog.Email);
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

                // Hash Password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);


                // Find or create the default "Admin" role
                var role = await _context.Role.FirstOrDefaultAsync(r => r.RoleType == "Admin");
                if (role == null)
                {
                    role = new Role { RoleType = "Admin" };
                    _context.Role.Add(role);
                    await _context.SaveChangesAsync();
                }

                // Create new user object
                var newUser = new User
                {
                    Email = registerLog.Email,
                    Password = hashedPassword,
                    FName = registerLog.FName,
                    LName = registerLog.LName,
                    PhoneNr = registerLog.PhoneNr,
                    Address = registerLog.Address,
                    CityId = registerLog.CityId, // Set CityId from the request
                    RoleID = role.RoleID // Assign the role ID here
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
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == EncryptionHelper.Encrypt(registerLog.Email));
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);

                var role = await _context.Role.FirstOrDefaultAsync(r => r.RoleType == "Customer") ?? new Role { RoleType = "Customer" };
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

                _context.User.Add(newUser);
                await _context.SaveChangesAsync();

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

                // Hash Password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);

                // Find or create the default "Customer" role
                var role = await _context.Role.FirstOrDefaultAsync(r => r.RoleType == "Customer");
                if (role == null)
                {
                    role = new Role { RoleType = "Customer" };
                    _context.Role.Add(role);
                    await _context.SaveChangesAsync();
                }

                // Encrypt all user data (if not encrypted already)
                var newUser = new User
                {
                    Email = encryptedEmail, // already encrypted
                    Password = hashedPassword,
                    FName = EncryptionHelper.Encrypt(registerLog.FName),
                    LName = EncryptionHelper.Encrypt(registerLog.LName),
                    PhoneNr = EncryptionHelper.Encrypt(registerLog.PhoneNr),
                    Address = EncryptionHelper.Encrypt(registerLog.Address),
                    CityId = registerLog.CityId,
                    RoleID = role.RoleID
                };

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
                var users = await _userRepo.GetAllUsers();
                if (users == null)
                {
                    return Problem("Nothing was returned from users, this is unexpected");
                }
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
                var user = await _userRepo.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"User with userid {userId} was not found");
                }

                // Ensure to decrypt the fields before sending them back in the response
                user.Email = EncryptionHelper.Decrypt(user.Email);
                user.FName = EncryptionHelper.Decrypt(user.FName);
                user.LName = EncryptionHelper.Decrypt(user.LName);
                user.PhoneNr = EncryptionHelper.Decrypt(user.PhoneNr);
                user.Address = EncryptionHelper.Decrypt(user.Address);

                // Return the user data after decryption
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
