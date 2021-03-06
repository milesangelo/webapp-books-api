using Books.Api.Models;

namespace Books.Api.Services
{
    public interface IUserService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest request);
        public Task<RegisterResponse> RegisterAsync(RegisterRequest model);
    }
}
