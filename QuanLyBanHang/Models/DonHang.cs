using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("DonHang")]
public partial class DonHang
{
    [Key]
    [Column("MaDH")]
    [StringLength(5)]
    [Unicode(false)]
    public string MaDh { get; set; } = null!;

    [Column("MaND")]
    [StringLength(5)]
    [Unicode(false)]
    public string? MaNd { get; set; }

    public DateOnly? NgayDat { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TongTien { get; set; }

    [InverseProperty("MaDhNavigation")]
    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    [ForeignKey("MaNd")]
    [InverseProperty("DonHangs")]
    public virtual NguoiDung? MaNdNavigation { get; set; }

    [ForeignKey("MaDh")]
    [InverseProperty("MaDhs")]
    public virtual ICollection<ThanhToan> MaThanhToans { get; set; } = new List<ThanhToan>();
}
