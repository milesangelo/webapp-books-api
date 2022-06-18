using Books.Api.Data;
using Books.Api.Models;

namespace Books.Api.Services;

public interface IBooksService
{
    Task<List<Book>> GetAllBooks(string userId);
    Task<BookPostResponse> Post(BookPostRequest book, string userId);
}

public class BooksService : IBooksService
{
    private readonly BooksDbContext _context;
    
    public BooksService(BooksDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooks(string userId)
    {
        return _context.Books.Where(b => b.UserId == userId).ToList();
    }

    public async Task<BookPostResponse> Post(BookPostRequest book, string userId)
    {
        var newBook = new Book()
        {
            Author = book.Author,
            Title = book.Title,
            Rating = Convert.ToDecimal(book.Rating),
            Notes = book.Notes,
            UserId = userId 
        };
        await _context.AddAsync(newBook);
        var saveResult = await _context.SaveChangesAsync();
        if (saveResult > 0)
        {
            return new BookPostResponse()
            {
                Success = true
            };
        }

        return new BookPostResponse()
        {
            Success = false
        };
    }
}