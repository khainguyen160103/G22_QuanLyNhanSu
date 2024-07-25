using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu
{
    public partial class qlChamCong : System.Web.UI.Page
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblChamCongNV", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split('~');
            string maNV = args[0];
            DateTime ngayChamCong = DateTime.Parse(args[1]);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblChamCongNV WHERE sMaNV = @sMaNV AND dNgayChamCong = @dNgayChamCong", con);
                cmd.Parameters.AddWithValue("@sMaNV", maNV);
                cmd.Parameters.AddWithValue("@dNgayChamCong", ngayChamCong);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMaNV.Text = dr["sMaNV"].ToString();
                    txtTenNV.Text = dr["sTenNV"].ToString();
                    txtNgayChamCong.Text = ((DateTime)dr["dNgayChamCong"]).ToString("yyyy-MM-dd");
                    ddlDiLam.SelectedValue = dr["bDiLam"].ToString();
                    txtMaNV.ReadOnly = false; // Không cho sửa mã NV
                    txtNgayChamCong.ReadOnly = false; // Không cho sửa ngày chấm công
                }
                con.Close();
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split('~');
            string maNV = args[0];
            DateTime ngayChamCong = DateTime.Parse(args[1]);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tblChamCongNV WHERE sMaNV = @sMaNV AND dNgayChamCong = @dNgayChamCong", con);
                cmd.Parameters.AddWithValue("@sMaNV", maNV);
                cmd.Parameters.AddWithValue("@dNgayChamCong", ngayChamCong);
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
                    cmd = new SqlCommand("UPDATE tblChamCongNV SET sTenNV = @sTenNV, bDiLam = @bDiLam WHERE sMaNV = @sMaNV AND dNgayChamCong = @dNgayChamCong", con);
                    cmd.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@sTenNV", txtTenNV.Text);
                    cmd.Parameters.AddWithValue("@dNgayChamCong", DateTime.Parse(txtNgayChamCong.Text));
                    cmd.Parameters.AddWithValue("@bDiLam", ddlDiLam.SelectedValue);
                }
                else // Chế độ thêm mới
                {
                    cmd = new SqlCommand("INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES (@sMaNV, @sTenNV, @dNgayChamCong, @bDiLam)", con);
                    cmd.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@sTenNV", txtTenNV.Text);
                    cmd.Parameters.AddWithValue("@dNgayChamCong", DateTime.Parse(txtNgayChamCong.Text));
                    cmd.Parameters.AddWithValue("@bDiLam", ddlDiLam.SelectedValue);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadData();
            ClearForm();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtNgayChamCong.Text = "";
            ddlDiLam.SelectedIndex = 0;
            txtMaNV.ReadOnly = false;
            txtNgayChamCong.ReadOnly = false;
        }
    }
}