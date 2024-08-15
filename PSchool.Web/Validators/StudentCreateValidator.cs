using FluentValidation;
using PSchool.Web.Models;

namespace PSchool.Web.Validators;

public class StudentCreateValidator : AbstractValidator<StudentCreateModel>
{
    public StudentCreateValidator()
    {
        Include(new BaseModelValidator());
        
        RuleFor(x => x.Group)
            .NotNull().WithMessage("Group is required.")
            .NotEmpty().WithMessage("Group cannot be empty.");
    }
}