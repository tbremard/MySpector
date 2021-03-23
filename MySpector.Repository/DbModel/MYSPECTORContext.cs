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
        public virtual DbSet<result_history> result_history { get; set; }
        public virtual DbSet<target> target { get; set; }
        public virtual DbSet<target_http> target_http { get; set; }
        public virtual DbSet<target_sql> target_sql { get; set; }
        public virtual DbSet<target_type> target_type { get; set; }
        public virtual DbSet<trox> trox { get; set; }
        public virtual DbSet<trox_closure> trox_closure { get; set; }
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

                entity.Property(e => e.ID_TROX).HasColumnType("int unsigned");

                entity.HasOne(d => d.ID_TROXNavigation)
                    .WithMany(p => p.checker_def)
                    .HasForeignKey(d => d.ID_TROX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_TROX3");
            });

            modelBuilder.Entity<checker_type>(entity =>
            {
                entity.HasKey(e => new { e.ID_TYPE, e.NAME })
                    .HasName("PRIMARY");

                entity.Property(e => e.ID_TYPE).ValueGeneratedOnAdd();

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

                entity.Property(e => e.ID_TROX).HasColumnType("int unsigned");

                entity.HasOne(d => d.ID_TROXNavigation)
                    .WithMany(p => p.notify_def)
                    .HasForeignKey(d => d.ID_TROX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_TROX4");
            });

            modelBuilder.Entity<notify_type>(entity =>
            {
                entity.HasKey(e => new { e.ID_TYPE, e.NAME })
                    .HasName("PRIMARY");

                entity.Property(e => e.ID_TYPE).ValueGeneratedOnAdd();

                entity.Property(e => e.NAME).HasMaxLength(50);
            });

            modelBuilder.Entity<result_history>(entity =>
            {
                entity.HasKey(e => e.ID_RESULT)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_TROX)
                    .HasName("FK_ID_TROX_789_idx");

                entity.Property(e => e.ID_RESULT).HasColumnType("int unsigned");

                entity.Property(e => e.ID_TROX).HasColumnType("int unsigned");

                entity.Property(e => e.IN_DATA).HasMaxLength(100);

                entity.Property(e => e.LATENCY_MS).HasColumnType("int unsigned");

                entity.Property(e => e.OUT_NUMBER).HasColumnType("decimal(20,10)");

                entity.Property(e => e.OUT_TEXT).HasMaxLength(100);

                entity.HasOne(d => d.ID_TROXNavigation)
                    .WithMany(p => p.result_history)
                    .HasForeignKey(d => d.ID_TROX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_TROX_789");
            });

            modelBuilder.Entity<target>(entity =>
            {
                entity.HasKey(e => e.ID_TARGET)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_TARGET_TYPE)
                    .HasName("FK_ID_WEB_TARGET_TYPE_idx");

                entity.Property(e => e.NAME).HasMaxLength(100);
            });

            modelBuilder.Entity<target_http>(entity =>
            {
                entity.HasKey(e => e.ID_TARGET)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_TARGET)
                    .HasName("FK_ID_WEB_TARGET_idx");

                entity.Property(e => e.CONTENT).HasMaxLength(1000);

                entity.Property(e => e.HEADERS).HasMaxLength(1000);

                entity.Property(e => e.METHOD)
                    .IsRequired()
                    .HasColumnType("enum('GET','POST','PUT','DELETE')")
                    .HasDefaultValueSql("'GET'");

                entity.Property(e => e.URI)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.VERSION)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("'1.1'");

                entity.HasOne(d => d.ID_TARGETNavigation)
                    .WithOne(p => p.target_http)
                    .HasForeignKey<target_http>(d => d.ID_TARGET)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_WEB_TARGET1");
            });

            modelBuilder.Entity<target_sql>(entity =>
            {
                entity.HasKey(e => e.ID_TARGET)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_TARGET)
                    .HasName("FK_ID_WEB_TARGET_idx");

                entity.Property(e => e.CONNECTION_STRING)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.PROVIDER)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.QUERY)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.ID_TARGETNavigation)
                    .WithOne(p => p.target_sql)
                    .HasForeignKey<target_sql>(d => d.ID_TARGET)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_WEB_TARGET2");
            });

            modelBuilder.Entity<target_type>(entity =>
            {
                entity.HasKey(e => new { e.ID_TYPE, e.NAME })
                    .HasName("PRIMARY");

                entity.Property(e => e.ID_TYPE).ValueGeneratedOnAdd();

                entity.Property(e => e.NAME).HasMaxLength(50);
            });

            modelBuilder.Entity<trox>(entity =>
            {
                entity.HasKey(e => e.ID_TROX)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ID_TARGET)
                    .HasName("FK_ID_WEB_TARGET_idx");

                entity.Property(e => e.ID_TROX).HasColumnType("int unsigned");

                entity.Property(e => e.ENABLED).HasDefaultValueSql("'1'");

                entity.Property(e => e.IS_DIRECTORY).HasDefaultValueSql("'0'");

                entity.Property(e => e.NAME)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ID_TARGETNavigation)
                    .WithMany(p => p.trox)
                    .HasForeignKey(d => d.ID_TARGET)
                    .HasConstraintName("FK_ID_WEB_TARGET");
            });

            modelBuilder.Entity<trox_closure>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.ID_CHILD)
                    .HasName("FK_ID_CHILD_idx");

                entity.HasIndex(e => e.ID_PARENT)
                    .HasName("FK_ID_PARENT_idx");

                entity.Property(e => e.ID_CHILD).HasColumnType("int unsigned");

                entity.Property(e => e.ID_PARENT).HasColumnType("int unsigned");

                entity.HasOne(d => d.ID_CHILDNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ID_CHILD)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_CHILD");

                entity.HasOne(d => d.ID_PARENTNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ID_PARENT)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_PARENT");
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

                entity.Property(e => e.ID_TROX).HasColumnType("int unsigned");

                entity.HasOne(d => d.ID_TROXNavigation)
                    .WithMany(p => p.xtrax_def)
                    .HasForeignKey(d => d.ID_TROX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_TROX2");
            });

            modelBuilder.Entity<xtrax_type>(entity =>
            {
                entity.HasKey(e => new { e.ID_TYPE, e.NAME })
                    .HasName("PRIMARY");

                entity.Property(e => e.ID_TYPE).ValueGeneratedOnAdd();

                entity.Property(e => e.NAME).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
