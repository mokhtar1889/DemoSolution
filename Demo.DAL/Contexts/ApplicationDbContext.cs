using Demo.DAL.Data.Configurations;
using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Models.EmployeeModel;
using Microsoft.EntityFrameworkCore;


namespace Demo.DAL.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Models.DepartmentModel.Department> Departments { get; set; }
        public DbSet<Models.EmployeeModel.Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Models.DepartmentModel.Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfiguration<Models.EmployeeModel.Employee>(new EmployeeConfiguration());
        }
    }
}
