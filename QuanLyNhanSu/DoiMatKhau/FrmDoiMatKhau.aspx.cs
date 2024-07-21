using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu.DoiMatKhau
{
    public partial class FrmDoiMatKhau : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static bool checkStreghPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }
            if (!Regex.IsMatch(password, "[a-z]"))
            {
                return false;
            }
            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                return false;
            }
            if (!Regex.IsMatch(password, "[0-9]"))
            {
                return false;
            }
            return true;
        }
        protected string checkvalid(string MKcu, string MKmoi, string MKnhaplai)
        {
            string message = "";
            string MS_001 = "Vui lòng nhập đủ thông tin";
            string MS_002 = "Mật khẩu cần có 8 ký tự trở lên, bao gồm chữ số, chữ thường, chữ hoa";
            string MS_003 = "Mật khẩu nhập lại không đúng";
            string MS_004 = "Mật khẩu cũ không đúng";

            string taikhoan = (string)Session["TaiKhoan"];

            if (MKcu == "" || MKmoi == "" || MKnhaplai == "")
            {
                message = MS_001;
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    string query = "SELECT COUNT(*) FROM tbl_TAIKHOAN WHERE sTaikhoan=@taikhoan and sMatkhau = @matkhau";
                    using (SqlCommand cmd = new SqlCommand(query, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                        cmd.Parameters.AddWithValue("@matkhau", MKcu);
                        int count = (int)cmd.ExecuteScalar();

                        Session["check"] = count;
                        if (count == 0)
                        {
                            message = MS_004;
                        }
                        else
                        {
                            if (checkStreghPassword(MKmoi) == false)
                            {
                                message = MS_002;
                            }
                            else
                            {
                                if (MKnhaplai != MKmoi)
                                {
                                    message = MS_003;
                                }
                            }
                        }
                    }
                }
            }
            return message;
        }
    }
}