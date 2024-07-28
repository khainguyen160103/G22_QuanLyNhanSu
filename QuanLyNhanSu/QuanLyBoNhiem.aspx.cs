using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace QuanLyNhanSu
{
    public partial class QuanLyNhanSu : System.Web.UI.Page
    { 
   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBoNhiemData();
            }
            notification.Visible = false;
        }
        private void LoadBoNhiemData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT PK_sMabonhiem, dNgaylap, dNgaycohieuluc, FK_sMaNV, FK_sMaCV, FK_sMaPB FROM tblBONHIEM";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridViewBoNhiem.DataSource = dt;
                GridViewBoNhiem.DataBind();
            }
        }
        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Làm trống các textbox
            nhanvien.Value = string.Empty;
            chucvu.Value = string.Empty;
            phongban.Value = string.Empty;
            luong.Value = string.Empty;
            ngaycohieuluc.Value = string.Empty;
            noidung.Value = string.Empty;
            mabonhiem.Value = string.Empty;
        }
        private void ShowNotification(string message, string type)
        {
            notification.InnerText = message;
            notification.Attributes["class"] = "notification " + type;
            notification.Visible = true;
        }
        private void ClearInputs()
        {
            nhanvien.Value = "";
            chucvu.Value = "";
            phongban.Value = "";
            ngaycohieuluc.Value = "";
            noidung.Value = "";
        }
        protected void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM tbl_BONHIEM WHERE PK_sMabonhiem = @MaBonhiem";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBonhiem", mabonhiem.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nhanvien.Value = reader["FK_sMaNV"].ToString();
                                chucvu.Value = reader["FK_sMaCV"].ToString();
                                phongban.Value = reader["FK_sMaPB"].ToString();
                                ngaycohieuluc.Value = Convert.ToDateTime(reader["dNgaycohieuluc"]).ToString("yyyy-MM-dd");
                                noidung.Value = ""; // Nếu bạn có cột nội dung, hãy lấy giá trị từ đó

                                ShowNotification("Tìm kiếm thành công", "success");
                            }
                            else
                            {
                                ClearInputs();
                                ShowNotification("Không tìm thấy bản ghi", "error");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Lỗi: " + ex.Message, "error");
            }
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
                {
            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE tbl_BONHIEM SET dNgaycohieuluc = @Ngaycohieuluc, FK_sMaCV = @MaCV, FK_sMaPB = @MaPB WHERE PK_sMabonhiem = @MaBonhiem";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBonhiem", mabonhiem.Value);
                        cmd.Parameters.AddWithValue("@Ngaycohieuluc", ngaycohieuluc.Value);
                        cmd.Parameters.AddWithValue("@MaCV", chucvu.Value);
                        cmd.Parameters.AddWithValue("@MaPB", phongban.Value);

                        cmd.ExecuteNonQuery();
                        ShowNotification("Cập nhật thành công", "success");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Lỗi: " + ex.Message, "error");
            };
                 }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM tbl_BONHIEM WHERE PK_sMabonhiem = @MaBonhiem";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBonhiem", mabonhiem.Value);

                        cmd.ExecuteNonQuery();
                        ShowNotification("Xóa thành công", "success");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Lỗi: " + ex.Message, "error");
            }
        }
        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM tbl_BONHIEM WHERE PK_sMabonhiem = @MaBonhiem";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBonhiem", mabonhiem.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nhanvien.Value = reader["FK_sMaNV"].ToString();
                                chucvu.Value = reader["FK_sMaCV"].ToString();
                                phongban.Value = reader["FK_sMaPB"].ToString();
                                ngaycohieuluc.Value = Convert.ToDateTime(reader["dNgaycohieuluc"]).ToString("yyyy-MM-dd");
                                noidung.Value = ""; // Nếu bạn có cột nội dung, hãy lấy giá trị từ đó

                                ShowNotification("Tìm kiếm thành công", "success");
                            }
                            else
                            {
                                ClearInputs();
                                ShowNotification("Không tìm thấy bản ghi", "error");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Lỗi: " + ex.Message, "error");
            }
        }

    }
}