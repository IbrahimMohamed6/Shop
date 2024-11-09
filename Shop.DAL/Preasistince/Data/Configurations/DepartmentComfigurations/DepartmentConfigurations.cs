using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DAL.Entites.Department;

namespace Shop.DAL.Preasistince.Data.Configurations.DepartmentComfigurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("VarChar(100)");
            builder.Property(X => X.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(X => X.LastModifiedOn).HasComputedColumnSql("GETDATE()");

            builder.HasMany(x => x.Employees).WithOne(EF => EF.Department)
                .HasForeignKey(E => E.DepartmentID)
                .OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}
