using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace qlNV
{
    public partial class qlNV : Page
    {
        private string connectionString = "Your_Connection_String_Here"; // Cập nhật chuỗi kết nối của bạn

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx"); // Chuyển hướng đến trang đăng nhập
            }

            if (!IsPostBack)
            {
                // Tải dữ liệu bảng nếu cần
                LoadNhanVien();
            }
        }

        /*protected void previewImage()
        {
            // Đảm bảo file đã được tải lên và có tên
            if (imgNhanVien.HasFile)
            {
                string fileName = imgNhanVien.FileName;
                string filePath = "~/Images/" + fileName; // Đường dẫn lưu ảnh
                string fullPath = Server.MapPath(filePath);

                // Hiển thị ảnh xem trước
                previewImg.ImageUrl = filePath;
                previewImg.Style["display"] = "block";
            }
            else
            {
                previewImg.Style["display"] = "none";
            }
        }*/

        protected void btnThem_Click(object sender, EventArgs e)
        {
            // Thêm nhân viên vào cơ sở dữ liệu
            AddEmployee();
            // Cập nhật GridView
            LoadNhanVien();
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            // Cập nhật thông tin nhân viên
            UpdateEmployee();
            // Cập nhật GridView
            LoadNhanVien();
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            // Xóa nhân viên
            DeleteEmployee();
            // Cập nhật GridView
            LoadNhanVien();
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa dữ liệu trên các ô nhập
            ClearForm();
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Tìm kiếm nhân viên
            SearchEmployee();
        }

        protected void gvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy mã nhân viên được chọn từ GridView
            GridViewRow row = gvNhanVien.SelectedRow;
            string maNhanVien = row.Cells[0].Text; // Giả sử cột mã nhân viên ở vị trí 0
            // Tải thông tin chi tiết của nhân viên
            LoadEmployeeDetails(maNhanVien);
        }

        private void LoadNhanVien()
        {
            // Lấy dữ liệu nhân viên từ cơ sở dữ liệu và gán cho GridView
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaNhanVien, TenNhanVien, ChucVu, TinhTrang FROM NhanVien", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvNhanVien.DataSource = dt;
                gvNhanVien.DataBind();
            }
        }

        private void AddEmployee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, GioiTinh, CCCD, NgayVaoLam, PhongBan, TinhTrang, DiaChi, SDT, Email, ChucVu, Luong, Anh) VALUES (@MaNhanVien, @TenNhanVien, @GioiTinh, @CCCD, @NgayVaoLam, @PhongBan, @TinhTrang, @DiaChi, @SDT, @Email, @ChucVu, @Luong, @Anh)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                cmd.Parameters.AddWithValue("@TenNhanVien", txtTenNhanVien.Text);
                cmd.Parameters.AddWithValue("@GioiTinh", ddlGioiTinh.SelectedValue);
                cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                cmd.Parameters.AddWithValue("@NgayVaoLam", DateTime.Parse(txtNgayVaoLam.Text));
                cmd.Parameters.AddWithValue("@PhongBan", txtPhongBan.Text);
                cmd.Parameters.AddWithValue("@TinhTrang", ddlTinhTrang.SelectedValue);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@ChucVu", txtChucVu.Text);
                cmd.Parameters.AddWithValue("@Luong", decimal.Parse(txtLuong.Text));

                // Xử lý ảnh
                string anh = string.Empty;
                if (imgNhanVien.HasFile)
                {
                    anh = "~/Images/" + imgNhanVien.FileName; // Đường dẫn tới ảnh
                    imgNhanVien.SaveAs(Server.MapPath(anh));
                }
                cmd.Parameters.AddWithValue("@Anh", anh);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateEmployee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE NhanVien SET TenNhanVien = @TenNhanVien, GioiTinh = @GioiTinh, CCCD = @CCCD, NgayVaoLam = @NgayVaoLam, PhongBan = @PhongBan, TinhTrang = @TinhTrang, DiaChi = @DiaChi, SDT = @SDT, Email = @Email, ChucVu = @ChucVu, Luong = @Luong, Anh = @Anh WHERE MaNhanVien = @MaNhanVien";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                cmd.Parameters.AddWithValue("@TenNhanVien", txtTenNhanVien.Text);
                cmd.Parameters.AddWithValue("@GioiTinh", ddlGioiTinh.SelectedValue);
                cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                cmd.Parameters.AddWithValue("@NgayVaoLam", DateTime.Parse(txtNgayVaoLam.Text));
                cmd.Parameters.AddWithValue("@PhongBan", txtPhongBan.Text);
                cmd.Parameters.AddWithValue("@TinhTrang", ddlTinhTrang.SelectedValue);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@ChucVu", txtChucVu.Text);
                cmd.Parameters.AddWithValue("@Luong", decimal.Parse(txtLuong.Text));

                // Xử lý ảnh
                string anh = string.Empty;
                if (imgNhanVien.HasFile)
                {
                    anh = "~/Images/" + imgNhanVien.FileName; // Đường dẫn tới ảnh
                    imgNhanVien.SaveAs(Server.MapPath(anh));
                }
                cmd.Parameters.AddWithValue("@Anh", anh);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteEmployee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void SearchEmployee()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaNhanVien, TenNhanVien, ChucVu, TinhTrang FROM NhanVien WHERE TenNhanVien LIKE @TenNhanVien";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TenNhanVien", "%" + txtTenNhanVien.Text + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvNhanVien.DataSource = dt;
                gvNhanVien.DataBind();
            }
        }

        private void LoadEmployeeDetails(string maNhanVien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtMaNhanVien.Text = reader["MaNhanVien"].ToString();
                    txtTenNhanVien.Text = reader["TenNhanVien"].ToString();
                    ddlGioiTinh.SelectedValue = reader["GioiTinh"].ToString();
                    txtCCCD.Text = reader["CCCD"].ToString();
                    txtNgayVaoLam.Text = Convert.ToDateTime(reader["NgayVaoLam"]).ToString("dd/MM/yyyy");
                    txtPhongBan.Text = reader["PhongBan"].ToString();
                    ddlTinhTrang.SelectedValue = reader["TinhTrang"].ToString();
                    txtDiaChi.Text = reader["DiaChi"].ToString();
                    txtSDT.Text = reader["SDT"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtChucVu.Text = reader["ChucVu"].ToString();
                    txtLuong.Text = reader["Luong"].ToString();
                   /* previewImg.ImageUrl = reader["Anh"].ToString();
                    previewImg.Style["display"] = "block";*/
                }
                reader.Close();
            }
        }

        private void ClearForm()
        {
            txtMaNhanVien.Text = string.Empty;
            txtTenNhanVien.Text = string.Empty;
            ddlGioiTinh.ClearSelection();
            txtCCCD.Text = string.Empty;
            txtNgayVaoLam.Text = string.Empty;
            txtPhongBan.Text = string.Empty;
            ddlTinhTrang.ClearSelection();
            txtDiaChi.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtChucVu.Text = string.Empty;
            txtLuong.Text = string.Empty;
           /* previewImg.ImageUrl = string.Empty;
            previewImg.Style["display"] = "none";*/
        }
    }
}
