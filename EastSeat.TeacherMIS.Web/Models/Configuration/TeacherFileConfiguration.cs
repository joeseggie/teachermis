using System;
using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class TeacherFileConfiguration : DbEntityConfiguration<TeacherFile>
    {
        public override void Configure(EntityTypeBuilder<TeacherFile> entity)
        {
            entity.HasKey(f => f.TeacherFileId);
            entity.Property(f => f.TeacherFileId)
                .ValueGeneratedOnAdd();

            entity.Property(f => f.RecordDate)
                .IsRequired()
                .ForSqlServerHasColumnType("date");

            entity.Property(f => f.Details)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(2000)
                .ForSqlServerHasColumnType("varchar(2000)");

            entity.Property(f => f.RowVersion)
                .IsRequired()
                .IsRowVersion();
            
            entity.HasOne(f => f.Teacher)
                .WithMany(t => t.TeacherFiles)
                .HasForeignKey(f => f.TeacherId);

            entity.ToTable("TeacherFile");
        }
    }
}