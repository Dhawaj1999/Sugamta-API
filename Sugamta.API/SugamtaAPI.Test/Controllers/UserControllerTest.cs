/*using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Moq;
using Sugamta.API.Controllers;
using Sugamta.API.Repository.Interface;

namespace SugamtaAPI.Test.Controllers
{
    public  class UserControllerTest
    {


        [Fact]
        public void GetUsers_ReturnsOkResult()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.user.GetUsers()).Returns(() => new List<User>());

            var controller = new UserController(null, mockUnitOfWork.Object);

            // Act
            var result = controller.GetUsers();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetUser_ReturnsOkResult()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.user.GetUser(It.IsAny<string>())).Returns(() => new User());

            var controller = new UserController(null, mockUnitOfWork.Object);

            // Act
            var result = controller.GetUser("test@example.com");

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void CreateUser_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.user.CreateUser(It.IsAny<User>()));

            var controller = new UserController(null, mockUnitOfWork.Object);
            var newUser = new User { Email = "newuser@exampl", Password = "password" };

            // Act
            var result = controller.CreateUser(newUser);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void UpdateUser_ReturnsNoContentResult()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.user.UpdateUser(It.IsAny<string>(), It.IsAny<User>())).Returns(true);

            var controller = new UserController(null, mockUnitOfWork.Object);
            var updatedUser = new User { Email = "test@example.com", Password = "newpassword" };

            // Act
            var result = controller.UpdateUser("test@example.com", updatedUser);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteUser_ReturnsOkResult()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.user.DeleteUser(It.IsAny<string>())).Returns(true);

            var controller = new UserController(null, mockUnitOfWork.Object);

            // Act
            var result = controller.DeleteUser("test@example.com");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        // Add more test cases for other controller methods
    }
}
*/


