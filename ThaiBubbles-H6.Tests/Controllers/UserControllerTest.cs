//using Microsoft.AspNetCore.Identity.Data;
//using ThaiBubbles_H6.Model;

//namespace ProjectUnitTests.Controllers
//{
//    public class UserControllerTest
//    {
//        private readonly UserController userController;
//        private Mock<IUserRepositories> UserRepositoryMock = new();

//        public UserControllerTest()
//        {
//            userController = new UserController(UserRepositoryMock.Object);
//        }

//        [Fact]
//        public async Task GetAll_ShouldReturnStatusCode200_WhenUsersExist()
//        {
//            // Arrange
//            var users = new List<User>
//            {
//                new User { UserID = 1, FName = "John", LName = "Doe", Address = "123 Main St", PhoneNr = "1234567890" },
//                new User { UserID = 2, FName = "Jane", LName = "Smith", Address = "456 Elm St", PhoneNr = "0987654321" }
//            };

//            UserRepositoryMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(users);

//            // Act
//            var result = (IStatusCodeActionResult)await userController.getUsers();

//            // Assert
//            Assert.Equal(200, result.StatusCode);
//        }

//        [Fact]
//        public async Task Authenticate_ShouldReturnJWTToken_WhenCredentialsAreValid()
//        {
//            // Arrange
//            string email = "test@example.com";
//            string password = "Password123";
//            string token = "mock.jwt.token";

//            UserRepositoryMock.Setup(repo => repo.AuthenticateAsync(email, password)).ReturnsAsync(token);

//            // Act
//            var result = await userController.Authenticate(new LoginRequest { Email = email, Password = password });

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result.Result);
//            Assert.Equal(200, okResult.StatusCode);
//            Assert.Equal(token, okResult.Value);
//        }

//        [Fact]
//        public async Task Authenticate_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
//        {
//            // Arrange
//            string email = "test@example.com";
//            string password = "WrongPassword";

//            UserRepositoryMock.Setup(repo => repo.AuthenticateAsync(email, password)).ReturnsAsync((string?)null);

//            // Act
//            var result = await userController.Authenticate(new LoginRequest { Email = email, Password = password });

//            // Assert
//            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result.Result);
//            Assert.Equal(401, unauthorizedResult.StatusCode);
//        }

//        [Fact]
//        public async Task UpdateUser_ShouldHashPassword_WhenPasswordChanges()
//        {
//            // Arrange
//            int userId = 1;
//            var existingUser = new User { UserID = userId, Password = BCrypt.Net.BCrypt.HashPassword("OldPassword") };
//            var updatedUser = new User { UserID = userId, Password = "NewPassword" };

//            UserRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(existingUser);
//            UserRepositoryMock.Setup(repo => repo.UpdateUser(userId, It.IsAny<User>())).ReturnsAsync(updatedUser);

//            // Act
//            var result = (IStatusCodeActionResult)await userController.PutUser(updatedUser, userId);

//            // Assert
//            Assert.Equal(200, result.StatusCode);
//            UserRepositoryMock.Verify(repo => repo.UpdateUser(userId, It.Is<User>(u => BCrypt.Net.BCrypt.Verify("NewPassword", u.Password))), Times.Once);
//        }

//        [Fact]
//        public async Task Delete_ShouldReturnStatusCode200_WhenUserIsDeleted()
//        {
//            // Arrange
//            int userId = 1;
//            var user = new User { UserID = userId, FName = "John", LName = "Doe" };

//            UserRepositoryMock.Setup(repo => repo.DeleteUser(userId)).ReturnsAsync(user);

//            // Act
//            var result = (IStatusCodeActionResult)await userController.DeleteUser(userId);

//            // Assert
//            Assert.Equal(200, result.StatusCode);
//        }
//    }
//}
