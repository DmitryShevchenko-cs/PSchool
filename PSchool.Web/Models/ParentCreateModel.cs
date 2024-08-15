namespace PSchool.Web.Models;

public class ParentCreateModel : BaseModel
{
    public ICollection<string> StudentsEmails { get; set; } = null!;
}