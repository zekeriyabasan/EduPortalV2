using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPortalV2.Models;

namespace EduPortalV2.Models.AppDBContext
{
    public class AppDBContext: IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
        }
        public DbSet<EduPortalV2.Models.Category> Category { get; set; }
        public DbSet<EduPortalV2.Models.Educator> Educator { get; set; }
        public DbSet<EduPortalV2.Models.Course> Course { get; set; }
    }
}
