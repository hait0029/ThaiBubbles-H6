
//﻿using Microsoft.AspNetCore.Identity.Data;
//using ThaiBubbles_H6.Model;

//namespace ThaiBubbles_H6.Tests.Controllers
//{
//    public class UserControllerTest
//    {
//        private readonly UserController userController;
//        private Mock<IUserRepositories> UserRepositoryMock = new();
//        private readonly Mock<DatabaseContext> dbContextMock = new();

//        public UserControllerTest()
//        {
//            userController = new UserController(UserRepositoryMock.Object, dbContextMock.Object);
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
//        public async Task UpdateUser_ShouldHashPassword_WhenPasswordChanges()
//        {
//            // Arrange
//            int userId = 1;
//            var existingUser = new User { UserID = userId, Password = BCrypt.Net.BCrypt.HashPassword("OldPassword") };
//            var updatedUser = new User { UserID = userId, Password = "NewPassword" };

//            UserRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(existingUser);
//            UserRepositoryMock.Setup(repo => repo.UpdateUser(userId, It.IsAny<User>())).ReturnsAsync(updatedUser);

//            // Act
//            var result = (IStatusCodeActionResult)await userController.PutUser(userId, updatedUser);

//            // Assert
//            Assert.Equal(200, result.StatusCode);
//            //UserRepositoryMock.Verify(repo => repo.UpdateUser(userId, It.Is<User>(u => BCrypt.Net.BCrypt.Verify("NewPassword", u.Password))), Times.Once);
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

//        [Fact]
//        public async Task Delete_ShouldReturnStatusCode404_WhenUserDoesNotExist()
//        {
//            // Arrange
//            int userId = 1;

//            UserRepositoryMock.Setup(repo => repo.DeleteUser(userId)).ReturnsAsync((User)null);

//            // Act
//            var result = (IStatusCodeActionResult)await userController.DeleteUser(userId);

//            // Assert
//            Assert.Equal(404, result.StatusCode);
//        }

//        [Fact]
//        public async Task Delete_ShouldReturnStatusCode500_WhenExceptionIsThrown()
//        {
//            // Arrange
//            int userId = 1;

//            UserRepositoryMock.Setup(repo => repo.DeleteUser(userId)).ThrowsAsync(new Exception("Delete failed"));

//            // Act
//            var result = (IStatusCodeActionResult)await userController.DeleteUser(userId);

//            // Assert
//            Assert.Equal(500, result.StatusCode);
//        }
//    }
//}

