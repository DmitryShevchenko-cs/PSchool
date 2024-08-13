namespace PSchool.BLL.Models;

public class ParentModel : BaseModel
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<StudentModel> Children { get; set; } = null!;
}