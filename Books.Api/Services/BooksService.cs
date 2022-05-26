using Books.Api.Data;
using Books.Api.Models;

namespace Books.Api.Services;

public interface IBooksService
{
    Task<List<Book>> GetAllBooks();
}

public class BooksService : IBooksService
{
    private readonly BooksDbContext _context;
    
    public BooksService(BooksDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return _context.Books.ToList();
    }
}