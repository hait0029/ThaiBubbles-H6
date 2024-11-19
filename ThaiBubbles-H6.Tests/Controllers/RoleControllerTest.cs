using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Controllers
{
    public class RoleControllerTests
    {
        private readonly Mock<IRoleRepositories> _repositoryMock;
        private readonly RoleController _controller;

        public RoleControllerTests()
        {
            _repositoryMock = new Mock<IRoleRepositories>();
            _controller = new RoleController(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetRoles_ShouldReturnOk_WhenRolesExist()
        {
            // Arrange
            var roles = new List<Role>
            {
                new Role { RoleID = 1, RoleType = "Admin" },
                new Role { RoleID = 2, RoleType = "User" }
            };
            _repositoryMock.Setup(repo => repo.GetAllRoles()).ReturnsAsync(roles);

            // Act
            var result = await _controller.GetRoles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRoles = Assert.IsType<List<Role>>(okResult.Value);
            Assert.Equal(2, returnedRoles.Count);
        }

        [Fact]
        public async Task GetRolesById_ShouldReturnOk_WhenRoleExists()
        {
            // Arrange
            var role = new Role { RoleID = 1, RoleType = "Admin" };
            _repositoryMock.Setup(repo => repo.GetRoleById(1)).ReturnsAsync(role);

            // Act
            var result = await _controller.GetRolesById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRole = Assert.IsType<Role>(okResult.Value);
            Assert.Equal("Admin", returnedRole.RoleType);
        }

        [Fact]
        public async Task GetRolesById_ShouldReturnNotFound_WhenRoleDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetRoleById(1)).ReturnsAsync((Role)null);

            // Act
            var result = await _controller.GetRolesById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PutRole_ShouldReturnNotFound_WhenRoleDoesNotExist()
        {
            // Arrange
            var updatedRole = new Role { RoleType = "SuperAdmin" };
            _repositoryMock.Setup(repo => repo.UpdateRole(1, updatedRole)).ReturnsAsync((Role)null);

            // Act
            var result = await _controller.PutRole(1, updatedRole);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PostRole_ShouldReturnCreated_WhenRoleIsValid()
        {
            // Arrange
            var newRole = new Role { RoleType = "Admin" };
            _repositoryMock.Setup(repo => repo.CreateRole(newRole)).ReturnsAsync(newRole);

            // Act
            var result = await _controller.PostRole(newRole);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedRole = Assert.IsType<Role>(createdResult.Value);
            Assert.Equal("Admin", returnedRole.RoleType);
        }

        [Fact]
        public async Task DeleteRole_ShouldReturnOk_WhenRoleExists()
        {
            // Arrange
            var role = new Role { RoleID = 1, RoleType = "Admin" };
            _repositoryMock.Setup(repo => repo.DeleteRole(1)).ReturnsAsync(role);

            // Act
            var result = await _controller.DeleteRole(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRole = Assert.IsType<Role>(okResult.Value);
            Assert.Equal("Admin", returnedRole.RoleType);
        }

        [Fact]
        public async Task DeleteRole_ShouldReturnNotFound_WhenRoleDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteRole(1)).ReturnsAsync((Role)null);

            // Act
            var result = await _controller.DeleteRole(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
