namespace BookStoreApp.API.ViewModels.UserViewModels
{
    public class UserCreateVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
