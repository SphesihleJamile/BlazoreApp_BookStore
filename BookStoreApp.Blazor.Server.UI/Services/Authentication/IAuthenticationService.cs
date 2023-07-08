using BookStoreApp.Blazor.Server.UI.Services.ViewModels;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(UserLoginVM loginModel);
        Task Logout();
    }
}
