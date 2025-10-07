using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
    internal class EmployeeConfiguration : BaseEntityConfiguration<Employee> ,IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Address).HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(e => e.Gender).HasConversion((empGender) => empGender.ToString(),(gender)=>(Gender)Enum.Parse(typeof(Gender),gender));
            builder.Property(e => e.EmployeeType).HasConversion((empType) => empType.ToString(),(type) => (EmployeeType)Enum.Parse(typeof(EmployeeType),type));
            base.Configure(builder);
        }
    }
}
