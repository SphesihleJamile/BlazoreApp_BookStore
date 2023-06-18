using BookStoreApp.API.ViewModels.UserViewModels;
using FluentValidation;

namespace BookStoreApp.API.Validations.UserValidations
{
    public class UserCreateVMValidator : AbstractValidator<UserCreateVM>
    {
        public UserCreateVMValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is Required")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters long")
                .MaximumLength(200).WithMessage("First Name must be a maximum of 200 characters long");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2).WithMessage("Last Name must be at least 2 characters long")
                .MaximumLength(200).WithMessage("Last Name must be a maximum of 200 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Address is Required")
                .EmailAddress().WithMessage("Invalid Email Address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Required")
                .MinimumLength(8).WithMessage("Password must be a minimum of 8 characters")
                .MaximumLength(200).WithMessage("Password must be a maximum of 200 characters long");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("User Role is Required");
        }
    }
}
