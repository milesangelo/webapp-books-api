using Books.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    readonly private IBooksService _bookService;
    
    /// <summary>
    /// Inj
    /// </summary>
    /// <param name="bookService"></param>
    public BooksController(IBooksService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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
