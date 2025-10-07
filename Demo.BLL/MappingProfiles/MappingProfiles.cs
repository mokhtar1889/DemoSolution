using AutoMapper;
using Demo.BLL.DTOs.Employee;
using Demo.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
 
            CreateMap<Employee , EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeDetailsDto>().ReverseMap();
            CreateMap<Employee, CreatedEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdatedEmployeeDto>().ReverseMap();

            //// mapping from EmployeeDto to Employee 
            //CreateMap<EmployeeDto, Employee>();

            //// both mapping
            //CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
