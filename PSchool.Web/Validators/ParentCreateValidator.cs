using System.ComponentModel.DataAnnotations;
using FluentValidation;
using PSchool.Web.Models;

namespace PSchool.Web.Validators;

public class ParentCreateValidator : AbstractValidator<ParentCreateViewModel>
{
    public ParentCreateValidator()
    {
        Include(new BaseModelValidator());
        
        RuleFor(x => x.StudentsEmail)
            .NotNull().WithMessage("Email is required.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}