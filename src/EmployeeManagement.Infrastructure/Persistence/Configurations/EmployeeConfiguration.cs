using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.Persistence.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(100).IsRequired();
        }
    }
}
