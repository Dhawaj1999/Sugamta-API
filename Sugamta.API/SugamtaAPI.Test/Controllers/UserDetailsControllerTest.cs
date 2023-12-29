using Microsoft.AspNetCore.Mvc;
using Moq;
using Sugamta.API.Controllers;
using Sugamta.API.Repository.Interface;
using System;
using Xunit;
using Sugamta.API.DTOs.UserDetailsDTOs;

public class UserDetailsControllerTests
{
    [Fact]
    public void GetUserDetails_ReturnsNotFound_WhenUserDetailsNotFound()
    {
        // Arrange
        var userEmail = "nonexistent@example.com";
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.GetUserDetails(userEmail)).Returns((UserDetailsDto)null);
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.GetUserDetails(userEmail);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void GetUserDetails_ReturnsOkResult_WhenUserDetailsFound()
    {
        // Arrange
        var userEmail = "existing@example.com";
        var userDetails = new UserDetailsDto { Email = userEmail };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.GetUserDetails(userEmail)).Returns(userDetails);
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.GetUserDetails(userEmail);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void AddUserDetails_ReturnsOkResult_WhenUserDetailsNotExists()
    {
        // Arrange
        var userDetailsDto = new UserDetailsDto { Email = "newuser@example.com" };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.GetUserDetails(userDetailsDto.Email)).Returns((UserDetailsDto)null);
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.AddUserDetails(userDetailsDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void AddUserDetails_ReturnsBadRequest_WhenUserDetailsExists()
    {
        // Arrange
        var existingUserDetailsDto = new UserDetailsDto { Email = "existinguser@example.com" };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.GetUserDetails(existingUserDetailsDto.Email)).Returns(existingUserDetailsDto);
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.AddUserDetails(existingUserDetailsDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void AddUserDetails_ReturnsInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var userDetailsDto = new UserDetailsDto { Email = "user@example.com" };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.GetUserDetails(userDetailsDto.Email)).Throws(new Exception("Simulated exception"));
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.AddUserDetails(userDetailsDto);

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, (result as ObjectResult)?.StatusCode);
    }

    [Fact]
    public void UpdateUserDetails_ReturnsOkResult_WhenUpdateSuccessful()
    {
        // Arrange
        var userEmail = "existing@example.com";
        var userDetailsDto = new UserDetailsDto { Email = userEmail };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.UpdateUserDetails(It.IsAny<UserDetailsDto>()));
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.UpdateUserDetails(userEmail, userDetailsDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void UpdateUserDetails_ReturnsBadRequest_WhenEmailsDoNotMatch()
    {
        // Arrange
        var userEmailInUrl = "url@example.com";
        var userDetailsDto = new UserDetailsDto { Email = "request@example.com" };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.UpdateUserDetails(userEmailInUrl, userDetailsDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void DeleteUserDetails_ReturnsNotFound_WhenUserDetailsNotFound()
    {
        // Arrange
        var userEmail = "nonexistent@example.com";
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.GetUserDetails(userEmail)).Returns((UserDetailsDto)null);
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.DeleteUserDetails(userEmail);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void DeleteUserDetails_ReturnsOkResult_WhenDeleteSuccessful()
    {
        // Arrange
        var userEmail = "existing@example.com";
        var userDetailsDto = new UserDetailsDto { Email = userEmail };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.GetUserDetails(userEmail)).Returns(userDetailsDto);
        mockUnitOfWork.Setup(u => u.UserDetails.DeleteUserDetails(It.IsAny<Models.Models.UserDetails>()));
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.DeleteUserDetails(userEmail);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void DeleteUserDetails_ReturnsInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var userEmail = "user@example.com";
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.UserDetails.GetUserDetails(userEmail)).Throws(new Exception("Simulated exception"));
        var controller = new UserDetailsController(mockUnitOfWork.Object);

        // Act
        var result = controller.DeleteUserDetails(userEmail);

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, (result as ObjectResult)?.StatusCode);
    }
}

