namespace PSchool.Web.Models;

public class ParentCreateModel : BaseViewModel
{
    public ICollection<string> StudentsEmails { get; set; } = null!;
}