using Demo.BLL.DTOs;
using Demo.BLL.Factories;
using Demo.DAL.Data.Repositories;


namespace Demo.BLL.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        //get all department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var depts = departmentRepository.GetAll();

            var departmentsToReturn = depts.Select(d => d.ToDepartmentDto());

            return departmentsToReturn;

        }

        public DepartmentDetailsDto? GetById(int id)
        {

            var dept = departmentRepository.GetById(id);

            return dept is null ? null : dept.ToDepartmentDetailsDto();

        }

        public int AddDepartment(CreatedDepartmentDto department)
        {

            return departmentRepository.Add(department.ToEntity());
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {

            return departmentRepository.Update(departmentDto.ToEntity());
        }

        public bool DeleteDepartment(int id)
        {

            var department = departmentRepository.GetById(id);

            if (department is null) return false;

            else
            {
                var res = departmentRepository.Remove(department);
                if (res > 0) return true;
                else return false;
            }
        }
    }
}
