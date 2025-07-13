using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("ThanhToan")]
public partial class ThanhToan
{
    [Key]
    public int MaThanhToan { get; set; }

    [StringLength(100)]
    public string? MaChuyenKhoan { get; set; }

    [StringLength(50)]
    public string? PhuongThuc { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }

    public DateOnly? NgayThanhToan { get; set; }

    [ForeignKey("MaThanhToan")]
    [InverseProperty("MaThanhToans")]
    public virtual ICollection<DonHang> MaDhs { get; set; } = new List<DonHang>();
}
