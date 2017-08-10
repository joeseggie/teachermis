using System;
using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class HeadmasterConfiguration : DbEntityConfiguration<Headmaster>
    {
        public override void Configure(EntityTypeBuilder<Headmaster> entity)
        {
            entity.HasKey(h => h.HeadmasterId);
            entity.Property(h => h.HeadmasterId)
                .ValueGeneratedOnAdd();

            entity.HasIndex(new string[] {"TeacherId", "SchoolId"});

            entity.HasOne(h => h.School)
                .WithMany(s => s.Headmasters)
                .HasForeignKey(h => h.SchoolId);

            entity.HasOne(h => h.Teacher)
                .WithOne(t => t.Headmaster)
                .IsRequired(false);

            entity.Property(h => h.RowVersion)
                .IsRequired()
                .IsRowVersion();

            entity.ToTable("Headmaster");
        }
    }
}