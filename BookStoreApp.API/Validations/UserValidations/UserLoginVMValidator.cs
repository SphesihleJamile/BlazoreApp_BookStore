using BookStoreApp.API.ViewModels.UserViewModels;
using FluentValidation;

namespace BookStoreApp.API.Validations.UserValidations
{
    public class UserLoginVMValidator : AbstractValidator<UserLoginVM>
    {
        public UserLoginVMValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username or Email Address is Required")
                .EmailAddress().WithMessage("Invalid Email Address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Required")
                .MinimumLength(8).WithMessage("Invalid Password");
        }
    }
}
