using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Presistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> Dep)
        {
            Dep.Property(D => D.Id)
                .UseIdentityColumn(10, 10);
            Dep.Property(D => D.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);

            Dep.Property(D => D.CreatedOn)
                .HasDefaultValueSql("GETDATE()");
            Dep.Property(D => D.LastModifiedOn)
              .HasComputedColumnSql("GETUTCDATE()"); //Not Working, try what is below


            //Dep.Property(D => D.LastModifiedOn)
            //   .ValueGeneratedOnAddOrUpdate()
            //   .HasDefaultValueSql("GETUTCDATE()");


        }
    }
}
