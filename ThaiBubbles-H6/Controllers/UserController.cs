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
                // Check if email already exists
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == registerLog.Email);
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

                // Hash Password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);

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
                // Check if email already exists
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == registerLog.Email);
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
                var users = await _userRepo.GetUserById(userId);
                if (users == null)
                {
                    return NotFound($"User with userid {userId} was not found");
                }
                return Ok(users);
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

        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            try
            {
                var createUser = await _userRepo.CreateUser(user);
                if (createUser == null)
                {
                    return StatusCode(500, "User was not created. Something failed...");
                }
                return CreatedAtAction("PostUser", new { userId = createUser.UserID }, createUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the User {ex.Message}");
            }
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
