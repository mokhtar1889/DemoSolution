using Demo.DAL.Contexts;
using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Demo.DAL.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext context) : GenericRepository<Department>(context), IDepartmentRepository
    {

    }
}
