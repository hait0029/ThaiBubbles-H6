using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Repositories
{
    public class RoleRepositoryTests
    {
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;
        private RoleRepository _repository;

        public RoleRepositoryTests()
        {
            // Set up the in-memory database
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "RoleRepositoryTests")
                .Options;

            _context = new DatabaseContext(_options);
            _repository = new RoleRepository(_context);
        }

        [Fact]
        public async Task CreateRole_ShouldAddRole_WhenValid()
        {
            // Arrange
            var role = new Role { RoleType = "Admin" };

            // Act
            var result = await _repository.CreateRole(role);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Admin", result.RoleType);
            Assert.Single(_context.Role);
        }

        [Fact]
        public async Task GetAllRoles_ShouldReturnAllRoles()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Role.Add(new Role { RoleType = "Admin" });
            _context.Role.Add(new Role { RoleType = "User" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllRoles();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetRoleById_ShouldReturnRole_WhenExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var role = new Role { RoleID = 1, RoleType = "Admin" };
            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetRoleById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Admin", result.RoleType);
        }

        [Fact]
        public async Task UpdateRole_ShouldModifyRole_WhenExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var role = new Role { RoleID = 1, RoleType = "Admin" };
            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            var updatedRole = new Role { RoleType = "SuperAdmin" };

            // Act
            var result = await _repository.UpdateRole(1, updatedRole);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SuperAdmin", result.RoleType);
        }

        [Fact]
        public async Task DeleteRole_ShouldRemoveRole_WhenExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var role = new Role { RoleID = 1, RoleType = "Admin" };
            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteRole(1);
            var deletedRole = await _repository.GetRoleById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Null(deletedRole);
        }
    }
}
