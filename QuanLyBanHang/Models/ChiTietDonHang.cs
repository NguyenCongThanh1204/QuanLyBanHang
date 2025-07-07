using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[PrimaryKey("MaDh", "MaSp")]
[Table("ChiTietDonHang")]
public partial class ChiTietDonHang
{
    [Key]
    [Column("MaDH")]
    public int MaDh { get; set; }

    [Key]
    [Column("MaSP")]
    public int MaSp { get; set; }

    public int? SoLuong { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DonGia { get; set; }

    [ForeignKey("MaDh")]
    [InverseProperty("ChiTietDonHangs")]
    public virtual DonHang MaDhNavigation { get; set; } = null!;

    [ForeignKey("MaSp")]
    [InverseProperty("ChiTietDonHangs")]
    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
