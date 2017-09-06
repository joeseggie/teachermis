using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    internal class PositionConfiguration : DbEntityConfiguration<Position>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Position> entity)
        {
            entity.HasKey(p => p.PositionId);
            entity.Property(p => p.PositionId)
                .ValueGeneratedOnAdd();

            entity.Property(p => p.Name)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(256)
                .ForSqlServerHasColumnType("varchar(256)");

            entity.Property(p => p.RowVersion)
                .IsRequired()
                .IsRowVersion();

            entity.ToTable("Position");
        }
    }
}