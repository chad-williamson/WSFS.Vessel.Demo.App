using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Models
{
    public class VesselInfoContext : DbContext
    {
        public VesselInfoContext(DbContextOptions<VesselInfoContext> options) : base(options)
        { }

        public DbSet<VesselInfo> VesselInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VesselInfo>().HasIndex(u => u.Id);
            modelBuilder.Entity<VesselInfo>().HasIndex(u => u.Name);
        }
    }
}
