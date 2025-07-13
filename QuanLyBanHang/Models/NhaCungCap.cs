using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("NhaCungCap")]
public partial class NhaCungCap
{
    [Key]
    [Column("MaNCC")]
    public int MaNcc { get; set; }

    [Column("TenNCC")]
    [StringLength(100)]
    public string? TenNcc { get; set; }

    [StringLength(255)]
    public string? DiaChi { get; set; }

    [Column("SDT")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Sdt { get; set; }

    [InverseProperty("MaNccNavigation")]
    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
