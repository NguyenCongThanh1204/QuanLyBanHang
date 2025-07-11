using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("NguoiDung")]
public partial class NguoiDung
{
    [Key]
    [Column("MaND")]
    [StringLength(5)]
    [Unicode(false)]
    public string MaNd { get; set; } = null!;

    [StringLength(100)]
    public string? HoTen { get; set; }

    [StringLength(100)]
    public string? MatKhau { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [Column("SoDT")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SoDt { get; set; }

    [StringLength(255)]
    public string? DiaChi { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? MaLoai { get; set; }

    public bool? TrangThai { get; set; }

    public DateOnly? NgayDangKy { get; set; }

    [InverseProperty("MaNdNavigation")]
    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    [InverseProperty("MaNdNavigation")]
    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    [InverseProperty("MaNdNavigation")]
    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();

    [ForeignKey("MaLoai")]
    [InverseProperty("NguoiDungs")]
    public virtual PhanQuyen? MaLoaiNavigation { get; set; }
}
