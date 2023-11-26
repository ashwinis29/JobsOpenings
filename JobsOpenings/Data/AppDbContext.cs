using JobsOpenings.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace JobsOpenings.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<tblJobs> tblJobs { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
