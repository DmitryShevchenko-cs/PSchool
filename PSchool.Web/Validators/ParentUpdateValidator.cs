using FluentValidation;
using PSchool.Web.Models;

namespace PSchool.Web.Validators;

public class ParentUpdateValidator : AbstractValidator<ParentUpdateModel>
{
    public ParentUpdateValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("First name is required.")
            .NotEmpty().WithMessage("First name cannot be empty.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.SecondName)
            .NotNull().WithMessage("Second name is required.")
            .NotEmpty().WithMessage("Second name cannot be empty.")
            .Length(2, 50).WithMessage("Second name must be between 2 and 50 characters.");
    }
}