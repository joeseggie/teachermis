using System;
using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class TeacherConfiguration : DbEntityConfiguration<Teacher>
    {
        public override void Configure(EntityTypeBuilder<Teacher> entity)
        {
            entity.HasKey(t => t.TeacherId);
            entity.Property(t => t.TeacherId)
                .ValueGeneratedOnAdd();

            entity.HasOne(t => t.School)
                .WithMany(s => s.Teachers)
                .HasForeignKey(t => t.SchoolId);

            entity.Property(t => t.Fullname)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50)
                .ForSqlServerHasColumnType("varchar(50)");

            entity.Property(t => t.Gender)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(10)
                .ForSqlServerHasColumnType("char(10)");

            entity.Property(t => t.UtsFileNumber)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20)
                .ForSqlServerHasColumnType("varchar(20)");

            entity.Property(t => t.DateOfBirth)
                .IsRequired()
                .ForSqlServerHasColumnType("date");

            entity.Property(t => t.RegistrationNumber)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20)
                .ForSqlServerHasColumnType("varchar(20)");

            entity.Property(t => t.IppsNumber)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20)
                .ForSqlServerHasColumnType("varchar(20)");

            entity.Property(t => t.FirstProbationAppDate)
                .IsRequired()
                .ForSqlServerHasColumnType("date");

            entity.Property(t => t.ProbationAppDesignation)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100)
                .ForSqlServerHasColumnType("varchar(100)");

            entity.Property(t => t.FirstAppEscMinute)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200)
                .ForSqlServerHasColumnType("varchar(200)");            

            entity.Property(t => t.ConfirmationEscMinute)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200)
                .ForSqlServerHasColumnType("varchar(200)");

            entity.Property(t => t.CurrentPosition)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100)
                .ForSqlServerHasColumnType("varchar(100)");

            entity.Property(t => t.CurrentPositionAppMinute)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200)
                .ForSqlServerHasColumnType("varchar(200)");

            entity.Property(t => t.CurrentPositionPostingDate)
                .IsRequired()
                .ForSqlServerHasColumnType("date");

            entity.Property(t => t.RowVersion)
                .IsRequired()
                .IsRowVersion();
        }
    }
}