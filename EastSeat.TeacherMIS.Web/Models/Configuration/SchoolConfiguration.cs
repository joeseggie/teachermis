using System;
using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class SchoolConfiguration : DbEntityConfiguration<School>
    {
        public override void Configure(EntityTypeBuilder<School> entity)
        {
            entity.HasKey(s => s.SchoolId);
            entity.Property(s => s.SchoolId)
                .ValueGeneratedOnAdd();

            entity.Property(s => s.Name)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(200)
                .ForSqlServerHasColumnType("varchar(200)");


            entity.HasOne(s => s.District)
                .WithMany(d => d.Schools)
                .HasForeignKey(s => s.DistrictId);

            entity.Property(s => s.RowVersion)
                .IsRequired(true)
                .IsRowVersion();

            entity.ToTable("School");
        }
    }
}