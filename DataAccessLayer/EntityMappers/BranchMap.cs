using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityMappers
{
    public class BranchMap : IEntityTypeConfiguration<Branches>
    {
        public void Configure(EntityTypeBuilder<Branches> builder)
        {
            builder.ToTable("branches");
            builder.HasKey(x => x.BranchId)
                   .HasName("pk_branch_id");
            builder.Property(x => x.BranchId)
                  .ValueGeneratedOnAdd()
                  .HasColumnName("branch_id")
                  .HasColumnType("INT");
            builder.Property(x => x.BranchName)
                  .HasColumnName("branch_name")
                  .HasColumnType("NVARCHAR(100)")
                  .IsRequired();
            builder.Property(x => x.BranchManager)
                 .HasColumnName("branch_manager")
                  .HasColumnType("NVARCHAR(100)")
                  .IsRequired();
            builder.Property(x => x.BranchLocation)
                 .HasColumnName("branch_location")
                  .HasColumnType("NVARCHAR(100)")
                  .IsRequired();
            builder.Property(x => x.BranchNumber)
                 .HasColumnName("branch_number")
                  .HasColumnType("BIGINT")
                  .IsRequired();
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
