using BookStoreApp.API.ViewModels.UserViewModels;

namespace BookStoreApp.API.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<List<string>?> CreateUserAsync(UserCreateVM userCreate);
        Task<object[]> LoginUserAsync(UserLoginVM userLogin);
    }
}
