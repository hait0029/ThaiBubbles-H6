
﻿using ThaiBubbles_H6.Helper;
using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Repositories
{
    public class UserRepositoryTest
    {
        private DbContextOptions<DatabaseContext> options;
        private DatabaseContext context;
        private UserRepository userRepository;
        private Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationMock = new();

        public UserRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("UserRepositoryTests")
                .Options;

            context = new DatabaseContext(options);

            configurationMock.Setup(config => config["Jwt:Key"]).Returns("TestSecretKeyForJwt"); // Mock the JWT key
            userRepository = new UserRepository(context, configurationMock.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldHashPassword_WhenAddingNewUser()
        {
            // Arrange
            var user = new User
            {
                UserID = 1,
                Email = "test@example.com",
                Password = "PlainTextPassword"
            };

            // Act
            var result = await userRepository.CreateLogin(user);

            // Assert
            Assert.NotNull(result);
            Assert.True(BCrypt.Net.BCrypt.Verify("PlainTextPassword", result.Password));
        }

        [Fact]
        public async Task CreateUser_ShouldFail_WhenMissingRequiredFields()
        {
            // Arrange
            await context.Database.EnsureDeletedAsync();

            var incompleteUser = new User
            {
                // Missing Email and Password
                FName = "Incomplete",
                LName = "User"
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => userRepository.CreateLogin(incompleteUser));
        }
        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenUserIdIsInvalid()
        {
            // Arrange
            await context.Database.EnsureDeletedAsync();

            // Act
            var result = await userRepository.GetUserById(99);  // ID does not exist

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldUpdateEmailOnly()
        {
            // Arrange
            await context.Database.EnsureDeletedAsync();

            var user = new User
            {
                UserID = 1,
                FName = "Alice",
                LName = "Smith",
                Email = "old@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password123")
            };
            await context.User.AddAsync(user);
            await context.SaveChangesAsync();

            var updatedUser = new User
            {
                Email = "new@example.com"
            };

            // Act
            var result = await userRepository.UpdateUser(user.UserID, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("new@example.com", EncryptionHelper.Decrypt(result.Email));
            Assert.Equal("Alice", result.FName);  // Unchanged
            Assert.Equal("Smith", result.LName);  // Unchanged
        }

        [Fact]
        public async Task UpdateUser_ShouldHashPassword_WhenPasswordChanges()
        {
            // Arrange
            int userId = 1;
            var existingUser = new User
            {
                UserID = userId,
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("OldPassword")
            };

            var updatedUser = new User
            {
                UserID = userId,
                Email = "test@example.com",
                Password = "NewPassword"
            };

            await context.User.AddAsync(existingUser);
            await context.SaveChangesAsync();

            // Act
            var result = await userRepository.UpdateUser(userId, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.True(BCrypt.Net.BCrypt.Verify("NewPassword", result.Password));
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            await context.Database.EnsureDeletedAsync();

            // Act
            var result = await userRepository.DeleteUser(99);  // Non-existent ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateUser_ShouldStoreHashedPassword()
        {
            // Arrange
            await context.Database.EnsureDeletedAsync();

            var user = new User
            {
                UserID = 1,
                Email = "secure@example.com",
                Password = "PlainTextPassword123"
            };

            // Act
            var result = await userRepository.CreateLogin(user);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual("PlainTextPassword123", result.Password);  // Verify password is hashed
            Assert.True(BCrypt.Net.BCrypt.Verify("PlainTextPassword123", result.Password));  // Verify hash
        }
    }
}
