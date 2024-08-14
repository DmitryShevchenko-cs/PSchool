using AutoMapper;
using PSchool.BLL.Models;
using PSchool.DAL.Entities;
using PSchool.Web.Models;

namespace PSchool.Web.MapperConfiguration;

public class MapperModelsConfig : Profile
{
    public MapperModelsConfig()
    {
        CreateMap<Student, StudentModel>()
            .ReverseMap();
        
        CreateMap<Parent, ParentModel>()
            .ReverseMap();
        
        
        CreateMap<StudentModel, StudentViewModel>()
            .AfterMap((src, dest) => 
            {
                foreach (var parent in dest.Parents)
                {
                    parent.Children = null!;
                }
            })
            .ReverseMap();
        
        CreateMap<StudentModel, StudentCreateModel>()
            .AfterMap((src, dest) =>
            {
                foreach (var parent in dest.Parents)
                {
                    parent.StudentsEmails = null!;
                }
            })
            .ReverseMap();
        
        CreateMap<ParentModel, ParentViewModel>()
            .AfterMap((src, dest) => 
            {
                foreach (var child in dest.Children)
                {
                    child.Parents = null!;
                }
            })
            .ReverseMap();
        
        CreateMap<ParentCreateModel, ParentModel>()
            .AfterMap((src, dest) => 
            {
                foreach (var email in src.StudentsEmails)
                {
                    dest.Children.Add(new StudentModel()
                    {
                        Email = email
                    });
                }
            })
            .ReverseMap();
        
    }
}