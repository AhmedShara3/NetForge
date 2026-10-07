using Microsoft.EntityFrameworkCore;
using NetForge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetForge.Infrastructure.Data
{
    public class NetForgeDbContext : DbContext  
    {
        public NetForgeDbContext(DbContextOptions<NetForgeDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<NetworkRequirement> NetworkRequirements => Set<NetworkRequirement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetForgeDbContext).Assembly);
        }




    }
}
