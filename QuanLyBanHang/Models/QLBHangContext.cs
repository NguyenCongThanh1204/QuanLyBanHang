using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

public partial class QLBHangContext : DbContext
{
    public QLBHangContext()
    {
    }

    public QLBHangContext(DbContextOptions<QLBHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Đọc chuỗi kết nối từ appsettings.json (không ghi cứng)
            optionsBuilder.UseSqlServer("Server=DESKTOP-L82D03H\\SQLEXPRESS01;Database=QUANLYBHANG;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => new { e.MaDh, e.MaSp }).HasName("PK__ChiTietD__F557D6E0E018C430");

            entity.Property(e => e.MaDh).IsFixedLength();
            entity.Property(e => e.MaSp).IsFixedLength();

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaDH__59063A47");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDonHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaSP__59FA5E80");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => new { e.MaGioHang, e.MaSp }).HasName("PK__ChiTietG__27724D22DEF3465D");

            entity.Property(e => e.MaSp).IsFixedLength();

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietGi__MaGio__5FB337D6");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietGioHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietGio__MaSP__60A75C0F");
        });

        modelBuilder.Entity<ChiTietKhuyenMai>(entity =>
        {
            entity.HasKey(e => new { e.MaKm, e.MaSp }).HasName("PK__ChiTietK__F5579F9414691246");

            entity.Property(e => e.MaSp).IsFixedLength();

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.ChiTietKhuyenMais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietKhu__MaKM__6B24EA82");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietKhuyenMais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietKhu__MaSP__6C190EBB");
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => e.MaDg).HasName("PK__DanhGia__27258660D17F8B9A");

            entity.Property(e => e.MaNd).IsFixedLength();
            entity.Property(e => e.MaSp).IsFixedLength();

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.DanhGia).HasConstraintName("FK__DanhGia__MaND__6477ECF3");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.DanhGia).HasConstraintName("FK__DanhGia__MaSP__656C112C");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DanhMuc__2725866EBFA8B6A7");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__2725866166C23444");

            entity.Property(e => e.MaDh).IsFixedLength();
            entity.Property(e => e.MaNd).IsFixedLength();

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.DonHangs).HasConstraintName("FK__DonHang__MaND__5629CD9C");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3C5BECA9C");

            entity.Property(e => e.MaNd).IsFixedLength();

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.GioHangs).HasConstraintName("FK__GioHang__MaND__5CD6CB2B");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF15D9A6DEF7");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNd).HasName("PK__NguoiDun__2725D724C0FB4CAD");

            entity.Property(e => e.MaNd).IsFixedLength();
            entity.Property(e => e.MaLoai).IsFixedLength();

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.NguoiDungs).HasConstraintName("FK__NguoiDung__MaLoa__4BAC3F29");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NhaCungC__3A185DEB96BE6C5E");
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__PhanQuye__730A5759E5C4A883");

            entity.Property(e => e.MaLoai).IsFixedLength();
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081CB2B9082A");

            entity.Property(e => e.MaSp).IsFixedLength();

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.SanPhams).HasConstraintName("FK__SanPham__MaDM__52593CB8");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.SanPhams).HasConstraintName("FK__SanPham__MaNCC__534D60F1");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaThanhToan).HasName("PK__ThanhToa__D4B258444B133332");

            entity.HasMany(d => d.MaDhs).WithMany(p => p.MaThanhToans)
                .UsingEntity<Dictionary<string, object>>(
                    "DonHangThanhToan",
                    r => r.HasOne<DonHang>().WithMany()
                        .HasForeignKey("MaDh")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DonHang_Th__MaDH__71D1E811"),
                    l => l.HasOne<ThanhToan>().WithMany()
                        .HasForeignKey("MaThanhToan")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DonHang_T__MaTha__70DDC3D8"),
                    j =>
                    {
                        j.HasKey("MaThanhToan", "MaDh").HasName("PK__DonHang___D6C00022524E2656");
                        j.ToTable("DonHang_ThanhToan");
                        j.IndexerProperty<string>("MaDh")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("MaDH");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
