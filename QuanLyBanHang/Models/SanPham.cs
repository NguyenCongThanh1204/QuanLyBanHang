using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("SanPham")]
public partial class SanPham
{
    [Key]
    [Column("MaSP")]
    [StringLength(5)]
    [Unicode(false)]
    public string MaSp { get; set; } = null!;

    [Column("TenSP")]
    [StringLength(100)]
    public string? TenSp { get; set; }

    public string? MoTa { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Gia { get; set; }

    public int? SoLuong { get; set; }

    [StringLength(20)]
    public string? Size { get; set; }

    [StringLength(50)]
    public string? MauSac { get; set; }

    [StringLength(255)]
    public string? HinhAnh { get; set; }

    public DateOnly? NgayNhap { get; set; }

    public bool? TrangThai { get; set; }

    [Column("MaDM")]
    public int? MaDm { get; set; }

    [Column("MaNCC")]
    public int? MaNcc { get; set; }

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; } = new List<ChiTietKhuyenMai>();

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    [ForeignKey("MaDm")]
    [InverseProperty("SanPhams")]
    public virtual DanhMuc? MaDmNavigation { get; set; }

    [ForeignKey("MaNcc")]
    [InverseProperty("SanPhams")]
    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
