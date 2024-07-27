using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace qlBienBanDanhGia
{
    public partial class qlBBDG : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployeeDropDown();
                LoadBienBanData();
            }
        }

        private void LoadEmployeeDropDown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT PK_sMaNV, sTenNV FROM tbl_NHANVIEN", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlEmployee.DataSource = reader;
                ddlEmployee.DataTextField = "sTenNV";
                ddlEmployee.DataValueField = "PK_sMaNV";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("-- Chọn nhân viên --", ""));
            }
        }

        private void LoadBienBanData()
        {
            string searchKeyword = txtSearch.Text.Trim();
            string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Câu lệnh SQL tìm kiếm theo tất cả các trường
                string sql = @"
            SELECT b.PK_sMabienban, b.dNgaylap, n.sTenNV, b.sNddanhgia
            FROM tbl_BIENBAN b
            JOIN tbl_NHANVIEN n ON b.FK_sMaNV = n.PK_sMaNV
            WHERE b.dNgaylap LIKE '%' + @searchKeyword + '%' 
                OR n.sTenNV LIKE '%' + @searchKeyword + '%' 
                OR b.sNddanhgia LIKE '%' + @searchKeyword + '%'
        ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@searchKeyword", searchKeyword);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvBienBan.DataSource = dt;
                gvBienBan.DataBind();
            }
        }


        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadBienBanData();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlAddEdit.CssClass = "border p-4 bg-light";
            lblAddEdit.Text = "Thêm Biên Bản Đánh Giá";
            hfBienBanID.Value = string.Empty;
            txtDate.Text = string.Empty;
            txtContent.Text = string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                if (string.IsNullOrEmpty(hfBienBanID.Value))
                {
                    // Insert new record
                    cmd = new SqlCommand("INSERT INTO tbl_BIENBAN (PK_sMabienban, dNgaylap, FK_sMaNV, sNddanhgia) VALUES (@PK_sMabienban, @dNgaylap, @FK_sMaNV, @sNddanhgia)", conn);
                    cmd.Parameters.AddWithValue("@PK_sMabienban", Guid.NewGuid().ToString()); // Generate a new ID
                }
                else
                {
                    // Update existing record
                    cmd = new SqlCommand("UPDATE tbl_BIENBAN SET dNgaylap = @dNgaylap, FK_sMaNV = @FK_sMaNV, sNddanhgia = @sNddanhgia WHERE PK_sMabienban = @PK_sMabienban", conn);
                    cmd.Parameters.AddWithValue("@PK_sMabienban", hfBienBanID.Value);
                }

                cmd.Parameters.AddWithValue("@dNgaylap", txtDate.Text);
                cmd.Parameters.AddWithValue("@FK_sMaNV", ddlEmployee.SelectedValue);
                cmd.Parameters.AddWithValue("@sNddanhgia", txtContent.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadBienBanData();
            pnlAddEdit.CssClass = "border p-4 bg-light d-none";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAddEdit.CssClass = "border p-4 bg-light d-none";
        }

        protected void gvBienBan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string bienBanID = e.CommandArgument.ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT PK_sMabienban, dNgaylap, FK_sMaNV, sNddanhgia FROM tbl_BIENBAN WHERE PK_sMabienban = @PK_sMabienban", conn);
                    cmd.Parameters.AddWithValue("@PK_sMabienban", bienBanID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        hfBienBanID.Value = reader["PK_sMabienban"].ToString();
                        txtDate.Text = reader["dNgaylap"].ToString();
                        ddlEmployee.SelectedValue = reader["FK_sMaNV"].ToString();
                        txtContent.Text = reader["sNddanhgia"].ToString();
                        pnlAddEdit.CssClass = "border p-4 bg-light";
                        lblAddEdit.Text = "Sửa Biên Bản Đánh Giá";
                    }
                    conn.Close();
                }
            }
            else if (e.CommandName == "Delete")
            {
                string bienBanID = e.CommandArgument.ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM tbl_BIENBAN WHERE PK_sMabienban = @PK_sMabienban", conn);
                    cmd.Parameters.AddWithValue("@PK_sMabienban", bienBanID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                LoadBienBanData();
            }
        }
    }
}
