using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string connectionString = _configuration.GetConnectionString("QLBanHangConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaND, HoTen, MaLoai FROM NguoiDung WHERE Email = @Email AND MatKhau = @MatKhau";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Input.Email);
                cmd.Parameters.AddWithValue("@MatKhau", Input.Password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    int maND = Convert.ToInt32(reader["MaND"]);
                    string hoTen = reader["HoTen"].ToString();
                    string maQuyen = reader["MaLoai"].ToString().Trim();

                    // Lưu Session nếu muốn
                    HttpContext.Session.SetInt32("MaND", maND);
                    HttpContext.Session.SetString("HoTen", hoTen);
                    HttpContext.Session.SetString("MaLoai", maQuyen);

                    // Tạo Claims và đăng nhập Cookie Auth
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, hoTen ?? ""),
                        new Claim("MaND", maND.ToString()),
                        new Claim("MaLoai", maQuyen)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (maQuyen == "PQ01")
                    {
                        return RedirectToPage("/QuanLySanPham");
                    }
                    else if (maQuyen == "PQ03")
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        ErrorMessage = "Tài khoản không có quyền truy cập!";
                        return Page();
                    }
                }
                else
                {
                    ErrorMessage = "Email hoặc mật khẩu không đúng!";
                    return Page();
                }
            }
        }
    }
}
