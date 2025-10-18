using Demo.DAL.Contexts;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(IEmployeeRepository employeeRepository , IDepartmentRepository departmentRepository , ApplicationDbContext dbContext) {

            employeeRepository = _employeeRepository;
            departmentRepository = _departmentRepository;
            dbContext = _dbContext;
        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository ;

        public int saveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
