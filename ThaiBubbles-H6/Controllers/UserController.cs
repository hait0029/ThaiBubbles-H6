using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThaiBubbles_H6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepositories _userRepo;
        

        public UserController(IUserRepositories temp)
        {
            _userRepo = temp;
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
