namespace PSchool.DAL.Entities;

public class Student : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Group { get; set; } = null!;
    public ICollection<Parent> Parents { get; set; } = null!;
}