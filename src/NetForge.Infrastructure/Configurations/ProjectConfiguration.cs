using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetForge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetForge.Infrastructure.Configurations
{
    /// <summary>
    /// EF Core Fluent API configuration for the Projects table.
    /// This maps exactly to what we defined in the DBML schema.
    /// </summary>
    /// 
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            // Configure the table name
            builder.ToTable("Projects");

            // Configure the primary key
            builder.HasKey(p => p.Id);

            //Name
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            // SiteType
            builder.Property(p => p.SiteType)
                .IsRequired();

            // NetworkSize
            builder.Property(p => p.NetworkSize)
                .IsRequired();
            // CreatedAt
            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
            // UpdatedAt
            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);




        }
    }
}
