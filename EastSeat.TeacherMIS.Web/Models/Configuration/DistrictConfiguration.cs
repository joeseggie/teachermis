using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class DistrictConfiguration : DbEntityConfiguration<District>
    {
        public override void Configure(EntityTypeBuilder<District> entity)
        {
            entity.HasKey(d => d.DistrictId);
            entity.Property(d => d.DistrictId)
                .ValueGeneratedOnAdd();

            entity.Property(d => d.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(false)
                .ForSqlServerHasColumnType("varchar(50)");

            entity.Property(d => d.RowVersion)
            .IsRequired()
            .IsRowVersion();

            entity.ToTable("District");
        }
    }
}