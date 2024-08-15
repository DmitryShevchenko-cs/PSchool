namespace PSchool.Web.Models;

public class StudentModel : BaseModel
{
    public string Group { get; set; } = null!;
    public ICollection<BaseModel> Parents { get; set; } = null!;
}