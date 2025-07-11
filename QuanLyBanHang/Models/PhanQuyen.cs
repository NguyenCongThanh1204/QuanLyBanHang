using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("PhanQuyen")]
public partial class PhanQuyen
{
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string MaLoai { get; set; } = null!;

    [StringLength(50)]
    public string? TenLoai { get; set; }

    [InverseProperty("MaLoaiNavigation")]
    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
