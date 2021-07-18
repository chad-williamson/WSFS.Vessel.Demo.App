using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Models
{
    public class VesselLocationContext : DbContext
    {
        public VesselLocationContext(DbContextOptions<VesselLocationContext> options) : base(options)
        { }

        public DbSet<VesselLocation> VesselLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
