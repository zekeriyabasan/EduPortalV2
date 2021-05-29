using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortalV2.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=MVCDB;integrated security=true;");
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Educator> Educators { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Category> Categories { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Educator>().ToTable("Educator");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>()
        .Property(p => p.PriceDaily)
        .HasColumnType("decimal(18,4)");

        }

    }
}
