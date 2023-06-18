using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Repositories.Abstract;
using BookStoreApp.API.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;

namespace BookStoreApp.API.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public UserRepository(BookStoreDbContext dbContext,
                            IMapper mapper,
                            UserManager<ApiUser> userManager)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._userManager = userManager;
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

        public async Task<bool> LoginUserAsync(UserLoginVM userLogin)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Username);
                if (user == null)
                    return false;

                var isPasswordValid = await _userManager.CheckPasswordAsync(user: user, userLogin.Password);
                if (!isPasswordValid)
                    return false;

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
