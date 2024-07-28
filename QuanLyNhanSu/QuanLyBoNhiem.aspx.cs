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
            ClearInputs();
        }

        private void ClearInputs()
        {
            nhanvien.Text = string.Empty;
            chucvu.Text = string.Empty;
            phongban.Text = string.Empty;
            luong.Text = string.Empty;
            ngaycohieuluc.Text = string.Empty;
            noidung.Text = string.Empty;
            mabonhiem.Text = string.Empty;
        }

        private void ShowNotification(string message, string type)
        {
            notification.InnerText = message;
            notification.Attributes["class"] = "notification " + type;
            notification.Visible = true;
        }

        protected void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO tblBONHIEM (PK_sMabonhiem, dNgaycohieuluc, FK_sMaNV, FK_sMaCV, FK_sMaPB) VALUES (@MaBonhiem, @Ngaycohieuluc, @MaNV, @MaCV, @MaPB)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBonhiem", mabonhiem.Text);
                        cmd.Parameters.AddWithValue("@MaNV", nhanvien.Text);
                        cmd.Parameters.AddWithValue("@MaCV", chucvu.Text);
                        cmd.Parameters.AddWithValue("@MaPB", phongban.Text);
                        cmd.Parameters.AddWithValue("@Ngaycohieuluc", DateTime.Parse(ngaycohieuluc.Text));

                        cmd.ExecuteNonQuery();
                        ShowNotification("Thêm mới thành công", "success");
                        LoadBoNhiemData();
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
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE tblBONHIEM SET FK_sMaNV = @MaNV, FK_sMaCV = @MaCV, FK_sMaPB = @MaPB, dNgaycohieuluc = @Ngaycohieuluc WHERE PK_sMabonhiem = @MaBonhiem";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBonhiem", mabonhiem.Text);
                        cmd.Parameters.AddWithValue("@MaNV", nhanvien.Text);
                        cmd.Parameters.AddWithValue("@MaCV", chucvu.Text);
                        cmd.Parameters.AddWithValue("@MaPB", phongban.Text);
                        cmd.Parameters.AddWithValue("@Ngaycohieuluc", DateTime.Parse(ngaycohieuluc.Text));

                        cmd.ExecuteNonQuery();
                        ShowNotification("Cập nhật thành công", "success");
                        LoadBoNhiemData();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Lỗi: " + ex.Message, "error");
            }
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM tblBONHIEM WHERE PK_sMabonhiem = @MaBonhiem";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBonhiem", mabonhiem.Text);

                        cmd.ExecuteNonQuery();
                        ShowNotification("Xóa thành công", "success");
                        LoadBoNhiemData();
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
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM tblBONHIEM WHERE PK_sMabonhiem = @MaBonhiem";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBonhiem", mabonhiem.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nhanvien.Text = reader["FK_sMaNV"].ToString();
                                chucvu.Text = reader["FK_sMaCV"].ToString();
                                phongban.Text = reader["FK_sMaPB"].ToString();
                                ngaycohieuluc.Text = Convert.ToDateTime(reader["dNgaycohieuluc"]).ToString("yyyy-MM-dd");
                                noidung.Text = ""; // Nếu bạn có cột nội dung, hãy lấy giá trị từ đó

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