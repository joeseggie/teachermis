using System;
using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class SubjectCategoryConfiguration : DbEntityConfiguration<SubjectCategory>
    {
        public override void Configure(EntityTypeBuilder<SubjectCategory> entity)
        {
            entity.HasKey(c => c.SubjectCategoryId);
            entity.Property(c => c.SubjectCategoryId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(new string[] {"Stub"});

            entity.Property(c => c.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50)
                .ForSqlServerHasColumnType("varchar(50)");

            entity.Property(c => c.Stub)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50)
                .ForSqlServerHasColumnType("varchar(50)");

            entity.Property(c => c.RowVersion)
                .IsRequired()
                .IsRowVersion();

            entity.ToTable("SubjectCategory");
        }
    }
}
