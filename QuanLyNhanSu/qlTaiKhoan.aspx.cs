using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quanLyTaiKhoanNV
{
    public partial class qlTaiKhoan : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAccounts();
                btnSave.Attributes["data-editmode"] = "false"; // Mặc định chế độ thêm mới
            }
        }

        private void LoadAccounts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PK_sMaTK, FK_sMaNV, sTaiKhoan, sMatKhau, sTinhTrang, FK_sMaquyen FROM tbl_TAIKHOAN";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button btnEdit = (Button)sender;
            string maTaiKhoan = btnEdit.CommandArgument;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PK_sMaTK, FK_sMaNV, sTaiKhoan, sMatKhau, sTinhTrang, FK_sMaquyen FROM tbl_TAIKHOAN WHERE PK_sMaTK = @MaTaiKhoan";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtMaTK.Text = reader["PK_sMaTK"].ToString();
                    txtMaNV.Text = reader["FK_sMaNV"].ToString();
                    txtTaiKhoan.Text = reader["sTaiKhoan"].ToString();
                    txtMatKhau.Text = reader["sMatKhau"].ToString();
                    txtTinhTrang.Text = reader["sTinhTrang"].ToString();
                    txtMaQuyen.Text = reader["FK_sMaquyen"].ToString();
                    ViewState["EditMode"] = true;
                    btnSave.Attributes["data-editmode"] = "true"; // Chế độ sửa
                }
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            string maTaiKhoan = btnDelete.CommandArgument;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM tbl_TAIKHOAN WHERE PK_sMaTK = @MaTaiKhoan";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadAccounts();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTK.Text) ||
                string.IsNullOrWhiteSpace(txtMaNV.Text) ||
                string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtTinhTrang.Text) ||
                string.IsNullOrWhiteSpace(txtMaQuyen.Text))
            {
                lblMessage.Text = "Vui lòng điền đầy đủ thông tin.";
                return;
            }

            if (ViewState["EditMode"] != null && (bool)ViewState["EditMode"])
            {
                UpdateAccount(txtMaTK.Text, txtMaNV.Text, txtTaiKhoan.Text, txtMatKhau.Text, txtTinhTrang.Text, txtMaQuyen.Text);
            }
            else
            {
                AddAccount(txtMaTK.Text, txtMaNV.Text, txtTaiKhoan.Text, txtMatKhau.Text, txtTinhTrang.Text, txtMaQuyen.Text);
            }

            lblMessage.Text = "Thông tin đã được lưu.";
            ClearForm();
            LoadAccounts();
        }

        private void AddAccount(string maTK, string maNV, string taiKhoan, string matKhau, string tinhTrang, string maQuyen)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tbl_TAIKHOAN (PK_sMaTK, FK_sMaNV, sTaiKhoan, sMatKhau, sTinhTrang, FK_sMaquyen) VALUES (@MaTK, @MaNV, @TaiKhoan, @MatKhau, @TinhTrang, @MaQuyen)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTK", maTK);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                cmd.Parameters.AddWithValue("@MaQuyen", maQuyen);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateAccount(string maTK, string maNV, string taiKhoan, string matKhau, string tinhTrang, string maQuyen)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE tbl_TAIKHOAN SET FK_sMaNV = @MaNV, sTaiKhoan = @TaiKhoan, sMatKhau = @MatKhau, sTinhTrang = @TinhTrang, FK_sMaquyen = @MaQuyen WHERE PK_sMaTK = @MaTK";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTK", maTK);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                cmd.Parameters.AddWithValue("@MaQuyen", maQuyen);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtMaTK.Text = string.Empty;
            txtMaNV.Text = string.Empty;
            txtTaiKhoan.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
            txtTinhTrang.Text = string.Empty;
            txtMaQuyen.Text = string.Empty;
            ViewState["EditMode"] = null;
            btnSave.Attributes["data-editmode"] = "false"; // Quay lại chế độ thêm mới
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchMaTK = txtSearchMaTK.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchMaTK))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT PK_sMaTK, FK_sMaNV, sTaiKhoan, sMatKhau, sTinhTrang, FK_sMaquyen FROM tbl_TAIKHOAN WHERE PK_sMaTK = @MaTK";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaTK", searchMaTK);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
            else
            {
                LoadAccounts();
            }
        }
    }
}
