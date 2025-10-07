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
    internal class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
