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
        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            string MaQ = "";

            string MS_001 = "Không để bỏ trống !";
            string DN_02 = "Tài khoản không tồn tại !";
            string DN_03 = "Sai mật khẩu !";

            string user = txtTenTaiKhoan.Value;
            string pass = txtMatKhau.Value;

            if (!checkValidDangNhap(user, pass))
            {
                messageMK.InnerText = MS_001;
            }
            else if (login(user, pass) == 0)
            {
                messageTK.InnerText = DN_02;
                messageMK.InnerText = "";
            }
            else if (login(user, pass) == 2)
            {
                messageMK.InnerText = DN_03;
                messageTK.InnerText = "";
            }
            else
            {
                setSession(MaQ, user, pass);
                Response.Redirect("../DoiMatKhau/FrmDoiMatKhau.aspx");

            }



        }
        protected bool checkValidDangNhap(string tenTK, string mk)
        {
            if (tenTK.Equals("") || mk.Equals(""))
            {
                return false;
            }

            return true;
        }
        protected int isUserNameExit(string tenTK)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
                        return 0;
                    }
                }
            }
            return 1;
        }
        protected int login(string tenTK, string mk)
        {
            int check = isUserNameExit(tenTK);
            if (check == 0)
            {
                return 0;
            }
            else
            {
                string qr = "SELECT COUNT(*) FROM tbl_TAIKHOAN WHERE sTaikhoan = @tenTK and sMatkhau COLLATE Latin1_General_CS_AS = @matkhau";
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    using (SqlCommand commam = new SqlCommand(qr, sqlcon))
                    {
                        commam.Parameters.AddWithValue("@tenTK", tenTK);
                        commam.Parameters.AddWithValue("@matkhau", mk);

                        int n = (int)commam.ExecuteScalar();
                        if (n == 0)
                        {
                            return 2;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }
        }
        protected void setSession(string Maquyen, string tenTK, string MK)
        {
            string query = "SELECT FK_sMaquyen FROM tbl_TAIKHOAN WHERE sTaikhoan = @tenTK";
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@tenTK", tenTK);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Maquyen = dr.GetString(0);
                    }
                    dr.Close();
                }
            }
            Session["MaQuyen"] = Maquyen;
            Session["TaiKhoan"] = tenTK;
            Session["MatKhau"] = MK;

        }
    }
}