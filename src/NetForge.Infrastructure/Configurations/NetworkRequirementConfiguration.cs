using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetForge.Core.Entities;

namespace NetForge.Infrastructure.Configurations
{
    public class NetworkRequirementConfiguration : IEntityTypeConfiguration<NetworkRequirement>
    {
        public void Configure(EntityTypeBuilder<NetworkRequirement> builder)
        {
            // Table name
            builder.ToTable("NetworkRequirements");

            // Primary key
            builder.HasKey(x => x.Id);

            // Properties

            builder.Property(x => x.WirelssAreaSqMeters)
                .HasPrecision(10,2)
                .IsRequired();

            builder.Property(x => x.DeviceCountMin) 
                .IsRequired();
            builder.Property(x => x.DeviceCountMax)
                .IsRequired();

            builder.Property(x => x.BudgetMin)
                .HasPrecision(18, 2)
                .IsRequired(false);
            builder.Property(x => x.BudgetMax)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.RequiredSpeed)
                .IsRequired(); 

            builder.Property(x => x.ExpectedCableMeters)
                .HasPrecision(8, 2)
                .IsRequired();

            builder.Property(x => x.ExistingInfrastructure)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(r => r.UpdatedAt)
                .IsRequired(false);

            builder.HasOne(x=>x.Project)
                .WithOne(x => x.NetworkRequirement)
                .HasForeignKey<NetworkRequirement>(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ProjectId)
                .IsUnique();





        }


    }
}
