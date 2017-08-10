using System;
using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class SubjectConfiguration : DbEntityConfiguration<Subject>
    {
        public override void Configure(EntityTypeBuilder<Subject> entity)
        {
            entity.HasKey(j => j.SubjectId);
            entity.Property(j => j.SubjectId)
                .ValueGeneratedOnAdd();

            entity.Property(j => j.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200)
                .ForSqlServerHasColumnType("varchar(200)");

            entity.Property(j => j.RowVersion)
                .IsRequired()
                .IsRowVersion();

            entity.ToTable("Subject");
        }
    }
}