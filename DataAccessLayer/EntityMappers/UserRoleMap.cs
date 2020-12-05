using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityMappers
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.ToTable("user_role");
            builder.HasKey(x => x.RoleId)
                   .HasName("pk_user_role");
            builder.Property(x => x.RoleId)
                  .ValueGeneratedOnAdd()
                  .HasColumnName("role_id")
                  .HasColumnType("INT");
            builder.Property(x => x.RoleName)
                  .HasColumnName("role_name")
                  .HasColumnType("NVARCHAR(100)")
                  .IsRequired();
            builder.Property(x => x.IsActive)
                 .HasColumnName("is_active")
                 .HasColumnType("BIT")
                 .IsRequired();
        }
    }
}
