using AutoMapper;
using PSchool.BLL.Models;
using PSchool.DAL.Entities;
using PSchool.Web.Models;
using BaseModel = PSchool.Web.Models.BaseModel;
using ParentModel = PSchool.Web.Models.ParentModel;
using StudentModel = PSchool.Web.Models.StudentModel;

namespace PSchool.Web.MapperConfiguration;

public class MapperModelsConfig : Profile
{
    public MapperModelsConfig()
    {
        CreateMap<Student, BLL.Models.StudentModel>()
            .ReverseMap();
        
        CreateMap<BLL.Models.StudentModel, StudentModel>()
            .ReverseMap();
        
        CreateMap<BLL.Models.StudentModel, StudentCreateModel>()
            .ReverseMap();
        
        CreateMap<BLL.Models.StudentModel, StudentPropModel>()
            .ReverseMap();
        
        CreateMap<BLL.Models.StudentModel, StudentUpdateModel>()
            .ReverseMap();
        
        
        CreateMap<Parent, BLL.Models.ParentModel>()
            .ReverseMap();
        
        CreateMap<BLL.Models.ParentModel, ParentModel>()
            .ReverseMap();
        
        CreateMap<BLL.Models.ParentModel, ParentModel>()
            .ReverseMap();
        
        CreateMap<BLL.Models.ParentModel, ParentUpdateModel>()
            .ReverseMap();
        
        
        CreateMap<ParentCreateModel, BLL.Models.ParentModel>()
            .AfterMap((src, dest) => 
            {
                foreach (var email in src.StudentsEmails)
                {
                    dest.Children.Add(new BLL.Models.StudentModel()
                    {
                        Email = email
                    });
                }
            })
            .ReverseMap();

        CreatePaginationResultMapping<BLL.Models.ParentModel, ParentModel>();
        CreatePaginationResultMapping<BLL.Models.StudentModel, StudentModel>();
        
    }

    private void CreatePaginationResultMapping<TViewModel, TModel>()
    {
        CreateMap<PaginationResponse<TViewModel>, PaginationResponse<TModel>>()
            .ReverseMap();
    }
}