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
    public int MaSp { get; set; }

    [Column("MaDM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? MaDm { get; set; }

    [Column("TenSP")]
    [StringLength(200)]
    public string? TenSp { get; set; }

    public string? MoTa { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Gia { get; set; }

    public int? SoLuong { get; set; }

    [StringLength(10)]
    public string? Size { get; set; }

    [StringLength(50)]
    public string? MauSac { get; set; }

    [StringLength(255)]
    public string? HinhAnh { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayNhap { get; set; }

    public bool? TrangThai { get; set; }

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    [ForeignKey("MaDm")]
    [InverseProperty("SanPhams")]
    public virtual Danhmuc? MaDmNavigation { get; set; }
}
