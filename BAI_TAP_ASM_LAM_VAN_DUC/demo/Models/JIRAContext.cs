using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace demo.Models
{
    public partial class JIRAContext : DbContext
    {
        public JIRAContext()
        {
        }

        public JIRAContext(DbContextOptions<JIRAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PhanQuyenProject> PhanQuyenProjects { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectJob> ProjectJobs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=JIRA;User Id=sa;Password=123456a@;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<PhanQuyenProject>(entity =>
            {
                entity.ToTable("PHAN_QUYEN_PROJECT");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("PROJECT");

                entity.HasIndex(e => e.ProjectKey, "UQ__PROJECT__C048AC95F56A857D")
                    .IsUnique();

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.ProjectKey)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectLead)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("projectLead");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("projectName");

                entity.Property(e => e.UserCrate)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("userCrate");
            });

            modelBuilder.Entity<ProjectJob>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__TASK__7C6949B18C0D22F4");

                entity.ToTable("PROJECT_JOB");

                entity.Property(e => e.DeadLineTask).HasColumnType("date");

                entity.Property(e => e.DescriptionTask).HasColumnType("ntext");

                entity.Property(e => e.LevelTask)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PriorityTask)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ProjectKey)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.StatusTask)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'To do')");

                entity.Property(e => e.TaskCreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TaskUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.TitleTask)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.TypeTask).HasMaxLength(64);

                entity.Property(e => e.UserCreate)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserImplement)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__USERS__F3DBC573FE346B55");

                entity.ToTable("USERS");

                entity.Property(e => e.Username)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(256)
                    .HasColumnName("avatar");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("fullName");

                entity.Property(e => e.NgayCapNhat)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayCapNhat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayTao")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
