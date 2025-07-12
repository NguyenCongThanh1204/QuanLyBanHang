using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyBanHang.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHang.Pages
{
    public class DanhMucModel : PageModel
    {
        private readonly QLBHangContext _context;

        public DanhMucModel(QLBHangContext context)
        {
            _context = context;
        }

        public List<DanhMuc> DanhMucs { get; set; }

        public void OnGet()
        {
            DanhMucs = _context.DanhMucs.ToList();
        }
    }
}
