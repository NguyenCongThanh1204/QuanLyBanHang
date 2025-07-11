using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

public partial class DanhGium
{
    [Key]
    [Column("MaDG")]
    public int MaDg { get; set; }

    [Column("MaND")]
    [StringLength(5)]
    [Unicode(false)]
    public string? MaNd { get; set; }

    [Column("MaSP")]
    [StringLength(5)]
    [Unicode(false)]
    public string? MaSp { get; set; }

    public int? SoSao { get; set; }

    public string? BinhLuan { get; set; }

    public DateOnly? NgayDanhGia { get; set; }

    [ForeignKey("MaNd")]
    [InverseProperty("DanhGia")]
    public virtual NguoiDung? MaNdNavigation { get; set; }

    [ForeignKey("MaSp")]
    [InverseProperty("DanhGia")]
    public virtual SanPham? MaSpNavigation { get; set; }
}
