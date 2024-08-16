using System.ComponentModel.DataAnnotations;
using FluentValidation;
using PSchool.Web.Models;

namespace PSchool.Web.Validators;

public class ParentCreateValidator : AbstractValidator<ParentCreateViewModel>
{
    public ParentCreateValidator()
    {
        Include(new BaseModelValidator());
        
        RuleFor(x => x.StudentsEmails)
            .NotNull().WithMessage("StudentsEmails is required.")
            .NotEmpty().WithMessage("StudentsEmails cannot be empty.")
            .Must(emails => emails.All(email => !string.IsNullOrEmpty(email)))
            .WithMessage("All student emails must be non-empty.")
            .Must(emails => emails.All(email => new EmailAddressAttribute().IsValid(email)))
            .WithMessage("All student emails must be valid email addresses.");
    }
}