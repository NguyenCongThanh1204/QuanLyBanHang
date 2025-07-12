using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHang.Pages
{
    public class GioHangModel : PageModel
    {
        private readonly QLBHangContext _context;

        public GioHangModel(QLBHangContext context)
        {
            _context = context;
        }

        public List<SanPham> DanhSachSanPham { get; set; } = new();

        public void OnGet()
        {
            // Lấy các sản phẩm từ ChiTietGioHang có liên kết với SanPham
            DanhSachSanPham = _context.SanPhams
    .AsNoTracking()
    .ToList();
        }

        public void OnPost()
        {
            // Tạm thời gọi lại OnGet để giữ nguyên dữ liệu giỏ hàng
            OnGet();
        }
    }
}
