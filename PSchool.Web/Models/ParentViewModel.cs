namespace PSchool.Web.Models;

public class ParentViewModel : BaseViewModel
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<StudentPropModel> Children { get; set; } = null!;
}