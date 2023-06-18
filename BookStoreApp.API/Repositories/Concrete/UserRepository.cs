using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Repositories.Abstract;
using BookStoreApp.API.Responses;
using BookStoreApp.API.Static;
using BookStoreApp.API.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApp.API.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(BookStoreDbContext dbContext,
                            IMapper mapper,
                            UserManager<ApiUser> userManager,
                            IConfiguration config,
                            ILogger<UserRepository> logger)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._userManager = userManager;
            this._config = config;
            this._logger = logger;
        }

        public async Task<List<string>?> CreateUserAsync(UserCreateVM userCreate)
        {
            try
            {
                var user = _mapper.Map<ApiUser>(userCreate);
                user.UserName = userCreate.Email;
                var result = await _userManager.CreateAsync(user, userCreate.Password);

                if(!result.Succeeded)
                {
                    List<string> errors = new();
                    foreach(var error in result.Errors)
                        errors.Add(item: error.Description);
                    return errors;
                }

                await _userManager.AddToRoleAsync(user, userCreate.Role);
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> GenerateToken(ApiUser user)
        {
            _logger.LogInformation($"Generating Login Token at {nameof(GenerateToken)}");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JwtSettings:Key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.UserId, user.Id),
            };

            if(roleClaims != null && roleClaims.Any())
                claims = claims.Union(roleClaims).ToList();

            if(userClaims != null && userClaims.Count > 0)
                claims = claims.Union(userClaims).ToList();

            var token = new JwtSecurityToken(
                issuer: _config.GetValue<string>("JwtSettings:Issuer"),
                audience: _config.GetValue<string>("JwtSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("JwtSettings:DurationInHours")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object[]> LoginUserAsync(UserLoginVM userLogin)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Username);
                if (user == null)
                    return new object[] { false };

                var isPasswordValid = await _userManager.CheckPasswordAsync(user: user, userLogin.Password);
                if (!isPasswordValid)
                    return new object[] { false };

                var token = await GenerateToken(user: user);

                var authResponse = new AuthResponse()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Token = token
                };

                return new object[] { false, authResponse };
            }
            catch
            {
                throw;
            }
        }
    }
}
