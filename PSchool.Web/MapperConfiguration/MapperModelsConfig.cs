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
        
        CreateMap<StudentModel, StudentViewModel>()
            .ReverseMap();
        
        CreateMap<StudentModel, BaseViewModel>()
            .ReverseMap();
        
        CreateMap<StudentModel, StudentCreateModel>()
            .ReverseMap();
        
        CreateMap<StudentModel, StudentPropModel>()
            .ReverseMap();
        
        CreateMap<StudentModel, StudentUpdateModel>()
            .ReverseMap();
        
        
        CreateMap<Parent, ParentModel>()
            .ReverseMap();
        
        CreateMap<ParentModel, ParentViewModel>()
            .ReverseMap();
        
        CreateMap<ParentModel, BaseViewModel>()
            .ReverseMap();
        
        CreateMap<ParentModel, ParentViewModel>()
            .ReverseMap();
        
        CreateMap<ParentModel, ParentUpdateViewModel>()
            .ReverseMap();
        
        
        CreateMap<ParentCreateViewModel, ParentModel>()
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

        CreatePaginationResultMapping<ParentModel, ParentViewModel>();
        CreatePaginationResultMapping<StudentModel, StudentViewModel>();
        
    }

    private void CreatePaginationResultMapping<TViewModel, TModel>()
    {
        CreateMap<PaginationResponse<TViewModel>, PaginationResponse<TModel>>()
            .ReverseMap();
    }
}