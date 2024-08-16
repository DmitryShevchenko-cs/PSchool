using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PSchool.BLL.Exceptions;
using PSchool.BLL.Models;
using PSchool.BLL.Services;
using PSchool.BLL.Services.Interfaces;
using PSchool.DAL.Repositories;
using PSchool.DAL.Repositories.Interfaces;

namespace PSchool.Test.Services;

public class StudentServiceTest : DefaultServiceTest<IStudentService, StudentService>
{
    protected override void SetUpAdditionalDependencies(IServiceCollection services)
    {
        services.AddScoped<IParentRepository, ParentRepository>();
        services.AddScoped<IParentService, ParentService>();
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        
        base.SetUpAdditionalDependencies(services);
    }

    [Test]
    public async Task CreateStudentWithParent_GetStudents_DeleteStudent()
    {
        var student = await Service.CreateStudentAsync(new StudentModel
        {
            FirstName = "John",
            SecondName = "Doe",
            Email = "JohnDoe@gmail.com",
            PhoneNumber = "testNumberStudent1",
            Group = "320D",
            Parents = new List<ParentModel>()
            {
                new()
                {
                    FirstName = "Alex",
                    SecondName = "Doe",
                    Email = "AlexDoe@gmail.com",
                    PhoneNumber = "testNumberParent1",
                }
            }
        });

        var students = await Service.GetStudentsAsync(new PaginationRequest
        {
            CurrentPage = 1,
            PageSize = 10
        });

        Assert.That(students.Data.Select(r => r.SecondName), Does.Contain(student.SecondName));

        await Service.DeleteStudentAsync(student.Id);

        Assert.ThrowsAsync<StudentNotFoundException>(async () =>
        {
            await Service.GetStudentByIdAsync(student.Id);
        });


    }

    [Test]
    public async Task CreateStudentWitOutParent_AddParents_DelParent()
    {
        var parentService = ServiceProvider.GetRequiredService<IParentService>();

        var student1 = await Service.CreateStudentAsync(new StudentModel
        {
            FirstName = "Student",
            SecondName = "1",
            Email = "Student1@gmail.com",
            PhoneNumber = "testNumberSt1",
            Group = "320D",
        });
        
        var parent1 = await parentService.CreateParentAsync(new ParentModel
        {
            FirstName = "Parent",
            SecondName = "1",
            Email = "Parent1@gmail.com",
            PhoneNumber = "testNumberPr1",
            Children = new List<StudentModel>()
            {
                new ()
                {
                    Email = student1.Email
                }
            }
        });
        
        var student2 = await Service.CreateStudentAsync(new StudentModel
        {
            FirstName = "John",
            SecondName = "Doe",
            Email = "JohnDoe@gmail.com",
            PhoneNumber = "testNumberSt2",
            Group = "320D",
        });

        student2 = await Service.AddParentAsync(student2.Id, parent1.Id);

        Assert.That(student2.Parents.Select(r => r.Email), Does.Contain(parent1.Email));

        var parent2 = await parentService.CreateParentAsync(new ParentModel
        {
            FirstName = "Julia",
            SecondName = "Doe",
            Email = "JuliaDoe@gmail.com",
            PhoneNumber = "123123123",
            Children = new List<StudentModel>()
            {
                new()
                {
                    Email = "JohnDoe@gmail.com"
                }
            }
        });

        student2 = await Service.GetStudentByIdAsync(student2.Id);
        Assert.That(student2.Parents, Has.Count.EqualTo(2));
        Assert.That(student2.Parents.Select(r => r.Email), Does.Contain(parent2.Email));
        Assert.That(student2.Parents.Select(r => r.Email), Does.Contain(parent1.Email));

        student2 = await Service.RemoveParent(student2.Id, parent1.Id);
        Assert.That(student2.Parents, Has.Count.EqualTo(1));
        Assert.That(student2.Parents.Select(r => r.Email), Does.Contain(parent2.Email));
        Assert.That(student2.Parents.Select(r => r.Email), Does.Not.Contain(parent1.Email));
        
        student1 = await Service.RemoveParent(student1.Id, parent1.Id);
        Assert.That(student1.Parents, Has.Count.EqualTo(0));
        Assert.That(await parentService.GetByIdAsync(parent1.Id), Is.EqualTo(null));
    }

    [Test]
    public async Task CreateStudent_UpdateStudent_EmailWontChange()
    {
        var student1 = await Service.CreateStudentAsync(new StudentModel
        {
            FirstName = "Student",
            SecondName = "1",
            Email = "StudentTryToChange@gmail.com",
            PhoneNumber = "123123123",
            Group = "320D",
        });

        var student = await Service.UpdateStudentAsync(new StudentModel
        {
            Id = student1.Id,
            FirstName = "ChangedName",
            SecondName = "ChangedSecondName",
            Email = "ChangedEmail",
            PhoneNumber = "ChangedPhone",
        });

        Assert.Multiple(() =>
        {
            Assert.That(student.Email, Is.EqualTo("ChangedEmail"));
            Assert.That(student.FirstName, Is.EqualTo("ChangedName"));
            Assert.That(student.SecondName, Is.EqualTo("ChangedSecondName"));
        });
        
        Assert.ThrowsAsync<EmailExistsException>(async () =>
        {
            await Service.CreateStudentAsync(new StudentModel
            {
                FirstName = "Student",
                SecondName = "1",
                Email = "ChangedEmail",
                PhoneNumber = "123123123",
                Group = "320D",
            });
        });
        
        Assert.ThrowsAsync<EmailExistsException>(async () =>
        {
            await Service.UpdateStudentAsync(new StudentModel
            {
                FirstName = "Student",
                SecondName = "1",
                Email = "ChangedEmail",
                PhoneNumber = "123123123",
                Group = "320D",
            });
        });
        
    }
}