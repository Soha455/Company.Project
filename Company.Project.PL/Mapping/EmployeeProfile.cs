using AutoMapper;
using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;

namespace Company.Project.PL.Mapping
{
    public class EmployeeProfile : Profile 
    {
        public EmployeeProfile() 
        {
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            //CreateMap<Employee, CreateEmployeeDto>();

        }
    }
}
