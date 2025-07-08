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
            [Required(ErrorMessage = "Vui l�ng nh?p h? t�n")]
            public string HoTen { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui l�ng nh?p email")]
            [EmailAddress(ErrorMessage = "Email kh�ng h?p l?")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui l�ng nh?p m?t kh?u")]
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
                    ErrorMessage = "Email ?� t?n t?i. Vui l�ng d�ng email kh�c.";
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
                        SuccessMessage = "??ng k� th�nh c�ng! Vui l�ng ??ng nh?p.";
                        return RedirectToPage("/DangNhap");
                    }
                    else
                    {
                        ErrorMessage = "??ng k� th?t b?i. Vui l�ng th? l?i.";
                        return Page();
                    }
                }
            }
        }
    }
}
