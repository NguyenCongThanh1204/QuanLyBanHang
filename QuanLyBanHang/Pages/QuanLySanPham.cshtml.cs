using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Pages
{
    public class QuanLySanPhamModel : PageModel
    {
        private readonly QLBanGiayContext _context;

        public QuanLySanPhamModel(QLBanGiayContext context)
        {
            _context = context;
        }

        public List<SanPham> DanhSachSanPham { get; set; } = new();

        public void OnGet()
        {
            DanhSachSanPham = _context.SanPhams
                .AsNoTracking() // tối ưu hiệu năng đọc
                .ToList();
        }
    }
}
