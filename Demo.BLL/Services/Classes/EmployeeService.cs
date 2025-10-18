using AutoMapper;
using Demo.BLL.DTOs.Employee;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.BLL.Services.Classes
{
    public class EmployeeService(IUnitOfWork unitOfWork,IEmployeeRepository employeeRepository ,IMapper _mapper) : IEmployeeService
    {
        public int AddEmployee(CreatedEmployeeDto employee)
        {
            var emp = _mapper.Map<Employee>(employee);

            unitOfWork.EmployeeRepository.Add(emp);

            return unitOfWork.saveChanges();
            
        }

        public bool DeleteEmployee(int id)
        {
            var emp = unitOfWork.EmployeeRepository.GetById(id);
            if (emp is null) return false;
            else { 
            
                emp.IsDeleted = true;
                unitOfWork.EmployeeRepository.Update(emp)  ;
                return unitOfWork.saveChanges()> 0 ? true : false;


            }

        }

        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            // first overload
            //var employees = employeeRepository.GetAll();
            //return _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            IEnumerable<Employee> employees;
            
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))

                employees = employeeRepository.GetAll();
                else 
                employees = employeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));

            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);



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

            unitOfWork.EmployeeRepository.Update(emp);

            return unitOfWork.saveChanges();
        }
    }
}
