using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MySpector.Repo.Model
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

        public virtual DbSet<CheckerDef> CheckerDef { get; set; }
        public virtual DbSet<CheckerType> CheckerType { get; set; }
        public virtual DbSet<NotifyDef> NotifyDef { get; set; }
        public virtual DbSet<NotifyType> NotifyType { get; set; }
        public virtual DbSet<Tjson> Tjson { get; set; }
        public virtual DbSet<Trox> Trox { get; set; }
        public virtual DbSet<TroxClosure> TroxClosure { get; set; }
        public virtual DbSet<WebTarget> WebTarget { get; set; }
        public virtual DbSet<WebTargetHttp> WebTargetHttp { get; set; }
        public virtual DbSet<WebTargetSql> WebTargetSql { get; set; }
        public virtual DbSet<WebTargetType> WebTargetType { get; set; }
        public virtual DbSet<XtraxDef> XtraxDef { get; set; }
        public virtual DbSet<XtraxType> XtraxType { get; set; }

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
            modelBuilder.Entity<CheckerDef>(entity =>
            {
                entity.HasKey(e => e.IdCheckerDef)
                    .HasName("PRIMARY");

                entity.ToTable("checker_def");

                entity.HasIndex(e => e.IdCheckerType)
                    .HasName("FK_ID_XTRAX_TYPE_idx");

                entity.HasIndex(e => e.IdTrox)
                    .HasName("FK_ID_PIPELINE_idx");

                entity.Property(e => e.IdCheckerDef).HasColumnName("ID_CHECKER_DEF");

                entity.Property(e => e.Arg)
                    .HasColumnName("ARG")
                    .HasMaxLength(100);

                entity.Property(e => e.IdCheckerType).HasColumnName("ID_CHECKER_TYPE");

                entity.Property(e => e.IdTrox).HasColumnName("ID_TROX");

                entity.Property(e => e.Order).HasColumnName("ORDER");

                entity.HasOne(d => d.IdCheckerTypeNavigation)
                    .WithMany(p => p.CheckerDef)
                    .HasForeignKey(d => d.IdCheckerType)
                    .HasConstraintName("FK_ID_CHECKER_TYPE");

                entity.HasOne(d => d.IdTroxNavigation)
                    .WithMany(p => p.CheckerDef)
                    .HasForeignKey(d => d.IdTrox)
                    .HasConstraintName("FK_ID_TROX3");
            });

            modelBuilder.Entity<CheckerType>(entity =>
            {
                entity.HasKey(e => e.IdCheckerType)
                    .HasName("PRIMARY");

                entity.ToTable("checker_type");

                entity.Property(e => e.IdCheckerType).HasColumnName("ID_CHECKER_TYPE");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NotifyDef>(entity =>
            {
                entity.HasKey(e => e.IdNotifyDef)
                    .HasName("PRIMARY");

                entity.ToTable("notify_def");

                entity.HasIndex(e => e.IdNotifyType)
                    .HasName("FK_ID_NOTIFY_TYPE_idx");

                entity.HasIndex(e => e.IdTrox)
                    .HasName("FK_ID_PIPELINE_idx");

                entity.Property(e => e.IdNotifyDef).HasColumnName("ID_NOTIFY_DEF");

                entity.Property(e => e.Arg)
                    .HasColumnName("ARG")
                    .HasMaxLength(100);

                entity.Property(e => e.IdNotifyType).HasColumnName("ID_NOTIFY_TYPE");

                entity.Property(e => e.IdTrox).HasColumnName("ID_TROX");

                entity.Property(e => e.Order).HasColumnName("ORDER");

                entity.HasOne(d => d.IdNotifyTypeNavigation)
                    .WithMany(p => p.NotifyDef)
                    .HasForeignKey(d => d.IdNotifyType)
                    .HasConstraintName("FK_ID_NOTIFY_TYPE");

                entity.HasOne(d => d.IdTroxNavigation)
                    .WithMany(p => p.NotifyDef)
                    .HasForeignKey(d => d.IdTrox)
                    .HasConstraintName("FK_ID_TROX4");
            });

            modelBuilder.Entity<NotifyType>(entity =>
            {
                entity.HasKey(e => e.IdNotifyType)
                    .HasName("PRIMARY");

                entity.ToTable("notify_type");

                entity.Property(e => e.IdNotifyType).HasColumnName("ID_NOTIFY_TYPE");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tjson>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tjson");

                entity.Property(e => e.Param)
                    .HasColumnName("param")
                    .HasColumnType("json");
            });

            modelBuilder.Entity<Trox>(entity =>
            {
                entity.HasKey(e => e.IdTrox)
                    .HasName("PRIMARY");

                entity.ToTable("trox");

                entity.Property(e => e.IdTrox).HasColumnName("ID_TROX");

                entity.Property(e => e.Enabled).HasColumnName("ENABLED");

                entity.Property(e => e.IsDirectory).HasColumnName("IS_DIRECTORY");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TroxClosure>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("trox_closure");

                entity.HasIndex(e => e.IdChild)
                    .HasName("FK_ID_CHILD_idx");

                entity.HasIndex(e => e.IdParent)
                    .HasName("FK_ID_PARENT_idx");

                entity.Property(e => e.IdChild).HasColumnName("ID_CHILD");

                entity.Property(e => e.IdParent).HasColumnName("ID_PARENT");

                entity.HasOne(d => d.IdChildNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdChild)
                    .HasConstraintName("FK_ID_CHILD");

                entity.HasOne(d => d.IdParentNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdParent)
                    .HasConstraintName("FK_ID_PARENT");
            });

            modelBuilder.Entity<WebTarget>(entity =>
            {
                entity.HasKey(e => e.IdWebTarget)
                    .HasName("PRIMARY");

                entity.ToTable("web_target");

                entity.HasIndex(e => e.IdTrox)
                    .HasName("FK_ID_TROX_idx");

                entity.HasIndex(e => e.IdWebTargetType)
                    .HasName("FK_ID_WEB_TARGET_TYPE_idx");

                entity.Property(e => e.IdWebTarget).HasColumnName("ID_WEB_TARGET");

                entity.Property(e => e.IdTrox).HasColumnName("ID_TROX");

                entity.Property(e => e.IdWebTargetType).HasColumnName("ID_WEB_TARGET_TYPE");

                entity.HasOne(d => d.IdTroxNavigation)
                    .WithMany(p => p.WebTarget)
                    .HasForeignKey(d => d.IdTrox)
                    .HasConstraintName("FK_ID_TROX1");

                entity.HasOne(d => d.IdWebTargetTypeNavigation)
                    .WithMany(p => p.WebTarget)
                    .HasForeignKey(d => d.IdWebTargetType)
                    .HasConstraintName("FK_ID_WEB_TARGET_TYPE");
            });

            modelBuilder.Entity<WebTargetHttp>(entity =>
            {
                entity.HasKey(e => e.IdWebTarget)
                    .HasName("PRIMARY");

                entity.ToTable("web_target_http");

                entity.HasIndex(e => e.IdWebTarget)
                    .HasName("FK_ID_WEB_TARGET_idx");

                entity.Property(e => e.IdWebTarget).HasColumnName("ID_WEB_TARGET");

                entity.Property(e => e.Content)
                    .HasColumnName("CONTENT")
                    .HasMaxLength(1000);

                entity.Property(e => e.Headers)
                    .HasColumnName("HEADERS")
                    .HasMaxLength(1000);

                entity.Property(e => e.Method)
                    .HasColumnName("METHOD")
                    .HasColumnType("enum('GET','POST','PUT','DELETE')");

                entity.Property(e => e.Uri)
                    .HasColumnName("URI")
                    .HasMaxLength(200);

                entity.Property(e => e.Version)
                    .HasColumnName("VERSION")
                    .HasMaxLength(5);

                entity.HasOne(d => d.IdWebTargetNavigation)
                    .WithOne(p => p.WebTargetHttp)
                    .HasForeignKey<WebTargetHttp>(d => d.IdWebTarget)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_WEB_TARGET1");
            });

            modelBuilder.Entity<WebTargetSql>(entity =>
            {
                entity.HasKey(e => e.IdWebTarget)
                    .HasName("PRIMARY");

                entity.ToTable("web_target_sql");

                entity.HasIndex(e => e.IdWebTarget)
                    .HasName("FK_ID_WEB_TARGET_idx");

                entity.Property(e => e.IdWebTarget).HasColumnName("ID_WEB_TARGET");

                entity.Property(e => e.ConnectionString)
                    .HasColumnName("CONNECTION_STRING")
                    .HasMaxLength(1000);

                entity.Property(e => e.Query)
                    .HasColumnName("QUERY")
                    .HasMaxLength(1000);

                entity.HasOne(d => d.IdWebTargetNavigation)
                    .WithOne(p => p.WebTargetSql)
                    .HasForeignKey<WebTargetSql>(d => d.IdWebTarget)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_WEB_TARGET2");
            });

            modelBuilder.Entity<WebTargetType>(entity =>
            {
                entity.HasKey(e => e.IdWebTargetType)
                    .HasName("PRIMARY");

                entity.ToTable("web_target_type");

                entity.Property(e => e.IdWebTargetType).HasColumnName("ID_WEB_TARGET_TYPE");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<XtraxDef>(entity =>
            {
                entity.HasKey(e => e.IdXtraxDef)
                    .HasName("PRIMARY");

                entity.ToTable("xtrax_def");

                entity.HasIndex(e => e.IdTrox)
                    .HasName("FK_ID_PIPELINE_idx");

                entity.HasIndex(e => e.IdXtraxType)
                    .HasName("FK_ID_XTRAX_TYPE_idx");

                entity.Property(e => e.IdXtraxDef).HasColumnName("ID_XTRAX_DEF");

                entity.Property(e => e.Arg)
                    .HasColumnName("ARG")
                    .HasMaxLength(100);

                entity.Property(e => e.IdTrox).HasColumnName("ID_TROX");

                entity.Property(e => e.IdXtraxType).HasColumnName("ID_XTRAX_TYPE");

                entity.Property(e => e.Order).HasColumnName("ORDER");

                entity.HasOne(d => d.IdTroxNavigation)
                    .WithMany(p => p.XtraxDef)
                    .HasForeignKey(d => d.IdTrox)
                    .HasConstraintName("FK_ID_TROX2");

                entity.HasOne(d => d.IdXtraxTypeNavigation)
                    .WithMany(p => p.XtraxDef)
                    .HasForeignKey(d => d.IdXtraxType)
                    .HasConstraintName("FK_ID_XTRAX_TYPE");
            });

            modelBuilder.Entity<XtraxType>(entity =>
            {
                entity.HasKey(e => e.IdXtraxType)
                    .HasName("PRIMARY");

                entity.ToTable("xtrax_type");

                entity.Property(e => e.IdXtraxType).HasColumnName("ID_XTRAX_TYPE");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
