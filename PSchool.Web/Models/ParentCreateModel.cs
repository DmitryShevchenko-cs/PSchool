namespace PSchool.Web.Models;

public class ParentCreateModel
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<string> StudentsEmails { get; set; } = null!;
}