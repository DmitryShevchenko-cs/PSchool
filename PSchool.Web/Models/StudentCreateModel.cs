namespace PSchool.Web.Models;

public class StudentCreateModel : BaseModel
{
    public string Group { get; set; } = null!;
    public ICollection<BaseModel> Parents { get; set; } = null!;
}