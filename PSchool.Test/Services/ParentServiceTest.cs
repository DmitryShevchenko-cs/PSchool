using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PSchool.BLL.Exceptions;
using PSchool.BLL.Models;
using PSchool.BLL.Services;
using PSchool.BLL.Services.Interfaces;
using PSchool.DAL.Entities;
using PSchool.DAL.Repositories;
using PSchool.DAL.Repositories.Interfaces;

namespace PSchool.Test.Services;

public class ParentServiceTest : DefaultServiceTest<IParentService, ParentService>
{
    protected override void SetUpAdditionalDependencies(IServiceCollection services)
    {
        services.AddScoped<IParentRepository, ParentRepository>();
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();
        
        base.SetUpAdditionalDependencies(services);
    }

    [Test]
    public async Task CreateParent_GetParents_DeleteParent()
    {
        var studentService = ServiceProvider.GetRequiredService<IStudentService>();
        var mapper = ServiceProvider.GetRequiredService<IMapper>();

        var student = await studentService.CreateStudentAsync(new StudentModel
        {
            FirstName = "Mark",
            SecondName = "Doe",
            Email = "MarkDoe@gmail.com",
            PhoneNumber = "12345678",
            Group = "320D",
        });
        
        var parent = await Service.CreateParentAsync(new ParentModel
        {
            FirstName = "John",
            SecondName = "Doe",
            Email = "someEmail@gmail.com",
            PhoneNumber = "12345678",
            Children = new List<StudentModel>()
            {
                mapper.Map<StudentModel>(student)
            }
        });

        var parents = await Service.GetParentsAsync(new PaginationRequest
        {
            CurrentPage = 1,
            PageSize = 10
        });

        Assert.That(parents.Data.FirstOrDefault(r => r.FirstName == parent.FirstName), Is.Not.EqualTo(null));

        await Service.DeleteParentAsync(parent.Id);
        
        parents = await Service.GetParentsAsync(new PaginationRequest
        {
            CurrentPage = 1,
            PageSize = 10
        });
            
        Assert.That(parents.Data.FirstOrDefault(r => r.FirstName == parent.FirstName), Is.EqualTo(null));
        var studentDb = await studentService.GetStudentByIdAsync(student.Id);
        Assert.That(studentDb.Parents, Is.Empty);
    }
    
    [Test]
    public async Task CreateParent_UpdateParent_EmailWontChange()
    {
        var studentService = ServiceProvider.GetRequiredService<IStudentService>();
        var student = await studentService.CreateStudentAsync(new StudentModel
        {
            FirstName = "Mark",
            SecondName = "Doe",
            Email = "UpdateParentEmail@gmail.com",
            PhoneNumber = "testNumber1",
            Group = "320D",
        });
        
        var parent1 = await Service.CreateParentAsync(new ParentModel()
        {
            FirstName = "Parent",
            SecondName = "1",
            Email = "ParentTryToChange@gmail.com",
            PhoneNumber = "testNumber2",
            Children = new List<StudentModel>()
            {
                new()
                {
                    Email = student.Email,
                }
            }
        });

        var parent = await Service.UpdateParentAsync(new ParentModel()
        {
            Id = parent1.Id,
            FirstName = "ChangedName",
            SecondName = "ChangedSecondName",
            Email = "ChangedEmail",
            PhoneNumber = "ChangedPhone",
        });

        Assert.Multiple(() =>
        {
            Assert.That(parent.Email, Is.EqualTo(parent1.Email));
            Assert.That(parent.PhoneNumber, Is.EqualTo(parent1.PhoneNumber));
            Assert.That(parent.FirstName, Is.EqualTo("ChangedName"));
            Assert.That(parent.SecondName, Is.EqualTo("ChangedSecondName"));
        });
        
        Assert.ThrowsAsync<EmailExistsException>(async () =>
        {
            await Service.CreateParentAsync(new ParentModel()
            {
                FirstName = "Parent",
                SecondName = "1",
                Email = "ParentTryToChange@gmail.com",
                PhoneNumber = "123123123",
                Children = new List<StudentModel>()
                {
                    new()
                    {
                        Email = student.Email,
                    }
                }
            });
        });
    }
    
}