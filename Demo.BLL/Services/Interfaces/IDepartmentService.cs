using Demo.BLL.DTOs.Department;

namespace Demo.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto department);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}