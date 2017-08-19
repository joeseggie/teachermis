using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EastSeat.TeacherMIS.Web.Data;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170819124134_ChangeTeacherFileRelation")]
    partial class ChangeTeacherFileRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.Headmaster", b =>
                {
                    b.Property<Guid>("HeadmasterId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("SchoolId");

                    b.Property<Guid?>("TeacherId");

                    b.HasKey("HeadmasterId");

                    b.HasIndex("SchoolId");

                    b.HasIndex("TeacherId")
                        .IsUnique();

                    b.HasIndex("TeacherId", "SchoolId");

                    b.ToTable("Headmaster");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.School", b =>
                {
                    b.Property<Guid>("SchoolId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(200)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("SchoolId");

                    b.ToTable("School");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.Subject", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(200)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid?>("SubjectCategoryId");

                    b.HasKey("SubjectId");

                    b.HasIndex("SubjectCategoryId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.SubjectCategory", b =>
                {
                    b.Property<Guid>("SubjectCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(50)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Stub")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(50)");

                    b.HasKey("SubjectCategoryId");

                    b.HasIndex("Stub");

                    b.ToTable("SubjectCategory");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.SubjectTaught", b =>
                {
                    b.Property<Guid>("SubjectTaughtId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("SubjectId");

                    b.Property<Guid>("TeacherId");

                    b.HasKey("SubjectTaughtId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("SubjectTaught");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.Teacher", b =>
                {
                    b.Property<Guid>("TeacherId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConfirmationEscMinute")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(200)");

                    b.Property<string>("CurrentPosition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(100)");

                    b.Property<string>("CurrentPositionAppMinute")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(200)");

                    b.Property<DateTime>("CurrentPositionPostingDate")
                        .HasAnnotation("SqlServer:ColumnType", "date");

                    b.Property<DateTime>("DateOfBirth")
                        .HasAnnotation("SqlServer:ColumnType", "date");

                    b.Property<string>("FirstAppEscMinute")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(200)");

                    b.Property<DateTime>("FirstProbationAppDate")
                        .HasAnnotation("SqlServer:ColumnType", "date");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "char(10)");

                    b.Property<string>("IppsNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(20)");

                    b.Property<string>("ProbationAppDesignation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(100)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(20)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("SchoolId");

                    b.Property<string>("UtsFileNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(20)");

                    b.HasKey("TeacherId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.TeacherFile", b =>
                {
                    b.Property<Guid>("TeacherFileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(2000)");

                    b.Property<DateTime>("RecordDate")
                        .HasAnnotation("SqlServer:ColumnType", "date");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("TeacherId");

                    b.HasKey("TeacherFileId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherFile");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.Headmaster", b =>
                {
                    b.HasOne("EastSeat.TeacherMIS.Web.Models.School", "School")
                        .WithMany("Headmasters")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EastSeat.TeacherMIS.Web.Models.Teacher", "Teacher")
                        .WithOne("Headmaster")
                        .HasForeignKey("EastSeat.TeacherMIS.Web.Models.Headmaster", "TeacherId");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.Subject", b =>
                {
                    b.HasOne("EastSeat.TeacherMIS.Web.Models.SubjectCategory", "SubjectCategory")
                        .WithMany("Subjects")
                        .HasForeignKey("SubjectCategoryId");
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.SubjectTaught", b =>
                {
                    b.HasOne("EastSeat.TeacherMIS.Web.Models.Subject", "Subject")
                        .WithMany("SubjectsTaught")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EastSeat.TeacherMIS.Web.Models.Teacher", "Teacher")
                        .WithMany("SubjectsTaught")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.Teacher", b =>
                {
                    b.HasOne("EastSeat.TeacherMIS.Web.Models.School", "School")
                        .WithMany("Teachers")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EastSeat.TeacherMIS.Web.Models.TeacherFile", b =>
                {
                    b.HasOne("EastSeat.TeacherMIS.Web.Models.Teacher", "Teacher")
                        .WithMany("TeacherFiles")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EastSeat.TeacherMIS.Web.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EastSeat.TeacherMIS.Web.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EastSeat.TeacherMIS.Web.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
