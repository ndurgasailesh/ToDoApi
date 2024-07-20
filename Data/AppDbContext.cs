using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TaskScheduler.Data.DBModels;
using TaskScheduler.Data.Models;

namespace TaskScheduler.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskList> TaskLists { get; set; }

        public DbSet<ApplicationUser> AppUsers { get; set; }

        //Seeders
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b => { b.HasMany(p => p.TaskLists); }) ;

            modelBuilder.Entity<TaskList>()
           .HasOne(po => po.ApplicationUser)
           .WithMany(a => a.TaskLists)
           .HasForeignKey(po => po.UserId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
