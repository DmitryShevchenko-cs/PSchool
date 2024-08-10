namespace PSchool.DAL.Entities;

public class Parent : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public IEnumerable<Student> Children { get; set; } = null!;
}