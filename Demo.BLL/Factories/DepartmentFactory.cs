using Demo.BLL.DTOs;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department department)
        {

            return new DepartmentDto()
            {

                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Describtion,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn)

            };

        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {

            return new DepartmentDetailsDto()
            {

                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Describtion,
                DateOfCreation = department.CreatedOn,
                IsDeleted = department.IsDeleted,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn,
            };

        }

        public static Department ToEntity(this CreatedDepartmentDto dto)
        {

            return new Department()
            {

                Name = dto.Name,
                Code = dto.Code,
                Describtion = dto.Description,
                CreatedOn = dto.DateOfCreation
            };

        }

        public static Department ToEntity(this UpdateDepartmentDto dto)
        {

            return new Department()
            {
                Id = dto.Id,
                Name = dto.Name,
                Code = dto.Code,
                Describtion = dto.Description,
                CreatedOn = dto.DateOfCreation
            };



        }
    }
}
