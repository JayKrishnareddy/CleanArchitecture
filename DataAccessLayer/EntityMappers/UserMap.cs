using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityMappers
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.UserId)
                   .HasName("pk_user");
            builder.Property(x => x.UserId)
                   .ValueGeneratedOnAdd()
                   .HasColumnName("user_id")
                   .HasColumnType("INT");
            builder.Property(x => x.FirstName)
                   .HasColumnName("first_name")
                   .HasColumnType("NVARCHAR(100)")
                   .IsRequired();
            builder.Property(x => x.LastName)
                   .HasColumnName("last_name")
                   .HasColumnType("NVARCHAR(100)")
                   .IsRequired();
            builder.Property(x => x.PhoneNumber)
                   .HasColumnName("phone_number")
                   .HasColumnType("BIGINT")
                   .IsRequired();
            builder.Property(x => x.Password)
                  .HasColumnName("password")
                  .HasColumnType("NVARCHAR(100)")
                  .IsRequired();
            builder.Property(x => x.Email)
                  .HasColumnName("email")
                  .HasColumnType("NVARCHAR(100)")
                  .IsRequired();
            builder.HasOne(x => x.UserRoles)
                   .WithOne(x => x.User)
                   .HasForeignKey<User>(x => x.RoleId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_user_user_roles");
            builder.Property(x => x.CreatedDate)
                 .HasColumnName("created_date")
                 .HasColumnType("DATETIME");
            builder.Property(x => x.ModifiedDate)
                 .HasColumnName("modified_date")
                 .HasColumnType("DATETIME");
            builder.Property(x => x.IsActive)
                 .HasColumnName("is_active")
                 .HasColumnType("BIT");

        }
    }
}
