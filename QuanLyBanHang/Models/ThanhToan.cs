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

    [Column("MaDH")]
    public int? MaDh { get; set; }

    [StringLength(50)]
    public string? PhuongThuc { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayThanhToan { get; set; }

    [ForeignKey("MaDh")]
    [InverseProperty("ThanhToans")]
    public virtual DonHang? MaDhNavigation { get; set; }
}
