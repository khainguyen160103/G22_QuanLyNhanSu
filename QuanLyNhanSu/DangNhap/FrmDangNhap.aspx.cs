using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu.DangNhap
{
    public partial class FrmDangNhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string checkvalidDangNhap(string tenTK, string MK)
        {
            string MS_001 = "Không để bỏ trống !";
            string DN_02 = "Tài khoản không tồn tại !";
            string DN_03 = "Sai mật khẩu !";

            string message;
            if (tenTK == "" || MK == "")
            {
                message = MS_001;
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    string query = "SELECT COUNT(*) FROM tbl_TAIKHOAN WHERE sTaikhoan = @taikhoan";
                    using (SqlCommand cmd = new SqlCommand(query, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@taikhoan", tenTK);

                        int count = (int)cmd.ExecuteScalar();


                        if (count == 0)
                        {
                            message = DN_02;
                        }
                        else
                        {
                            string qr = "SELECT COUNT(*) FROM tbl_TAIKHOAN WHERE sTaikhoan = @tenTK and sMatkhau COLLATE Latin1_General_CS_AS = @matkhau";
                            using (SqlCommand commam = new SqlCommand(qr, sqlcon))
                            {
                                commam.Parameters.AddWithValue("@tenTK", tenTK);
                                commam.Parameters.AddWithValue("@matkhau", MK);

                                int n = (int)commam.ExecuteScalar();
                                if (n == 0)
                                {
                                    message = DN_03;
                                }
                                else
                                {
                                    message = "";
                                }
                            }
                        }
                    }
                }
            }

            return message;
        }
        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taikhoan = txtTenTaiKhoan.Value;
            string matkhau = txtMatKhau.Value;

            string MS_001 = "Không để bỏ trống !";
            string DN_02 = "Tài khoản không tồn tại !";
            string DN_03 = "Sai mật khẩu !";

            string mes = checkvalidDangNhap(taikhoan, matkhau);


            if (mes == MS_001)
            {

                if (taikhoan == "")
                {
                    messageTK.InnerText = mes;

                }
                else
                {
                    messageTK.InnerText = "";
                    messageMK.InnerText = mes;

                }
            }
            else if (mes == DN_02)
            {
                messageTK.InnerText = mes;

            }
            else if (mes == DN_03)
            {
                messageTK.InnerText = "";
                messageMK.InnerText = mes;

            }
            else
            {
                setSession(taikhoan);
                Response.Redirect("FrmTrangChu.aspx");
            }



        }
        protected void setSession(string tk)
        {
            Session["Taikhoan"] = tk;
        }
    }
}