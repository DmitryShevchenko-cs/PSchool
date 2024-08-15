namespace PSchool.Web.Models;

public class StudentCreateModel : BaseViewModel
{
    public string Group { get; set; } = null!;
    public ICollection<BaseViewModel> Parents { get; set; } = null!;
}