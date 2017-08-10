using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EastSeat.TeacherMIS.Web.Models;
using EastSeat.TeacherMIS.Web.Extensions;
using EastSeat.TeacherMIS.Web.Models.Configuration;

namespace EastSeat.TeacherMIS.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Headmaster> Headmasters { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.AddConfiguration(new HeadmasterConfiguration());
            builder.AddConfiguration(new TeacherConfiguration());
            builder.AddConfiguration(new SchoolConfiguration());
        }
    }
}
