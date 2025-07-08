using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHang.Pages
{
    public class DangKyModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public DangKyModel(IConfiguration configuration)
        {
            _configuration = configuration;
            Input = new InputModel();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vui lòng nh?p h? tên")]
            public string HoTen { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nh?p email")]
            [EmailAddress(ErrorMessage = "Email không h?p l?")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nh?p m?t kh?u")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @Email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Email", Input.Email);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    ErrorMessage = "Email ?ã t?n t?i. Vui lòng dùng email khác.";
                    return Page();
                }
                else
                {
                    string insertQuery = "INSERT INTO NguoiDung (HoTen, Email, MatKhau, MaQuyen) VALUES (@HoTen, @Email, @MatKhau, @MaQuyen)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@HoTen", Input.HoTen);
                    insertCmd.Parameters.AddWithValue("@Email", Input.Email);
                    insertCmd.Parameters.AddWithValue("@MatKhau", Input.Password);
                    insertCmd.Parameters.AddWithValue("@MaQuyen", "PQ03"); // Quy?n m?c ??nh user

                    int result = insertCmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        SuccessMessage = "??ng ký thành công! Vui lòng ??ng nh?p.";
                        return RedirectToPage("/DangNhap");
                    }
                    else
                    {
                        ErrorMessage = "??ng ký th?t b?i. Vui lòng th? l?i.";
                        return Page();
                    }
                }
            }
        }
    }
}
