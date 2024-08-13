using AutoMapper;
using PSchool.BLL.Models;
using PSchool.DAL.Entities;

namespace PSchool.Web.MapperConfiguration;

public class MapperModelsConfig : Profile
{
    public MapperModelsConfig()
    {
        CreateMap<Student, StudentModel>()
            .ReverseMap();
        
        CreateMap<Parent, ParentModel>()
            .ReverseMap();
    }
}