

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DAL.Entites.Employee;
using Shop.DAL.Entites.Helper;

namespace Shop.DAL.Preasistince.Data.Configurations.EmployeeConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasIndex(E => E.Email).IsUnique();
                 builder.Property(E => E.Name).IsRequired().HasColumnType("varChar(50)");
            builder.Property(E => E.Address).IsRequired().HasColumnType("varChar(50)");
            builder.Property(E => E.Salary).IsRequired().HasColumnType("decimal(8,2)");

            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETDATE()");

            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GETDATE()");

            builder.Property(E => E.Gender)
                .HasConversion(

                (gender) => gender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );

            builder.Property(E => E.EmployeeType)
               .HasConversion(

               (type) => type.ToString(),
               (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
               );
        }
    }
}
