using Books.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    readonly private IBooksService _bookService;
    
    public BooksController(IBooksService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var books = await _bookService.GetAllBooks();

        if (books.Any())
        {
            return Ok(books);
        }

        return Ok();
    }
}
