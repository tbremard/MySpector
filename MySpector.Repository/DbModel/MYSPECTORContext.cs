using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MySpector.Repo.DbModel
{
    public partial class MYSPECTORContext : DbContext
    {
        public MYSPECTORContext()
        {
        }

        public MYSPECTORContext(DbContextOptions<MYSPECTORContext> options)
            : base(options)
        {
        }

        public virtual DbSet<checker_def> checker_def { get; set; }
        public virtual DbSet<checker_type> checker_type { get; set; }
        public virtual DbSet<notify_def> notify_def { get; set; }
        public virtual DbSet<notify_type> notify_type { get; set; }
        public virtual DbSet<tjson> tjson { get; set; }
        public virtual DbSet<trox> trox { get; set; }
        public virtual DbSet<trox_closure> trox_closure { get; set; }
        public virtual DbSet<web_target> web_target { get; set; }
        public virtual DbSet<web_target_http> web_target_http { get; set; }
        public virtual DbSet<web_target_sql> web_target_sql { get; set; }
        public virtual DbSet<web_target_type> web_target_type { get; set; }
        public virtual DbSet<xtrax_def> xtrax_def { get; set; }
        public virtual DbSet<xtrax_type> xtrax_type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=localhost;Database=MYSPECTOR;Uid=root; Pwd=123456789;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<checker_def>(entity =>
            {
                entity.HasKey(e => e.ID_CHECKER_DEF)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_CHECKER_TYPE)
                    .HasName("FK_ID_XTRAX_TYPE_idx");

                entity.HasIndex(e => e.ID_TROX)
                    .HasName("FK_ID_PIPELINE_idx");

                entity.Property(e => e.ARG).HasMaxLength(100);

                entity.HasOne(d => d.ID_CHECKER_TYPENavigation)
                    .WithMany(p => p.checker_def)
                    .HasForeignKey(d => d.ID_CHECKER_TYPE)
                    .HasConstraintName("FK_ID_CHECKER_TYPE");

                entity.HasOne(d => d.ID_TROXNavigation)
                    .WithMany(p => p.checker_def)
                    .HasForeignKey(d => d.ID_TROX)
                    .HasConstraintName("FK_ID_TROX3");
            });

            modelBuilder.Entity<checker_type>(entity =>
            {
                entity.HasKey(e => e.ID_CHECKER_TYPE)
                    .HasName("PRIMARY");

                entity.Property(e => e.NAME).HasMaxLength(50);
            });

            modelBuilder.Entity<notify_def>(entity =>
            {
                entity.HasKey(e => e.ID_NOTIFY_DEF)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_NOTIFY_TYPE)
                    .HasName("FK_ID_NOTIFY_TYPE_idx");

                entity.HasIndex(e => e.ID_TROX)
                    .HasName("FK_ID_PIPELINE_idx");

                entity.Property(e => e.ARG).HasMaxLength(100);

                entity.HasOne(d => d.ID_NOTIFY_TYPENavigation)
                    .WithMany(p => p.notify_def)
                    .HasForeignKey(d => d.ID_NOTIFY_TYPE)
                    .HasConstraintName("FK_ID_NOTIFY_TYPE");

                entity.HasOne(d => d.ID_TROXNavigation)
                    .WithMany(p => p.notify_def)
                    .HasForeignKey(d => d.ID_TROX)
                    .HasConstraintName("FK_ID_TROX4");
            });

            modelBuilder.Entity<notify_type>(entity =>
            {
                entity.HasKey(e => e.ID_NOTIFY_TYPE)
                    .HasName("PRIMARY");

                entity.Property(e => e.NAME).HasMaxLength(50);
            });

            modelBuilder.Entity<tjson>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.param).HasColumnType("json");
            });

            modelBuilder.Entity<trox>(entity =>
            {
                entity.HasKey(e => e.ID_TROX)
                    .HasName("PRIMARY");

                entity.Property(e => e.NAME).HasMaxLength(100);
            });

            modelBuilder.Entity<trox_closure>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.ID_CHILD)
                    .HasName("FK_ID_CHILD_idx");

                entity.HasIndex(e => e.ID_PARENT)
                    .HasName("FK_ID_PARENT_idx");

                entity.HasOne(d => d.ID_CHILDNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ID_CHILD)
                    .HasConstraintName("FK_ID_CHILD");

                entity.HasOne(d => d.ID_PARENTNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ID_PARENT)
                    .HasConstraintName("FK_ID_PARENT");
            });

            modelBuilder.Entity<web_target>(entity =>
            {
                entity.HasKey(e => e.ID_WEB_TARGET)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_TROX)
                    .HasName("FK_ID_TROX_idx");

                entity.HasIndex(e => e.ID_WEB_TARGET_TYPE)
                    .HasName("FK_ID_WEB_TARGET_TYPE_idx");

                entity.HasOne(d => d.ID_TROXNavigation)
                    .WithMany(p => p.web_target)
                    .HasForeignKey(d => d.ID_TROX)
                    .HasConstraintName("FK_ID_TROX1");

                entity.HasOne(d => d.ID_WEB_TARGET_TYPENavigation)
                    .WithMany(p => p.web_target)
                    .HasForeignKey(d => d.ID_WEB_TARGET_TYPE)
                    .HasConstraintName("FK_ID_WEB_TARGET_TYPE");
            });

            modelBuilder.Entity<web_target_http>(entity =>
            {
                entity.HasKey(e => e.ID_WEB_TARGET)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_WEB_TARGET)
                    .HasName("FK_ID_WEB_TARGET_idx");

                entity.Property(e => e.CONTENT).HasMaxLength(1000);

                entity.Property(e => e.HEADERS).HasMaxLength(1000);

                entity.Property(e => e.METHOD).HasColumnType("enum('GET','POST','PUT','DELETE')");

                entity.Property(e => e.URI).HasMaxLength(200);

                entity.Property(e => e.VERSION).HasMaxLength(5);

                entity.HasOne(d => d.ID_WEB_TARGETNavigation)
                    .WithOne(p => p.web_target_http)
                    .HasForeignKey<web_target_http>(d => d.ID_WEB_TARGET)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_WEB_TARGET1");
            });

            modelBuilder.Entity<web_target_sql>(entity =>
            {
                entity.HasKey(e => e.ID_WEB_TARGET)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_WEB_TARGET)
                    .HasName("FK_ID_WEB_TARGET_idx");

                entity.Property(e => e.CONNECTION_STRING).HasMaxLength(1000);

                entity.Property(e => e.QUERY).HasMaxLength(1000);

                entity.HasOne(d => d.ID_WEB_TARGETNavigation)
                    .WithOne(p => p.web_target_sql)
                    .HasForeignKey<web_target_sql>(d => d.ID_WEB_TARGET)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_WEB_TARGET2");
            });

            modelBuilder.Entity<web_target_type>(entity =>
            {
                entity.HasKey(e => e.ID_WEB_TARGET_TYPE)
                    .HasName("PRIMARY");

                entity.Property(e => e.NAME).HasMaxLength(50);
            });

            modelBuilder.Entity<xtrax_def>(entity =>
            {
                entity.HasKey(e => e.ID_XTRAX_DEF)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_TROX)
                    .HasName("FK_ID_PIPELINE_idx");

                entity.HasIndex(e => e.ID_XTRAX_TYPE)
                    .HasName("FK_ID_XTRAX_TYPE_idx");

                entity.Property(e => e.ARG).HasMaxLength(100);

                entity.HasOne(d => d.ID_TROXNavigation)
                    .WithMany(p => p.xtrax_def)
                    .HasForeignKey(d => d.ID_TROX)
                    .HasConstraintName("FK_ID_TROX2");

                entity.HasOne(d => d.ID_XTRAX_TYPENavigation)
                    .WithMany(p => p.xtrax_def)
                    .HasForeignKey(d => d.ID_XTRAX_TYPE)
                    .HasConstraintName("FK_ID_XTRAX_TYPE");
            });

            modelBuilder.Entity<xtrax_type>(entity =>
            {
                entity.HasKey(e => e.ID_XTRAX_TYPE)
                    .HasName("PRIMARY");

                entity.Property(e => e.NAME).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
