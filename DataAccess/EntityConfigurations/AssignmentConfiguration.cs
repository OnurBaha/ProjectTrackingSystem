using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignments");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("Id");
            builder.Property(a => a.ProjectId).IsRequired();
            builder.Property(a => a.Title).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.Property(a => a.CreationDate).IsRequired();
            builder.Property(a => a.Status).IsRequired().HasMaxLength(50);

            builder.HasOne(a => a.Project)
                   .WithMany(p => p.Assignments)
                   .HasForeignKey(a => a.ProjectId);

            builder.HasQueryFilter(op => !op.DeletedDate.HasValue);
        }
    }
}
