using AutoMapper;
using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;

namespace Company.Project.PL.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>();
        }
    }
}
