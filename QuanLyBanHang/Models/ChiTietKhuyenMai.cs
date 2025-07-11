using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[PrimaryKey("MaKm", "MaSp")]
[Table("ChiTietKhuyenMai")]
public partial class ChiTietKhuyenMai
{
    [Key]
    [Column("MaKM")]
    public int MaKm { get; set; }

    [Key]
    [Column("MaSP")]
    [StringLength(5)]
    [Unicode(false)]
    public string MaSp { get; set; } = null!;

    [Column("TyLeKM")]
    public double? TyLeKm { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [ForeignKey("MaKm")]
    [InverseProperty("ChiTietKhuyenMais")]
    public virtual KhuyenMai MaKmNavigation { get; set; } = null!;

    [ForeignKey("MaSp")]
    [InverseProperty("ChiTietKhuyenMais")]
    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
