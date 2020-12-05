using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.SeedData
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Data for Admin Roles
            modelBuilder.Entity<UserRoles>().HasData(
                new UserRoles { RoleId = 1,RoleName = "SuperAdmin",IsActive = true  },
                new UserRoles { RoleId = 2,RoleName = "Admin",IsActive = true  }
            );
        }
    }
}
