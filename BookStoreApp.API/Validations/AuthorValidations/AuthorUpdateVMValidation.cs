using BookStoreApp.API.ViewModels.AuthorViewModels;
using FluentValidation;

namespace BookStoreApp.API.Validations.AuthorValidations
{
    public class AuthorUpdateVMValidation : AbstractValidator<AuthorUpdateVM>
    {
        public AuthorUpdateVMValidation()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("First Name is required")
                .NotEmpty().WithMessage("First Name is required")
                .MaximumLength(50).WithMessage("First Name must be a maximum of 50 characters")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters long");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("Last Name is required")
                .NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(50).WithMessage("Last Name must be a maximum of 50 characters")
                .MinimumLength(2).WithMessage("Last Name must be at least 2 characters long");

            RuleFor(x => x.Bio)
                .MinimumLength(0)
                .MaximumLength(250).WithMessage("Bio must be a maximum of 250 characters");
        }
    }
}
