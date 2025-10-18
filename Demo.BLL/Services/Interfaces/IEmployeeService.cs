using Demo.BLL.DTOs.Employee;

namespace Demo.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(CreatedEmployeeDto employee);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName);
        EmployeeDetailsDto? GetById(int id);
        int UpdateEmployee(UpdatedEmployeeDto employee);
    }
}
