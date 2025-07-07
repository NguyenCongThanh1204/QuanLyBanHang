using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("NguoiDung")]
[Index("Email", Name = "UQ__NguoiDun__A9D10534B09C7174", IsUnique = true)]
public partial class NguoiDung
{
    [Key]
    [Column("MaND")]
    public int MaNd { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? MaQuyen { get; set; }

    [StringLength(100)]
    public string? HoTen { get; set; }

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string MatKhau { get; set; } = null!;

    [StringLength(20)]
    public string? SoDienThoai { get; set; }

    [StringLength(255)]
    public string? DiaChi { get; set; }

    public bool? TrangThai { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayDangKy { get; set; }

    [InverseProperty("MaNdNavigation")]
    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    [InverseProperty("MaNdNavigation")]
    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    [InverseProperty("MaNdNavigation")]
    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();

    [InverseProperty("MaNdNavigation")]
    public virtual ICollection<LienHe> LienHes { get; set; } = new List<LienHe>();

    [ForeignKey("MaQuyen")]
    [InverseProperty("NguoiDungs")]
    public virtual Phanquyen? MaQuyenNavigation { get; set; }
}
