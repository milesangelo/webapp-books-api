using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Api.Controllers;
using Books.Api.Models;
using Books.Api.Services;
using Books.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Books.UnitTests.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task Login_WithEmptyRequestData_ReturnsBadRequest()
        {
            //var sut = new UsersController();
            //var result = (BadRequestResult) await sut.Login(new LoginRequest());
            //result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Login_WithValidLoginRequest_ReturnsOk()
        {
            //var sut = new UsersController();
            //var result = (OkResult) await sut.Login(new LoginRequest {Email = "test@gmail.com", Password = "123qwe"});
            //result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Login_WithValidLoginRequest_Invokes_UserServiceGet()
        {
            //var mockUsersService = new Mock<IUsersService>();
            //mockUsersService
            //    .Setup(_ => _.GetUser())
            //    .ReturnsAsync(());
        }
    }
}
