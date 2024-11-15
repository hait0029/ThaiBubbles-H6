using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Repositories
{
    public class LoginRepositoriesTests
    {
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;
        private LoginRepository _loginRepository;

        public LoginRepositoriesTests()
        {
            // Set up the in-memory database
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LoginRepositoryTests")
                .Options;

            _context = new DatabaseContext(_options);
            _loginRepository = new LoginRepository(_context);
        }

        [Fact]
        public async Task CreateLogin_ShouldAddNewLogin_WhenLoginIsValid()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var newLogin = new Login { Email = "test@example.com", Password = "password123" };

            // Act
            var result = await _loginRepository.CreateLogin(newLogin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newLogin.Email, result.Email);
            Assert.Equal(1, _context.Login.Count());
        }

        [Fact]
        public async Task GetLoginById_ShouldReturnLogin_WhenLoginExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var login = new Login { LoginID = 1, Email = "user@example.com", Password = "securepass" };
            _context.Login.Add(login);
            await _context.SaveChangesAsync();

            // Act
            var result = await _loginRepository.GetLoginById(login.LoginID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(login.LoginID, result.LoginID);
        }

        [Fact]
        public async Task UpdateLogin_ShouldModifyLogin_WhenLoginExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var login = new Login { LoginID = 1, Email = "oldemail@example.com", Password = "oldpass" };
            _context.Login.Add(login);
            await _context.SaveChangesAsync();

            var updatedLogin = new Login { Email = "newemail@example.com", Password = "newpass" };

            // Act
            var result = await _loginRepository.UpdateLogin(login.LoginID, updatedLogin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("newemail@example.com", result.Email);
            Assert.Equal("newpass", result.Password);
        }

        [Fact]
        public async Task DeleteLogin_ShouldRemoveLogin_WhenLoginExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var login = new Login { LoginID = 1, Email = "delete@example.com", Password = "deletepass" };
            _context.Login.Add(login);
            await _context.SaveChangesAsync();

            // Act
            var result = await _loginRepository.DeleteLogin(login.LoginID);
            var deletedLogin = await _loginRepository.GetLoginById(login.LoginID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(login.LoginID, result.LoginID);
            Assert.Null(deletedLogin);
        }
    }
}
