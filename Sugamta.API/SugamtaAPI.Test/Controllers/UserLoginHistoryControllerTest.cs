
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sugamta.API.Controllers;
using Sugamta.API.Repository.Interface;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SugamtaAPI.Tests.Controllers
{
    public class UserLoginHistoryControllerTest
    {
        [Fact]
        public void GetAllUserLoginHistory_ReturnsOkResult()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserLoginHistory.GetAllLoginHistory()).Returns(new List<UserLoginHistory>
                {
    new             UserLoginHistory { LoginHistoryId = 1, Email = "user1@example.com", LastLoginTime = DateTime.Now.AddHours(-1) },}) ;
            var controller = new UserLoginHistoryController(unitOfWorkMock.Object);

            // Act
            var result = controller.GetAllUserLoginHistory();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserLoginHistory>>(okResult.Value);
            Assert.NotEmpty(model);
        }

        [Fact]
        public void GetAllUserLoginHistory_ReturnsNotFoundResult()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserLoginHistory.GetAllLoginHistory()).Returns(Enumerable.Empty<UserLoginHistory>());
            var controller = new UserLoginHistoryController(unitOfWorkMock.Object);

            // Act
            var result = controller.GetAllUserLoginHistory();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

            // Adjust the expected type here to ModelStateDictionary if needed
            var modelState = Assert.IsAssignableFrom<ModelStateDictionary>(notFoundResult.Value);

            Assert.Single(modelState);
            Assert.Contains("Empty Data", modelState.Keys);
        }


        [Fact]
        public void GetAllUserLoginHistory_ReturnsBadRequestResultOnException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserLoginHistory.GetAllLoginHistory()).Throws(new Exception("Test Exception"));
            var controller = new UserLoginHistoryController(unitOfWorkMock.Object);

            // Act
            var result = controller.GetAllUserLoginHistory();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Test Exception", badRequestResult.Value);
        }

        [Fact]
        public void GetUserLoginHistoryByEmail_ReturnsOkResult()
        {
            // Arrange
            var email = "test@example.com";
            var expectedUserLoginHistory = new UserLoginHistory
            {
                LoginHistoryId = 1,
                Email = "test@example.com",
                LastLoginTime = DateTime.Now.AddHours(-1),
                LastLogoutTime = DateTime.Now,
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            // Set up the mock to return the expectedUserLoginHistory for the specified email
            unitOfWorkMock.Setup(u => u.UserLoginHistory.GetLoginHistoryByEmail(It.IsAny<Expression<Func<UserLoginHistory, bool>>>()))
                          .Returns(new List<UserLoginHistory> { expectedUserLoginHistory });

            var controller = new UserLoginHistoryController(unitOfWorkMock.Object);

            // Act
            var result = controller.GetUserLoginHistoryByEmail(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserLoginHistory>>(okResult.Value);
            Assert.NotEmpty(model);

            // Optionally, you can add more specific assertions based on your expected data
            Assert.Equal(expectedUserLoginHistory.LoginHistoryId, model.First().LoginHistoryId);
            Assert.Equal(expectedUserLoginHistory.Email, model.First().Email);
            // Add assertions for other properties as needed
        }




        [Fact]
        public void GetUserLoginHistoryByEmail_ReturnsNotFoundResult()
        {
            // Arrange
            var email = "nonexistent@example.com";
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            // Adjust the mock setup based on your scenario, return an empty collection or null for non-existing email
            unitOfWorkMock.Setup(u => u.UserLoginHistory.GetLoginHistoryByEmail(It.IsAny<Expression<Func<UserLoginHistory, bool>>>()))
                          .Returns(Enumerable.Empty<UserLoginHistory>());

            var controller = new UserLoginHistoryController(unitOfWorkMock.Object);

            // Act
            var result = controller.GetUserLoginHistoryByEmail(email);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var modelState = Assert.IsType<ModelStateDictionary>(notFoundResult.Value);
            Assert.Single(modelState);
            Assert.Contains("Empty", modelState.Keys);
        }


        [Fact]
        public void GetUserLoginHistoryByEmail_ReturnsBadRequestResultOnException()
        {
            // Arrange
            var email = "test@example.com";
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserLoginHistory.GetLoginHistoryByEmail(It.IsAny<Expression<Func<UserLoginHistory, bool>>>()))
                          .Throws(new Exception("Test Exception"));
            var controller = new UserLoginHistoryController(unitOfWorkMock.Object);

            // Act
            var result = controller.GetUserLoginHistoryByEmail(email);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Test Exception", badRequestResult.Value);
        }

        [Fact]
        public void CreateUserLoginHistory_ReturnsOkResultOnSuccessfulCreation()
        {
            // Arrange
            var userLoginHistory = new UserLoginHistory { Email = "test@example.com" /* other properties */ };
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.user.GetUser(It.IsAny<string>())).Returns(new User { /* your mock user data */ });
            unitOfWorkMock.Setup(u => u.UserLoginHistory.CreateLoginHistory(It.IsAny<UserLoginHistory>())).Verifiable();
            unitOfWorkMock.Setup(u => u.Save()).Verifiable();
            var controller = new UserLoginHistoryController(unitOfWorkMock.Object);

            // Act
            var result = controller.CreateUserLoginHistory(userLoginHistory);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Login History is Created Successfully.", okResult.Value);
            unitOfWorkMock.Verify(u => u.UserLoginHistory.CreateLoginHistory(It.IsAny<UserLoginHistory>()), Times.Once);
            unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }

        // Add more test cases as needed for other scenarios in CreateUserLoginHistory


    }
}



