using AutoMapper;
using Demo.BLL.DTOs.Employee;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository employeeRepository, IMapper _mapper) : IEmployeeService
    {
        public int AddEmployee(CreatedEmployeeDto employee)
        {
            var emp = _mapper.Map<Employee>(employee);

            return employeeRepository.Add(emp);
            
        }

        public bool DeleteEmployee(int id)
        {
            var emp = employeeRepository.GetById(id);
            if (emp is null) return false;
            else { 
            
                emp.IsDeleted = true;
                return employeeRepository.Update(emp) > 0 ? true : false ;
            
            }

        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);


        }

        public EmployeeDetailsDto? GetById(int id)
        {
            var employee = employeeRepository.GetById(id);

            return employee is null ? null : _mapper.Map<EmployeeDetailsDto>(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employee)
        {
            var emp = _mapper.Map<Employee>(employee);

            return employeeRepository.Update(emp);
        }
    }
}
