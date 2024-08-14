namespace PSchool.Web.Models;

public class StudentViewModel : BaseViewModel
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Group { get; set; } = null!;
    public ICollection<ParentPropModel> Parents { get; set; } = null!;
}