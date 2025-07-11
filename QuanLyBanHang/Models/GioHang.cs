using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("GioHang")]
public partial class GioHang
{
    [Key]
    public int MaGioHang { get; set; }

    [Column("MaND")]
    [StringLength(5)]
    [Unicode(false)]
    public string? MaNd { get; set; }

    public DateOnly? NgayTao { get; set; }

    [InverseProperty("MaGioHangNavigation")]
    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    [ForeignKey("MaNd")]
    [InverseProperty("GioHangs")]
    public virtual NguoiDung? MaNdNavigation { get; set; }
}
