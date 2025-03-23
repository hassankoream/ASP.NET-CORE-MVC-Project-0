using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Employees;

namespace Demo.DAL.Presistance.Data.Configurations.Employees
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");
            builder.Property(E => E.Gender)
                .HasConversion(
                (gender) => gender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );
            builder.Property(E => E.EmployeeType)
               .HasConversion(
               (emptype) => emptype.ToString(),
               (emptype) => (EmployeeType)Enum.Parse(typeof(EmployeeType), emptype)
               );
            builder.Property(E => E.CreatedOn)
                 .HasDefaultValueSql("GETDATE()");
            builder.Property(E => E.LastModifiedOn)
              .HasComputedColumnSql("GETUTCDATE()"); //Not Working, try what is below


            //Dep.Property(D => D.LastModifiedOn)
            //   .ValueGeneratedOnAddOrUpdate()
            //   .HasDefaultValueSql("GETUTCDATE()");



        }
    }
}
