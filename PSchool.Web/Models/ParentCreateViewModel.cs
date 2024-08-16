namespace PSchool.Web.Models;

public class ParentCreateViewModel : BaseViewModel
{
    public ICollection<string> StudentsEmails { get; set; } = null!;
}