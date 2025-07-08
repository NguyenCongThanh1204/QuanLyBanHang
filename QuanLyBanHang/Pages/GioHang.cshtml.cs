using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyBanHang.Models;
using System.Collections.Generic;

namespace QuanLyBanHang.Pages
{
    public class GioHangModel : PageModel
    {
        public List<SanPham> DanhSachSanPham { get; set; }

        public void OnGet()
        {
            DanhSachSanPham = new List<SanPham>
            {
                new SanPham
                {
                    MaSp = 1,
                    TenSp = "Rau bắp cải",
                    HinhAnh = "/images/bapcai.jpg",
                    MauSac = "Xanh",
                    Size = "Vừa",
                    SoLuong = 1,
                    Gia = 75000
                },
                new SanPham
                {
                    MaSp = 2,
                    TenSp = "Khoai lang",
                    HinhAnh = "/images/khoailang.jpg",
                    MauSac = "Tím",
                    Size = "Bé",
                    SoLuong = 2,
                    Gia = 50000
                }
            };
        }

        public void OnPost()
        {
            // Tạm thời chưa xử lý cập nhật/xoá, vì bạn đang dùng dữ liệu giả lập
            OnGet(); // để hiển thị lại dữ liệu sau POST
        }
    }
}
