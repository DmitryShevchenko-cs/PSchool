namespace PSchool.Web.Models;

public class StudentViewModel : BaseViewModel
{
    public string Group { get; set; } = null!;
    public ICollection<BaseViewModel> Parents { get; set; } = null!;
}