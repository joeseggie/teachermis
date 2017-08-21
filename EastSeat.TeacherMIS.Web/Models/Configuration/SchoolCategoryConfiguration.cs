using EastSeat.TeacherMIS.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EastSeat.TeacherMIS.Web.Models.Configuration
{
    public class SchoolCategoryConfiguration : DbEntityConfiguration<SchoolCategory>
    {
        public override void Configure(EntityTypeBuilder<SchoolCategory> entity)
        {
            entity.HasKey(c => c.SchoolCategoryId);
            entity.Property(c => c.SchoolCategoryId)
                .ValueGeneratedOnAdd();

            entity.Property(c => c.Description)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50)
                .ForSqlServerHasColumnType("varchar(50)");

            entity.Property(c => c.RowVersion)
            .IsRequired()
            .IsRowVersion();
        }
    }
}