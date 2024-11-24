//using ThaiBubbles_H6.Model;

//namespace ProjectUnitTests.Repositories
//{
//    public class UserRepositoryTest
//    {
//        private DbContextOptions<DatabaseContext> options;
//        private DatabaseContext context;
//        private UserRepository userRepository;
//       // private Mock<IConfiguration> configurationMock = new();

//        public UserRepositoryTest()
//        {
//            options = new DbContextOptionsBuilder<DatabaseContext>()
//                .UseInMemoryDatabase("UserRepositoryTests")
//                .Options;

//            context = new DatabaseContext(options);

//            configurationMock.Setup(config => config["Jwt:Key"]).Returns("TestSecretKeyForJwt"); // Mock the JWT key
//            userRepository = new UserRepository(context, configurationMock.Object);
//        }

//        [Fact]
//        public async Task Authenticate_ShouldReturnToken_WhenCredentialsAreCorrect()
//        {
//            // Arrange
//            string email = "test@example.com";
//            string password = "Password123";
//            var user = new User
//            {
//                UserID = 1,
//                Email = email,
//                Password = BCrypt.Net.BCrypt.HashPassword(password),
//                FName = "Test",
//                LName = "User",
//                Role = new Role { RoleType = "Admin" }
//            };

//            await context.User.AddAsync(user);
//            await context.SaveChangesAsync();

//            // Act
//            var token = await userRepository.AuthenticateAsync(email, password);

//            // Assert
//            Assert.NotNull(token);
//            Assert.IsType<string>(token);
//        }

//        [Fact]
//        public async Task Authenticate_ShouldReturnNull_WhenCredentialsAreInvalid()
//        {
//            // Arrange
//            string email = "test@example.com";
//            string password = "WrongPassword";
//            var user = new User
//            {
//                UserID = 1,
//                Email = email,
//                Password = BCrypt.Net.BCrypt.HashPassword("Password123")
//            };

//            await context.User.AddAsync(user);
//            await context.SaveChangesAsync();

//            // Act
//            var token = await userRepository.AuthenticateAsync(email, password);

//            // Assert
//            Assert.Null(token);
//        }

//        [Fact]
//        public async Task CreateUser_ShouldHashPassword_WhenAddingNewUser()
//        {
//            // Arrange
//            var user = new User
//            {
//                UserID = 1,
//                Email = "test@example.com",
//                Password = "PlainTextPassword"
//            };

//            // Act
//            var result = await userRepository.CreateUser(user);

//            // Assert
//            Assert.NotNull(result);
//            Assert.True(BCrypt.Net.BCrypt.Verify("PlainTextPassword", result.Password));
//        }

//        [Fact]
//        public async Task UpdateUser_ShouldHashPassword_WhenPasswordChanges()
//        {
//            // Arrange
//            int userId = 1;
//            var existingUser = new User
//            {
//                UserID = userId,
//                Email = "test@example.com",
//                Password = BCrypt.Net.BCrypt.HashPassword("OldPassword")
//            };

//            var updatedUser = new User
//            {
//                UserID = userId,
//                Email = "test@example.com",
//                Password = "NewPassword"
//            };

//            await context.User.AddAsync(existingUser);
//            await context.SaveChangesAsync();

//            // Act
//            var result = await userRepository.UpdateUser(userId, updatedUser);

//            // Assert
//            Assert.NotNull(result);
//            Assert.True(BCrypt.Net.BCrypt.Verify("NewPassword", result.Password));
//        }
//    }
//}
