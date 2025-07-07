using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Models;

[Table("LienHe")]
public partial class LienHe
{
    [Key]
    [Column("MaLH")]
    public int MaLh { get; set; }

    [Column("MaND")]
    public int? MaNd { get; set; }

    [StringLength(100)]
    public string? HoTen { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    public string? NoiDung { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayGui { get; set; }

    [ForeignKey("MaNd")]
    [InverseProperty("LienHes")]
    public virtual NguoiDung? MaNdNavigation { get; set; }
}
