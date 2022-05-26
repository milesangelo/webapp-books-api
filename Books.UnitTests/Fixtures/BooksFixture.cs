using Books.Api.Models;

namespace Books.UnitTests.Fixtures;

public static class BooksFixture
{
    public static List<Book> GetTestBooks() => new()
    {
        new Book()
        {
            Id = 1,
            Title = "The Titanic",
            Author = "James Patterson",
            Rating = 3.5m
        },
        new Book()
        {
            Id = 2,
            Title = "The Forgetting Machine",
            Author = "Rodrigo Quian Quiroga",
            Rating = 4.75m
        },
        new Book()
        {
            Id = 3,
            Title = "Hyperion",
            Author = "Dan Simmons",
            Rating = 2m
        }
    };

}