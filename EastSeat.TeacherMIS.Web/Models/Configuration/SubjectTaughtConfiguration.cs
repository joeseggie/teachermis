using System;
using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class SubjectTaughtConfiguration : DbEntityConfiguration<SubjectTaught>
    {
        public override void Configure(EntityTypeBuilder<SubjectTaught> entity)
        {
            entity.HasKey(s => s.SubjectTaughtId);
            entity.Property(s => s.SubjectTaughtId)
                .ValueGeneratedOnAdd();

            entity.HasOne(s => s.Teacher)
                .WithMany(t => t.SubjectsTaught)
                .HasForeignKey(s => s.TeacherId);

            entity.HasOne(s => s.Subject)
                .WithMany(j => j.SubjectsTaught)
                .HasForeignKey(s => s.SubjectId);

            entity.Property(s => s.RowVersion)
                .IsRequired()
                .IsRowVersion();

            entity.ToTable("SubjectTaught");
        }
    }
}