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
                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
                        b.PK_sMabienban, 
                        b.dNgaylap, 
                        n.sTenNV, 
                        b.sNddanhgia 
                    FROM tbl_BIENBAN b 
                    JOIN tbl_NHANVIEN n ON b.FK_sMaNV = n.PK_sMaNV 
                    WHERE 
                        b.sNddanhgia LIKE '%' + @searchKeyword + '%' OR
                        b.dNgaylap LIKE '%' + @searchKeyword + '%' OR
                        n.sTenNV LIKE '%' + @searchKeyword + '%'", conn);
                cmd.Parameters.AddWithValue("@searchKeyword", searchKeyword);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                rptBienBan.DataSource = dt;
                rptBienBan.DataBind();
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
            ddlEmployee.SelectedIndex = 0;
        }

        private string GenerateUniqueBienBanID()
        {
            DateTime now = DateTime.Now;
            string datePart = now.ToString("yyMMdd"); // Ví dụ: 240727 cho ngày 27 tháng 7 năm 2024
            string randomPart = new Random().Next(10, 99).ToString(); // Sinh số ngẫu nhiên từ 1000 đến 9999

            // Tạo mã biên bản với định dạng "BByyMMddNNNN" (tối đa 10 ký tự)
            return $"BB{datePart}{randomPart}";
        }

        private string GenerateUniqueBienBanIDWithCheck()
        {
            string newBienBanID;
            bool isUnique;

            do
            {
                newBienBanID = GenerateUniqueBienBanID();
                isUnique = CheckBienBanIDExists(newBienBanID);
            } while (!isUnique);

            return newBienBanID;
        }

        private bool CheckBienBanIDExists(string bienBanID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_BIENBAN WHERE PK_sMabienban = @PK_sMabienban", conn);
                cmd.Parameters.AddWithValue("@PK_sMabienban", bienBanID);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();

                return count == 0; // Return true if no records exist with the given ID
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    if (string.IsNullOrEmpty(hfBienBanID.Value))
                    {
                        // Tạo mã biên bản duy nhất
                        string newBienBanID = GenerateUniqueBienBanIDWithCheck();

                        cmd = new SqlCommand("INSERT INTO tbl_BIENBAN (PK_sMabienban, dNgaylap, FK_sMaNV, sNddanhgia) VALUES (@PK_sMabienban, @dNgaylap, @FK_sMaNV, @sNddanhgia)", conn);
                        cmd.Parameters.AddWithValue("@PK_sMabienban", newBienBanID);
                    }
                    else
                    {
                        cmd = new SqlCommand("UPDATE tbl_BIENBAN SET dNgaylap=@dNgaylap, FK_sMaNV=@FK_sMaNV, sNddanhgia=@sNddanhgia WHERE PK_sMabienban=@PK_sMabienban", conn);
                        cmd.Parameters.AddWithValue("@PK_sMabienban", hfBienBanID.Value);
                    }
                    cmd.Parameters.AddWithValue("@dNgaylap", txtDate.Text);
                    cmd.Parameters.AddWithValue("@FK_sMaNV", ddlEmployee.SelectedValue);
                    cmd.Parameters.AddWithValue("@sNddanhgia", txtContent.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                pnlAddEdit.CssClass = "border p-4 bg-light d-none";
                LoadBienBanData();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Có lỗi xảy ra: " + ex.Message;
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
            }
        }




        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAddEdit.CssClass = "border p-4 bg-light d-none";
        }

        protected void rptBienBan_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string bienBanID = e.CommandArgument.ToString();
            if (e.CommandName == "Edit")
            {
                EditBienBan(bienBanID);
            }
            else if (e.CommandName == "Delete")
            {
                DeleteBienBan(bienBanID);
            }
        }

        private void EditBienBan(string bienBanID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT dNgaylap, FK_sMaNV, sNddanhgia FROM tbl_BIENBAN WHERE PK_sMabienban = @PK_sMabienban", conn);
                cmd.Parameters.AddWithValue("@PK_sMabienban", bienBanID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    pnlAddEdit.CssClass = "border p-4 bg-light";
                    lblAddEdit.Text = "Sửa Biên Bản Đánh Giá";
                    hfBienBanID.Value = bienBanID;
                    txtDate.Text = Convert.ToDateTime(reader["dNgaylap"]).ToString("yyyy-MM-dd");
                    ddlEmployee.SelectedValue = reader["FK_sMaNV"].ToString();
                    txtContent.Text = reader["sNddanhgia"].ToString();
                }
            }
        }

        private void DeleteBienBan(string bienBanID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_BIENBAN WHERE PK_sMabienban = @PK_sMabienban", conn);
                cmd.Parameters.AddWithValue("@PK_sMabienban", bienBanID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadBienBanData();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

            LoadEmployeeDropDown();
        }
    }
}
