using CanWeFixIt.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace CanWeFixIt.Api.Tests
{
    public class ErrorControllerTests
    {
        private ErrorController CreateSystemUnderTest() => new();

        [Fact]
        [Trait("Category", "Unit")]
        public void HandleError_WhenUserProvideInvalidData_ShouldReturnBadRequest()
        {
            //Arrange
            ErrorController sut = CreateSystemUnderTest();
            IExceptionHandlerFeature exceptionHandlerFeature = 
                new ExceptionHandlerFeature { Error = new ArgumentException() };
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.ControllerContext.HttpContext.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeature);

            //Act
            var actual = sut.HandleError();

            //Assert
            actual.Should().BeAssignableTo<IActionResult>();
            var objectResult = actual as ObjectResult;
            var problemDetails = objectResult.Value as ProblemDetails;
            problemDetails.Detail.Should().Contain("User");
            problemDetails.Title.Should().Contain("Client");
            problemDetails.Status.Should().Be(400);
        }

        // TODO: test other cases, like 500
    }
}
