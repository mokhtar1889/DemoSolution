using Demo.DAL.Contexts;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.DAL.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext context) : GenericRepository<Employee>(context) , IEmployeeRepository
    {
    }
}
