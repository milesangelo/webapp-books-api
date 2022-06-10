using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Books.Api.Constants;
using Books.Api.Data;
using Books.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Books.Api.Services
{
    public class UsersService : IUserService
    {
        private readonly IJwtService _jwtService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BooksUser> _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtService"></param>
        public UsersService(
            UserManager<BooksUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var loginResponse = new LoginResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                loginResponse.IsAuthenticated = false;
                loginResponse.Message = $"No Accounts Registered with {request.Email}.";
                return loginResponse;
            }

            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                loginResponse.Message = $"Welcome to SpotOps.Api, {user.FirstName}!";
                loginResponse.IsAuthenticated = true;
                var jwt = await CreateJwtToken(user);
                loginResponse.Token = _jwtService.WriteToken(jwt);
                loginResponse.Name = user.FirstName;
                loginResponse.Email = user.Email;
                loginResponse.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                loginResponse.Roles = rolesList.ToList();
                return loginResponse;
            }

            loginResponse.IsAuthenticated = false;
            loginResponse.Message = $"Incorrect Credentials for user {user.Email}.";
            return loginResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest model)
        {
            var user = new BooksUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                //LastName = model.LastName,
                //Active = 1,
            };

            var response = new RegisterResponse();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail != null)
            {
                response.Message = $"Email {user.Email} is already registered.";
                response.IsRegistered = false;
                return response;
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
            }
            else if (result.Errors.Any())
            {
                response.Message = $"{JsonSerializer.Serialize(result.Errors.First().Description)}";
                response.IsRegistered = false;
                return response;
            }

            response.Message = $"User Registered with username {user.UserName}";
            response.IsRegistered = true;

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<JwtSecurityToken> CreateJwtToken(BooksUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            return _jwtService.Generate(user, userClaims, roles);
        }
    }
}
