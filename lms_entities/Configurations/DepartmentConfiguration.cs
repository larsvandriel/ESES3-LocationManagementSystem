using LocationManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasOne(d => d.HeadOfDepartment).WithOne().IsRequired();
            builder.HasMany(d => d.TeamLeads).WithOne().IsRequired();
            builder.Property(d => d.Name).IsRequired();
            builder.Property(d => d.Email);
            builder.Property(d => d.PhoneNumber);
        }
    }
}
