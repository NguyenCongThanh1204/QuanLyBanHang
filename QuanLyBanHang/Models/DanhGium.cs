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
    public int? MaNd { get; set; }

    [Column("MaSP")]
    public int? MaSp { get; set; }

    public int? SoSao { get; set; }

    public string? BinhLuan { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayDanhGia { get; set; }

    [ForeignKey("MaNd")]
    [InverseProperty("DanhGia")]
    public virtual NguoiDung? MaNdNavigation { get; set; }

    [ForeignKey("MaSp")]
    [InverseProperty("DanhGia")]
    public virtual SanPham? MaSpNavigation { get; set; }
}
