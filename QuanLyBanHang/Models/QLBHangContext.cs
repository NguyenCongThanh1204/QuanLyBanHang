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

    public virtual DbSet<LoaiNguoiDung> LoaiNguoiDungs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=laptop-0aqct8gt\\sqlexpress;Database=QUANLYBHANG;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => new { e.MaDh, e.MaSp }).HasName("PK__ChiTietD__F557D6E0AD7A9104");

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
            entity.HasKey(e => new { e.MaGioHang, e.MaSp }).HasName("PK__ChiTietG__27724D2220594FBF");

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
            entity.HasKey(e => new { e.MaKm, e.MaSp }).HasName("PK__ChiTietK__F5579F94F230C785");

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
            entity.HasKey(e => e.MaDg).HasName("PK__DanhGia__2725866013DAB626");

            entity.Property(e => e.MaSp).IsFixedLength();

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.DanhGia).HasConstraintName("FK__DanhGia__MaND__6477ECF3");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.DanhGia).HasConstraintName("FK__DanhGia__MaSP__656C112C");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DanhMuc__2725866EDC8256FC");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__2725866120E26E41");

            entity.Property(e => e.MaDh).IsFixedLength();

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.DonHangs).HasConstraintName("FK__DonHang__MaND__5629CD9C");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3309D0F37");

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.GioHangs).HasConstraintName("FK__GioHang__MaND__5CD6CB2B");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF1528C98C69");
        });

        modelBuilder.Entity<LoaiNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LoaiNguo__730A5759C1C7356D");

            entity.Property(e => e.MaLoai).IsFixedLength();
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNd).HasName("PK__NguoiDun__2725D7248B128B57");

            entity.Property(e => e.MaLoai).IsFixedLength();

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.NguoiDungs).HasConstraintName("FK__NguoiDung__MaLoa__4BAC3F29");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NhaCungC__3A185DEB0CB98624");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081CA5DAD9BF");

            entity.Property(e => e.MaSp).IsFixedLength();

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.SanPhams).HasConstraintName("FK__SanPham__MaDM__52593CB8");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.SanPhams).HasConstraintName("FK__SanPham__MaNCC__534D60F1");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaThanhToan).HasName("PK__ThanhToa__D4B25844E697B9CA");

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
                        j.HasKey("MaThanhToan", "MaDh").HasName("PK__DonHang___D6C0002266789DED");
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
