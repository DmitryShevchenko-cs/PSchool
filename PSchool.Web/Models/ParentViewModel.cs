namespace PSchool.Web.Models;

public class ParentViewModel : BaseModel
{
    public ICollection<StudentPropModel> Children { get; set; } = null!;
}