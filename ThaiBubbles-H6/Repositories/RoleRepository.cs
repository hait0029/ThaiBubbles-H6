namespace ThaiBubbles_H6.Repositories
{
    public class RoleRepository : IRoleRepositories
    {
        private DatabaseContext _context { get; set; }
        public RoleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateRole(Role newRole)
        {
            _context.Role.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole;
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _context.Role.FirstOrDefaultAsync(x => x.RoleID == roleId);
        }
        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Role.ToListAsync();
        }


        public async Task<Role> UpdateRole(int roleId, Role updateRole)
        {
            Role role = await GetRoleById(roleId);
            if (role != null)
            {
                // role.RoleID = updateRole.RoleID;
                role.RoleType = updateRole.RoleType;
                await _context.SaveChangesAsync();
            }
            return role;
        }

        public async Task<Role> DeleteRole(int roleId)
        {
            Role role = await GetRoleById(roleId);
            if (role != null)
            {
                _context.Role.Remove(role);
                await _context.SaveChangesAsync();
            }
            return role;
        }
    }
}

