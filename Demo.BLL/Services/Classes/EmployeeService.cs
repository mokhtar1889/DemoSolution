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
            // first overload
            //var employees = employeeRepository.GetAll();
            //return _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            // second overload
            var employees = employeeRepository.GetAll(e => new EmployeeDto() { 
            
                Id = e.Id,
                Name = e.Name,
                Salary = e.Salary,
                Age = e.Age,
            
            });
            return employees;



            #region IEnumerable (return all data from database and filteration inside the application)
            //var result = employeeRepository.GetEnumerable()
            //                               .Where(e => e.IsDeleted == false)
            //                               .Select(e => new EmployeeDto() { 

            //                                   Id = e.Id,
            //                                   Name = e.Name,
            //                                   Age = e.Age,

            //                               });
            //return result.ToList();

            #endregion

            #region IQuerable (the selection and filteration of the data inside database)
            //var result = employeeRepository.GetQueryable()
            //                               .Where(e => e.IsDeleted == false)
            //                               .Select(e => new EmployeeDto()
            //                               {

            //                                   Id = e.Id,
            //                                   Name = e.Name,
            //                                   Age = e.Age,

            //                               });
            //return result.ToList();

            #endregion


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
