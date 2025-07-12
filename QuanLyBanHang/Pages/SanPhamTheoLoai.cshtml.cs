using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHang.Pages
{
    public class SanPhamTheoLoaiModel : PageModel
    {
        private readonly QLBHangContext _context;

        public SanPhamTheoLoaiModel(QLBHangContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? MaDM { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TuKhoa { get; set; }

        public List<SanPham> SanPhams { get; set; } = new();

        public IActionResult OnGet()
        {
            var query = _context.SanPhams
                .Include(sp => sp.MaDmNavigation)
                .AsQueryable();

            if (MaDM.HasValue)
            {
                query = query.Where(sp => sp.MaDm == MaDM);
            }

            if (!string.IsNullOrWhiteSpace(TuKhoa))
            {
                query = query.Where(sp => sp.TenSp.Contains(TuKhoa));
            }

            SanPhams = query.ToList();
            return Page();
        }

        public IActionResult OnPostThemVaoGio(string maSp, int soLuong, int? maDM, string? tuKhoa)
        {
            // ✅ Kiểm tra đăng nhập
            var maND = HttpContext.Session.GetString("MaND");
            if (string.IsNullOrEmpty(maND))
            {
                TempData["Error"] = "Vui lòng đăng nhập để thêm vào giỏ hàng.";
                return RedirectToPage("/DangNhap");
            }

            // ✅ Tìm hoặc tạo giỏ hàng
            var gioHang = _context.GioHangs.FirstOrDefault(g => g.MaNd == maND);
            if (gioHang == null)
            {
                gioHang = new GioHang
                {
                    MaNd = maND,
                    NgayTao = DateOnly.FromDateTime(DateTime.Now)
                };
                _context.GioHangs.Add(gioHang);
                _context.SaveChanges();
            }

            // ✅ Thêm hoặc cập nhật sản phẩm trong giỏ
            var chiTiet = _context.ChiTietGioHangs
                .FirstOrDefault(ct => ct.MaGioHang == gioHang.MaGioHang && ct.MaSp == maSp);

            if (chiTiet != null)
            {
                chiTiet.SoLuong += soLuong;
            }
            else
            {
                _context.ChiTietGioHangs.Add(new ChiTietGioHang
                {
                    MaGioHang = gioHang.MaGioHang,
                    MaSp = maSp,
                    SoLuong = soLuong
                });
            }

            _context.SaveChanges();
            TempData["Success"] = "Đã thêm sản phẩm vào giỏ hàng.";

            // ✅ Quay lại với thông tin lọc cũ (MaDM hoặc TuKhoa)
            return RedirectToPage(new { MaDM = maDM, TuKhoa = tuKhoa });
        }
    }
}
