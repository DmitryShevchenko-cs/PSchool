namespace PSchool.Web.Models;

public class StudentCreateModel
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Group { get; set; } = null!;
    public ICollection<ParentCreateModel> Parents { get; set; } = null!;
}