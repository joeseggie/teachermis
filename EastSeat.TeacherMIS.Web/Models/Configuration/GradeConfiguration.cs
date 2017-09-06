using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class GradeConfiguration : DbEntityConfiguration<Grade>
    {
        public override void Configure(EntityTypeBuilder<Grade> entity)
        {
            entity.HasKey(g => g.GradeId);
            entity.Property(g => g.GradeId)
                .ValueGeneratedOnAdd();

            entity.Property(g => g.Name)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(256)
                .ForSqlServerHasColumnType("varchar(256)");

            entity.Property(g => g.RowVersion)
                .IsRequired()
                .IsRowVersion();
            
            entity.ToTable("Grade");
        }
    }
}