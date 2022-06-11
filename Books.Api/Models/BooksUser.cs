using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Books.Api.Models
{
    public class BooksUser : IdentityUser
    {
        public string? FirstName { get; set; }
    }
}
