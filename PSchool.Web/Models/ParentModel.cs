namespace PSchool.Web.Models;

public class ParentModel : BaseModel
{
    public ICollection<StudentPropModel> Children { get; set; } = null!;
}