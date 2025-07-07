using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("PHANQUYEN")]
public partial class Phanquyen
{
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string MaQuyen { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? TenQuyen { get; set; }

    [InverseProperty("MaQuyenNavigation")]
    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
