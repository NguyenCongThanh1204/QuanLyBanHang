using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("DANHMUC")]
public partial class Danhmuc
{
    [Key]
    [Column("MaDM")]
    [StringLength(5)]
    [Unicode(false)]
    public string MaDm { get; set; } = null!;

    [Column("TenDM")]
    [StringLength(50)]
    public string? TenDm { get; set; }

    [Column("hinh")]
    [StringLength(50)]
    public string? Hinh { get; set; }

    [InverseProperty("MaDmNavigation")]
    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
