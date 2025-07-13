using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("KhuyenMai")]
public partial class KhuyenMai
{
    [Key]
    [Column("MaKM")]
    public int MaKm { get; set; }

    [Column("TenKM")]
    [StringLength(100)]
    public string? TenKm { get; set; }

    [Column("NgayBD")]
    public DateOnly? NgayBd { get; set; }

    [Column("NgayKT")]
    public DateOnly? NgayKt { get; set; }

    public bool? TrangThai { get; set; }

    [StringLength(255)]
    public string? MoTa { get; set; }

    [InverseProperty("MaKmNavigation")]
    public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; } = new List<ChiTietKhuyenMai>();
}
