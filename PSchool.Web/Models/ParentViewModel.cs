namespace PSchool.Web.Models;

public class ParentViewModel : BaseViewModel
{
    public ICollection<StudentPropModel> Children { get; set; } = null!;
}