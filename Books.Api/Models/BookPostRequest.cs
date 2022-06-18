namespace Books.Api.Models;

public class BookPostRequest
{
    public string Title { get; set; }
    public string Author { get; set; }
    public double Rating { get; set; }
    public string Notes { get; set; }
    public string Jwt { get; set; }
}