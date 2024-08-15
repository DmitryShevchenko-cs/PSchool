namespace PSchool.Web.Models;

public class StudentViewModel : BaseModel
{
    public string Group { get; set; } = null!;
    public ICollection<BaseModel> Parents { get; set; } = null!;
}