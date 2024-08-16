using FluentValidation;
using PSchool.Web.Models;

namespace PSchool.Web.Validators;

public class BaseModelValidator : AbstractValidator<BaseViewModel>
{
    public BaseModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("First name is required.")
            .NotEmpty().WithMessage("First name cannot be empty.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.SecondName)
            .NotNull().WithMessage("Second name is required.")
            .NotEmpty().WithMessage("Second name cannot be empty.")
            .Length(2, 50).WithMessage("Second name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is required.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotNull().WithMessage("Phone number is required.")
            .NotEmpty().WithMessage("Phone number cannot be empty.")
            .Matches(@"^\+\d{10,15}$").WithMessage("Phone number must start with '+' and contain between 10 and 15 digits.");
    }
}