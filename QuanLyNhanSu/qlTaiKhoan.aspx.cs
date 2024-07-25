using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quanLyTaiKhoanNV
{
    public class TaiKhoanNV
    {
        public string MaTaiKhoan { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string TinhTrang { get; set; }
        public string MaNhanVien { get; set; }
        public string MaQuyen { get; set; }
        public string HoatDong { get; set; }
    }

    public partial class qlTaiKhoan : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                ViewState["EditMode"] = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearchMaTK.Text.Trim());
        }

        private void LoadData(string searchMaTK = "")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd;
                if (string.IsNullOrEmpty(searchMaTK))
                {
                    cmd = new SqlCommand("SELECT * FROM tblTaiKhoan", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT * FROM tblTaiKhoan WHERE sMaTK LIKE @searchMaTK", con);
                    cmd.Parameters.AddWithValue("@searchMaTK", "%" + searchMaTK + "%");
                }

                SqlDataReader reader = cmd.ExecuteReader();

                Repeater1.DataSource = reader;
                Repeater1.DataBind();

                reader.Close();
                con.Close();
            }

            lblMessage.Text = ""; // Clear error message after loading data
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            string maTK = ((Button)sender).CommandArgument;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblTaiKhoan WHERE sMaTK=@sMaTK", con);
                cmd.Parameters.AddWithValue("@sMaTK", maTK);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtMaTK.Text = reader["sMaTK"].ToString();
                    txtTenTK.Text = reader["sTenTK"].ToString();
                    txtMatKhau.Text = reader["sMatKhau"].ToString();

                    ViewState["EditMode"] = true;
                    btnSave.Text = "Sửa";
                    btnSave.Attributes["data-editmode"] = "true";
                }

                reader.Close();
                con.Close();
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string maTK = ((Button)sender).CommandArgument;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM tblTaiKhoan WHERE sMaTK=@sMaTK", con);
                cmd.Parameters.AddWithValue("@sMaTK", maTK);

                cmd.ExecuteNonQuery();
                con.Close();
            }

            LoadData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maTK = txtMaTK.Text.Trim();
            string tenTK = txtTenTK.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            lblMessage.Text = ""; // Clear error message

            if (string.IsNullOrEmpty(maTK))
            {
                lblMessage.Text = "Mã tài khoản không được để trống.";
                return;
            }
            if (string.IsNullOrEmpty(tenTK))
            {
                lblMessage.Text = "Tên tài khoản không được để trống.";
                return;
            }
            if (string.IsNullOrEmpty(matKhau))
            {
                lblMessage.Text = "Mật khẩu không được để trống.";
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd;

                if ((bool)ViewState["EditMode"])
                {
                    cmd = new SqlCommand("UPDATE tblTaiKhoan SET sTenTK=@sTenTK, sMatKhau=@sMatKhau WHERE sMaTK=@sMaTK", con);
                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO tblTaiKhoan (sMaTK, sTenTK, sMatKhau) VALUES (@sMaTK, @sTenTK, @sMatKhau)", con);
                }

                cmd.Parameters.AddWithValue("@sMaTK", maTK);
                cmd.Parameters.AddWithValue("@sTenTK", tenTK);
                cmd.Parameters.AddWithValue("@sMatKhau", matKhau);

                cmd.ExecuteNonQuery();
                con.Close();
            }

            // Reset form and reload data
            txtMaTK.Text = "";
            txtTenTK.Text = "";
            txtMatKhau.Text = "";
            ViewState["EditMode"] = false;
            btnSave.Text = "Lưu";
            btnSave.Attributes["data-editmode"] = "false";
            LoadData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Reset form
            txtMaTK.Text = "";
            txtTenTK.Text = "";
            txtMatKhau.Text = "";
            ViewState["EditMode"] = false;
            btnSave.Text = "Lưu";
            btnSave.Attributes["data-editmode"] = "false";
            lblMessage.Text = "";
        }
    }
}
