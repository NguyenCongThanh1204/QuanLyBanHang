//giao diện
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý bán hàng siêu thị mini</title>
    <style>
        .product-box {
            width: 220px;
            border: 1px solid #ccc;
            padding: 10px;
            margin: 10px;
            float: left;
        }

        .form-input {
            width: 100%;
            padding: 8px;
            margin: 4px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="rptSanPham" runat="server">
                <ItemTemplate>
                    <div class="product-box">
                        <asp:Image ID="imgSP" runat="server" ImageUrl='<%# Eval("HinhAnh", "~/Images/{0}") %>' Width="180px" /><br />
                        <strong><%# Eval("TenSP") %></strong><br />
                        Giá: <%# Eval("Gia") %> VNĐ<br />
                        Số lượng:
                        <asp:TextBox ID="txtSoLuong" runat="server" Text="1" Width="40px" /><br />
                        <asp:Button ID="btnThem" runat="server" Text="Thêm vào giỏ hàng" CommandArgument='<%# Eval("MaSP") %>' OnClick="btnThem_Click" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div style="clear: both;">
            <h3>Giỏ hàng:</h3>
            <asp:Label ID="lblTongTien" runat="server" Text="Tổng thành tiền: 0 VNĐ"></asp:Label><br />
            <br />

            <h4>Thông tin đặt hàng</h4>
            <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-input" Placeholder="Họ và Tên"></asp:TextBox>
            <asp:TextBox ID="txtSDT" runat="server" CssClass="form-input" Placeholder="Số điện thoại"></asp:TextBox>
            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-input" Placeholder="Địa chỉ"></asp:TextBox>
            <asp:Button ID="btnDatHang" runat="server" Text="Đặt hàng ngay" BackColor="BlueViolet" ForeColor="White" OnClick="btnDatHang_Click" />
        </div>
    </form>
</body>
</html>

// phần chức năng
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QUANLYBHANG"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadSanPham();
        }

        protected void LoadSanPham()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT MaSP, TenSP, Gia, HinhAnh FROM SanPham WHERE TrangThai = 1", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            rptSanPham.DataSource = reader;
            rptSanPham.DataBind();
            reader.Close();
            conn.Close();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string maSP = btn.CommandArgument;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox txtSL = (TextBox)item.FindControl("txtSoLuong");
            int soLuong = int.Parse(txtSL.Text);

            List<CartItem> gioHang = Session["GioHang"] as List<CartItem> ?? new List<CartItem>();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT TenSP, Gia FROM SanPham WHERE MaSP = @MaSP", conn);
            cmd.Parameters.AddWithValue("@MaSP", maSP);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string tenSP = reader["TenSP"].ToString();
                decimal gia = Convert.ToDecimal(reader["Gia"]);

                var itemGH = gioHang.FirstOrDefault(x => x.MaSP == maSP);
                if (itemGH != null)
                    itemGH.SoLuong += soLuong;
                else
                    gioHang.Add(new CartItem { MaSP = maSP, TenSP = tenSP, DonGia = gia, SoLuong = soLuong });

                Session["GioHang"] = gioHang;
                Session["TongTien"] = gioHang.Sum(x => x.SoLuong * x.DonGia);
                lblTongTien.Text = "Tổng thành tiền: " + Session["TongTien"] + " VNĐ";
            }
            reader.Close();
            conn.Close();
        }

        protected void btnDatHang_Click(object sender, EventArgs e)
        {
            string maND = "ND002"; // demo: bạn nên lấy từ tài khoản đăng nhập
            string hoTen = txtHoTen.Text;
            string sdt = txtSDT.Text;
            string diaChi = txtDiaChi.Text;
            decimal tongTien = Convert.ToDecimal(Session["TongTien"] ?? 0);
            string maDH = "DH" + DateTime.Now.Ticks % 100000;

            List<CartItem> gioHang = Session["GioHang"] as List<CartItem>;
            if (gioHang == null || gioHang.Count == 0)
            {
                lblTongTien.Text = "Giỏ hàng trống!";
                return;
            }

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmdDH = new SqlCommand("INSERT INTO DonHang (MaDH, MaND, NgayDat, TrangThai, TongTien) VALUES (@MaDH, @MaND, GETDATE(), N'Chờ xác nhận', @TongTien)", conn);
            cmdDH.Parameters.AddWithValue("@MaDH", maDH);
            cmdDH.Parameters.AddWithValue("@MaND", maND);
            cmdDH.Parameters.AddWithValue("@TongTien", tongTien);
            cmdDH.ExecuteNonQuery();

            foreach (var item in gioHang)
            {
                SqlCommand cmdCT = new SqlCommand("INSERT INTO ChiTietDonHang (MaDH, MaSP, SoLuong, DonGia) VALUES (@MaDH, @MaSP, @SL, @DG)", conn);
                cmdCT.Parameters.AddWithValue("@MaDH", maDH);
                cmdCT.Parameters.AddWithValue("@MaSP", item.MaSP);
                cmdCT.Parameters.AddWithValue("@SL", item.SoLuong);
                cmdCT.Parameters.AddWithValue("@DG", item.DonGia);
                cmdCT.ExecuteNonQuery();
            }

            conn.Close();
            Session["GioHang"] = null;
            Session["TongTien"] = 0;
            lblTongTien.Text = "Đặt hàng thành công!";
        }

        public class CartItem
        {
            public string MaSP { get; set; }
            public string TenSP { get; set; }
            public decimal DonGia { get; set; }
            public int SoLuong { get; set; }
        }
    }
}
// phần liên kết cơ sở dữ liệu
<?xml version="1.0" encoding="utf-8"?>

<!--
  For more information on how to configure your ASP.NET application, please visit
  https://go.microsoft.com/fwlink/?LinkId=169433
  -->
<configuration>
  <connectionStrings>
    <add name="QUANLYBHANG" connectionString="Data Source=DESKTOP-2H5ANE7\SQLEXPRESS;Initial Catalog=QUANLYBHANG;Integrated Security=True" providerName="System.Data.SqlClient" />
  </connectionStrings>

  <system.web>
    <compilation debug="true" targetFramework="4.6.1"/>
    <httpRuntime targetFramework="4.6.1"/>
  </system.web>
  <system.codedom>
    <compilers>
      <compiler language="c#;cs;csharp" extension=".cs"
        type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
        warningLevel="4" compilerOptions="/langversion:default /nowarn:1659;1699;1701"/>
      <compiler language="vb;vbs;visualbasic;vbscript" extension=".vb"
        type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
        warningLevel="4" compilerOptions="/langversion:default /nowarn:41008 /define:_MYTYPE=\&quot;Web\&quot; /optionInfer+"/>
    </compilers>
  </system.codedom>

</configuration>

