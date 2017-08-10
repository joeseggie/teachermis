using Microsoft.EntityFrameworkCore;

namespace EastSeat.TeacherMIS.Web.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<T>(this ModelBuilder modelBuilder, DbEntityConfiguration<T> configuration) where T : class
        {
            modelBuilder.Entity<T>(configuration.Configure);
        }
    }
}