using BookStoreApp.API.ViewModels.BooksViewModels;
using FluentValidation;

namespace BookStoreApp.API.Validations.BookValidations
{
    public class BookCreateVMValidation : AbstractValidator<BookCreateVM>
    {
        public BookCreateVMValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("A Title is required for a book registration")
                .MinimumLength(1).WithMessage("A title must have a minimum of 1 character")
                .MaximumLength(50).WithMessage("A title must have a maximum of 50 characters");

            RuleFor(x => x.Year)
                .LessThanOrEqualTo(DateTime.UtcNow.Year)
                .WithMessage("A book must have a year less than or equals to this year");

            RuleFor(x => x.Isbn)
                .NotEmpty().WithMessage("ISBN is required or book registration")
                .MinimumLength(11).WithMessage("ISBN must have a minimum of 11 characters")
                .MaximumLength(50).WithMessage("ISBN must have a maximum of 50 characters");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("A Price is required for a book registration")
                .InclusiveBetween((decimal)0.0, (decimal)999999999999999999.99)
                .WithMessage("The price cannot be less than 0");

            RuleFor(x => x.AuthorId)
                .NotNull().WithMessage("The Author is required for a book registration");
        }
    }
}
