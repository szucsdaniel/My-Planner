using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using webapi.Models;

namespace webapi
{
    public class MyPlannerDbContext : DbContext
    {

        public MyPlannerDbContext(DbContextOptions<MyPlannerDbContext> options) : base(options)
        {
        }

        // These DbSets of the classes in the Model folder will be the tables of the database
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Branch> Branches { get; set; }

        public DbSet<Assignee> Assignees { get; set; }

        //defining the relations between tables and the constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Converts the Status enum to string for the database and back to enum when reading the value.
            modelBuilder.Entity<SubTask>().Property(s => s.Status)
                .HasConversion( s => s.ToString(), 
                s => (Status)Enum.Parse(typeof(Status), s));

            modelBuilder.Entity<Branch>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<Branch>().HasMany(b => b.Subtasks).WithOne(s => s.Branch).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Project>().HasMany(p => p.Branches).WithOne(b => b.Project).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assignee>().HasMany(a => a.SubTasks).WithMany(s => s.Assignees);
            modelBuilder.Entity<Assignee>().HasMany(a => a.Projects).WithMany(s => s.Assignees);

        }

    }
}
