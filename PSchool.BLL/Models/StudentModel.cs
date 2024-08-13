namespace PSchool.BLL.Models;

public class StudentModel : BaseModel
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Group { get; set; } = null!;
    public ICollection<ParentModel> Parents { get; set; } = null!;
}