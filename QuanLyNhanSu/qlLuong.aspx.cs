using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu
{
    public partial class qlLuong : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblLuongNV", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string maNV = btn.CommandArgument;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblLuongNV WHERE sMaNV = @sMaNV", con);
                cmd.Parameters.AddWithValue("@sMaNV", maNV);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMaNV.Text = dr["sMaNV"].ToString();
                    txtTenNV.Text = dr["sTenNV"].ToString();
                    txtLuongCB.Text = dr["fLuongCB"].ToString();
                    txtThuong.Text = dr["fThuong"].ToString();
                    txtMaNV.ReadOnly = true; // Không cho sửa mã NV
                }
                con.Close();
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string maNV = btn.CommandArgument;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tblLuongNV WHERE sMaNV = @sMaNV", con);
                cmd.Parameters.AddWithValue("@sMaNV", maNV);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                if (txtMaNV.ReadOnly) // Đang ở chế độ chỉnh sửa
                {
                    cmd = new SqlCommand("UPDATE tblLuongNV SET sTenNV = @sTenNV, fLuongCB = @fLuongCB, fThuong = @fThuong WHERE sMaNV = @sMaNV", con);
                }
                else // Đang ở chế độ thêm mới
                {
                    cmd = new SqlCommand("INSERT INTO tblLuongNV (sMaNV, sTenNV, fLuongCB, fThuong) VALUES (@sMaNV, @sTenNV, @fLuongCB, @fThuong)", con);
                }

                cmd.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@sTenNV", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@fLuongCB", txtLuongCB.Text);
                cmd.Parameters.AddWithValue("@fThuong", txtThuong.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            ClearForm();
            LoadData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtLuongCB.Text = "";
            txtThuong.Text = "";
            txtMaNV.ReadOnly = false; // Cho phép nhập mã NV mới
        }
    }
}
