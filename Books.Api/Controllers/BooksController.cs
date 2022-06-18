using System.Security.Claims;
using Books.Api.Models;
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
    private IJwtService _jwtService;
    private IHttpContextAccessor _httpContextAccessor;
    /// <summary>
    /// Inj
    /// </summary>
    /// <param name="bookService"></param>
    public BooksController(IBooksService bookService,
        IHttpContextAccessor httpContextAccessor,
        IJwtService jwtService)
    {
        _bookService = bookService;
        _httpContextAccessor = httpContextAccessor;
        _jwtService = jwtService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost()]
    [Route("getAllBooks")]
    public async Task<IActionResult> Get([FromBody] GetBookRequest request)
    {
        var userid = await GetUserId(request.Jwt);
        var books = await _bookService.GetAllBooks(userid);

        if (books.Any())
        {
            return Ok(books);
        }

        return Ok();
    }

    private async Task<string> GetUserId(string jwt)
    {
        var user = await _jwtService.GetUserFromJwtAsync(jwt);
        if (user == null) throw new Exception("Error retrieving user id.");
        return user.Id;
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] BookPostRequest request)
    {
        var userid = await GetUserId(request.Jwt);
        var response = await _bookService.Post(request, userid);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest();
    }

}
