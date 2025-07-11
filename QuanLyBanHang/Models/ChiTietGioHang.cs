using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[PrimaryKey("MaGioHang", "MaSp")]
[Table("ChiTietGioHang")]
public partial class ChiTietGioHang
{
    [Key]
    public int MaGioHang { get; set; }

    [Key]
    [Column("MaSP")]
    [StringLength(5)]
    [Unicode(false)]
    public string MaSp { get; set; } = null!;

    public int? SoLuong { get; set; }

    [ForeignKey("MaGioHang")]
    [InverseProperty("ChiTietGioHangs")]
    public virtual GioHang MaGioHangNavigation { get; set; } = null!;

    [ForeignKey("MaSp")]
    [InverseProperty("ChiTietGioHangs")]
    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
