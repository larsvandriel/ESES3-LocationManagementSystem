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
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);
            builder.HasIndex(l => l.LocationNumber).IsUnique();
            builder.HasOne(l => l.Address).WithOne().IsRequired();
            builder.HasOne(l => l.SiteManager).WithOne().IsRequired();
            builder.HasMany(l => l.Departments).WithOne().IsRequired();
            builder.HasMany(l => l.Rooms).WithOne().IsRequired();
            builder.HasOne(l => l.Inventory).WithOne().IsRequired();
            builder.Property(l => l.Type).IsRequired();
            builder.Property(l => l.Name).IsRequired();
            builder.Property(l => l.Size).IsRequired();
            builder.Property(l => l.PhoneNumber).IsRequired();
            builder.Property(l => l.Email).IsRequired();
            builder.Property(l => l.TimeCreated).IsRequired();
            builder.Property(l => l.Deleted).IsRequired();
            builder.Property(l => l.TimeDeleted);
        }
    }
}
