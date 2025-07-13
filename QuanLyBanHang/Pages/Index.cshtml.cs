using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyBanHang.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHang.Pages
{
    public class IndexModel : PageModel
    {
        private readonly QLBHangContext _context;

        public IndexModel(QLBHangContext context)
        {
            _context = context;
        }

        public List<DanhMuc> DanhMuc { get; set; }

        public void OnGet()
        {
            DanhMuc = _context.DanhMucs.Take(5).ToList();
        }

        public IActionResult OnPostDatNgay()
        {
            var maSp = "SP01"; // giữ nguyên string theo thiết kế ban đầu
            var soLuong = 1;

            var maNDStr = HttpContext.Session.GetString("MaND");
            if (string.IsNullOrEmpty(maNDStr) || !int.TryParse(maNDStr, out int maND))
            {
                TempData["Error"] = "Bạn cần đăng nhập để đặt hàng.";
                return RedirectToPage("/DangNhap");
            }

            var gioHang = _context.GioHangs.FirstOrDefault(g => g.MaNd == maND);
            if (gioHang == null)
            {
                gioHang = new GioHang
                {
                    MaNd = maND,
                    NgayTao = DateOnly.FromDateTime(DateTime.Today)
                };
                _context.GioHangs.Add(gioHang);
                _context.SaveChanges();
            }

            var ct = _context.ChiTietGioHangs
                .FirstOrDefault(ct => ct.MaGioHang == gioHang.MaGioHang && ct.MaSp == maSp);

            if (ct != null)
            {
                ct.SoLuong += soLuong;
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
            return RedirectToPage("/GioHang");
        }
    }
}
