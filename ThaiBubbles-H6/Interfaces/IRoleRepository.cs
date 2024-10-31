namespace ThaiBubbles_H6.Interfaces
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllRoles();
        public Task<Role> GetRoleById(int roleId);
        public Task<Role> CreateRole(Role role);
        public Task<Role> UpdateRole(int roleId, Role role);
        public Task<Role> DeleteRole(int roleId);
    }
}
