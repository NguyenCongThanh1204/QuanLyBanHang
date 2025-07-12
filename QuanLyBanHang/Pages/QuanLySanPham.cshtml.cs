using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Pages
{
    public class QuanLySanPhamModel : PageModel
    {
        private readonly QLBHangContext _context;

        public QuanLySanPhamModel(QLBHangContext context)
        {
            _context = context;
        }

        public List<SanPham> DanhSachSanPham { get; set; } = new List<SanPham>();


        public void OnGet()
        {
            DanhSachSanPham = _context.SanPhams
                .AsNoTracking() // tối ưu hiệu năng đọc
                .ToList();
        }
    }
}
