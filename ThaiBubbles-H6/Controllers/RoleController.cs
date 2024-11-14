namespace ThaiBubbles_H6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepositories _roleRepo;
        public RoleController(IRoleRepositories temp)
        {
            _roleRepo = temp;
        }

        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                var roles = await _roleRepo.GetAllRoles();

                if (roles == null)
                {
                    return Problem("Nothing was returned from roles, this is unexpected");
                }
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{roleId}")]
        public async Task<ActionResult> GetRolesById(int roleId)
        {
            try
            {
                var role = await _roleRepo.GetRoleById(roleId);

                if (role == null)
                {
                    return NotFound($"role with id {roleId} was not found");
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{roleId}")]
        public async Task<ActionResult> PutRole(int roleId, Role role)
        {
            try
            {
                var roleResult = await _roleRepo.UpdateRole(roleId, role);

                if (role == null)
                {
                    return NotFound($"Role with id {roleId} was not found");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(role);

        }

        //Create Method
        [HttpPost]
        public async Task<ActionResult> PostRole(Role role)
        {
            try
            {
                var createRole = await _roleRepo.CreateRole(role);

                if (createRole == null)
                {
                    return StatusCode(500, "Role was not created. Something failed...");
                }
                return CreatedAtAction("PostRole", new { roleId = createRole.RoleID }, createRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the city {ex.Message}");
            }
        }

        //Delete Method
        [HttpDelete("{roleId}")]
        public async Task<ActionResult> DeleteRole(int roleId)
        {
            try
            {
                var role = await _roleRepo.DeleteRole(roleId);

                if (role == null)
                {
                    return NotFound($"Role with id {roleId} was not found");
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
