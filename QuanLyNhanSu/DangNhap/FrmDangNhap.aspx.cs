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

    }
}