namespace Books.Api.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Rating { get; set; }
    public string Notes { get; set; }
    public string UserId { get; set; }
}