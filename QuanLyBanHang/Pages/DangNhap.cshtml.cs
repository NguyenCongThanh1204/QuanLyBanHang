using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHang.Pages
{
    public class DangNhapModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public DangNhapModel(IConfiguration configuration)
        {
            _configuration = configuration;
            Input = new InputModel();
            ErrorMessage = string.Empty;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vui lòng nhập email")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            // Clear session if needed on loading login page
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // ❌ Nếu dữ liệu không hợp lệ: yêu cầu nhập lại
                return Page();
            }

            // ✅ Nếu dữ liệu hợp lệ, kết nối CSDL kiểm tra đăng nhập
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaND, HoTen, MaQuyen FROM NguoiDung WHERE Email = @Email AND MatKhau = @MatKhau";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Input.Email);
                cmd.Parameters.AddWithValue("@MatKhau", Input.Password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // ✅ Nếu thông tin đúng: lưu vào session
                    reader.Read();
                    HttpContext.Session.SetInt32("MaND", Convert.ToInt32(reader["MaND"]));
                    HttpContext.Session.SetString("HoTen", reader["HoTen"].ToString());
                    string maQuyen = reader["MaQuyen"].ToString().Trim();
                    HttpContext.Session.SetString("MaQuyen", maQuyen);

                    // ✅ Điều hướng theo quyền
                    if (maQuyen == "PQ01")
                    {
                        return RedirectToPage("/QLSanPham"); // Admin
                    }
                    else if (maQuyen == "PQ03")
                    {
                        return RedirectToPage("/Index"); // User
                    }
                    else
                    {
                        ErrorMessage = "Tài khoản không có quyền truy cập!";
                        return Page();
                    }
                }
                else
                {
                    // ❌ Nếu thông tin sai: hiển thị thông báo lỗi
                    ErrorMessage = "Email hoặc mật khẩu không đúng!";
                    return Page();
                }
            }
        }
    }
}
