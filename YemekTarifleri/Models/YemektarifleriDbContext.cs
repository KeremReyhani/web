using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Yemektarifleri.Models;

public partial class yemektarifleriDbContext : DbContext
{
    public yemektarifleriDbContext()
    {
    }

    public yemektarifleriDbContext(DbContextOptions<yemektarifleriDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kategoriler> Kategorilers { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<Menuler> Menulers { get; set; }

    public virtual DbSet<Sayfalar> Sayfalars { get; set; }

    public virtual DbSet<YemekTarifleri> YemekTarifleris { get; set; }

    public virtual DbSet<Yorumlar> Yorumlars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=KEREM-R-00\\SQLEXPRESS;Database=yemektarifleriDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kategoriler>(entity =>
        {
            entity.HasKey(e => e.KategoriId).HasName("PK__Kategori__1D9181BD4A2885ED");

            entity.ToTable("Kategoriler");

            entity.Property(e => e.KategoriId).HasColumnName("kategoriID");
            entity.Property(e => e.Aktif).HasColumnName("aktif");
            entity.Property(e => e.Kategoriadi)
                .HasMaxLength(50)
                .HasColumnName("kategoriadi");
            entity.Property(e => e.Silindi).HasColumnName("silindi");
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.KullaniciId).HasName("PK__Kullanic__848DC54B5E2AB8A3");

            entity.ToTable("Kullanicilar");

            entity.Property(e => e.KullaniciId).HasColumnName("kullaniciID");
            entity.Property(e => e.Adi)
                .HasMaxLength(20)
                .HasColumnName("adi");
            entity.Property(e => e.Aktif).HasColumnName("aktif");
            entity.Property(e => e.Eposta)
                .HasMaxLength(50)
                .HasColumnName("eposta");
            entity.Property(e => e.Parola)
                .HasMaxLength(50)
                .HasColumnName("parola");
            entity.Property(e => e.Silindi).HasColumnName("silindi");
            entity.Property(e => e.Soyadi)
                .HasMaxLength(20)
                .HasColumnName("soyadi");
            entity.Property(e => e.Telefon)
                .HasMaxLength(11)
                .HasColumnName("telefon");
            entity.Property(e => e.Yetki).HasColumnName("yetki");
        });

        modelBuilder.Entity<Menuler>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menuler__3B407E947CF8C469");

            entity.ToTable("Menuler");

            entity.Property(e => e.MenuId).HasColumnName("menuID");
            entity.Property(e => e.Aktif).HasColumnName("aktif");
            entity.Property(e => e.Baslik)
                .HasMaxLength(50)
                .HasColumnName("baslik");
            entity.Property(e => e.Link)
                .HasMaxLength(250)
                .HasColumnName("link");
            entity.Property(e => e.Silindi).HasColumnName("silindi");
            entity.Property(e => e.Sira).HasColumnName("sira");
            entity.Property(e => e.UstId).HasColumnName("ustID");
        });

        modelBuilder.Entity<Sayfalar>(entity =>
        {
            entity.HasKey(e => e.SayfaId).HasName("PK__Sayfalar__F81FC2B21D7182AA");

            entity.ToTable("Sayfalar");

            entity.Property(e => e.SayfaId).HasColumnName("sayfaID");
            entity.Property(e => e.Aktif).HasColumnName("aktif");
            entity.Property(e => e.Baslik)
                .HasMaxLength(250)
                .HasColumnName("baslik");
            entity.Property(e => e.Icerik)
                .HasColumnType("ntext")
                .HasColumnName("icerik");
            entity.Property(e => e.Silindi).HasColumnName("silindi");
        });

        modelBuilder.Entity<YemekTarifleri>(entity =>
        {
            entity.HasKey(e => e.TarifId).HasName("PK__YemekTar__87FD2128A3D05B27");

            entity.ToTable("YemekTarifleri");

            entity.Property(e => e.TarifId).HasColumnName("tarifID");
            entity.Property(e => e.Aktif).HasColumnName("aktif");
            entity.Property(e => e.KategoriId).HasColumnName("kategoriID");
            entity.Property(e => e.Silindi).HasColumnName("silindi");
            entity.Property(e => e.Sira).HasColumnName("sira");
            entity.Property(e => e.Tarif)
                .HasColumnType("ntext")
                .HasColumnName("tarif");
            entity.Property(e => e.Yemekadi)
                .HasMaxLength(250)
                .HasColumnName("yemekadi");

            entity.HasOne(d => d.Kategori).WithMany(p => p.YemekTarifleris)
                .HasForeignKey(d => d.KategoriId)
                .HasConstraintName("FK__YemekTari__kateg__3D5E1FD2");
        });

        modelBuilder.Entity<Yorumlar>(entity =>
        {
            entity.HasKey(e => e.YorumId).HasName("PK__Yorumlar__222191BFAF87D508");

            entity.ToTable("Yorumlar");

            entity.Property(e => e.YorumId).HasColumnName("yorumID");
            entity.Property(e => e.Aktif).HasColumnName("aktif");
            entity.Property(e => e.Eklemetarihi)
                .HasColumnType("datetime")
                .HasColumnName("eklemetarihi");
            entity.Property(e => e.Silindi).HasColumnName("silindi");
            entity.Property(e => e.TarifId).HasColumnName("tarifID");
            entity.Property(e => e.UyeId).HasColumnName("uyeID");
            entity.Property(e => e.Yorum)
                .HasMaxLength(250)
                .HasColumnName("yorum");

            entity.HasOne(d => d.TarifNavigation).WithMany(p => p.Yorumlars)
                .HasForeignKey(d => d.TarifId)
                .HasConstraintName("FK__Yorumlar__tarifI__4222D4EF");

            entity.HasOne(d => d.Uye).WithMany(p => p.Yorumlars)
                .HasForeignKey(d => d.UyeId)
                .HasConstraintName("FK__Yorumlar__uyeID__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
