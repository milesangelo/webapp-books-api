using Books.Api.Controllers;
using Books.Api.Models;
using Books.Api.Services;
using Books.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Books.UnitTests.Controllers;

public class TestBooksController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange.
        var mockBookService = new Mock<IBooksService>();
        mockBookService
            .Setup(_ => _.GetAllBooks())
            .ReturnsAsync(BooksFixture.GetTestBooks());
        var sut = new BooksController(mockBookService.Object);

        // Act.
        var result = (OkObjectResult) await sut.Get();

        // Assert.
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesBookService() 
    {
        // Arrange.
        var mockBooksService = new Mock<IBooksService>();
        mockBooksService
            .Setup(_ => _.GetAllBooks())
            .ReturnsAsync(new List<Book>());
            
        var sut = new BooksController(mockBooksService.Object);
        await sut.Get();
        mockBooksService.Verify(_ => _.GetAllBooks(), Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfBooks()
    {
        var mockBookService = new Mock<IBooksService>();
        mockBookService
            .Setup(_ => _.GetAllBooks())
            .ReturnsAsync(BooksFixture.GetTestBooks());
        var sut = new BooksController(mockBookService.Object);
        var result = await sut.Get();
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<Book>>();
    }

    [Fact]
    public async Task Get_OnNoBooksFound_Returns404()
    {
        var mockBookService = new Mock<IBooksService>();
        mockBookService
            .Setup(_ => _.GetAllBooks())
            .ReturnsAsync(new List<Book>());
        var sut = new BooksController(mockBookService.Object);
        var result = await sut.Get();

        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}