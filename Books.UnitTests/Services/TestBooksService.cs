using Books.Api.Data;
using Books.Api.Models;
using Books.Api.Services;
using Books.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Books.UnitTests.Services;

public class TestBooksService
{
    [Fact]
    public async Task GetAllBooks_WhenCalled_ShouldReturnAllBooksAsList()
    {
        var options = new DbContextOptionsBuilder<BooksDbContext>()
            .UseInMemoryDatabase(databaseName: "BooksDatabase")
            .Options;

        using (var context = new BooksDbContext(options))
        {
            context.Books.AddRange(BooksFixture.GetTestBooks());
            context.SaveChanges();
        }

        using (var context = new BooksDbContext(options))
        {
            var bookService = new BooksService(context);
            var books = await bookService.GetAllBooks();

            books.Count.Should().Be(3);
            books.Should().BeOfType<List<Book>>();
        }
    }
}