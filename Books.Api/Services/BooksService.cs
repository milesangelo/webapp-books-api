using Books.Api.Models;

namespace Books.Api.Services;

public interface IBooksService
{
    Task<List<Book>> GetAllBooks();
}

public class BooksService : IBooksService
{
    public BooksService()
    {
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return new List<Book>();
    }
}