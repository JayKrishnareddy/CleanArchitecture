using DataAccessLayer.EntityMappers;
using DataAccessLayer.Models;
using DataAccessLayer.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.ApplicationDbContext
{
    public partial class CFCDbContext : DbContext
    {
        public CFCDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<UserRoles> userRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new BranchMap());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
