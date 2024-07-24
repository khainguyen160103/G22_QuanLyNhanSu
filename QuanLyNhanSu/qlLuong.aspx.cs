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
                ViewState["EditMode"] = false;
                btnSave.Text = "Lưu";
                LoadData();
            }
        }

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblLuongNV", con);
                SqlDataReader reader = cmd.ExecuteReader();

                Repeater1.DataSource = reader;
                Repeater1.DataBind();

                reader.Close();
                con.Close();
            }

            lblMessage.Text = ""; // Clear error message after loading data
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string tenNV = txtTenNV.Text.Trim();
            decimal luongCB;
            decimal thuong;

            lblMessage.Text = ""; // Clear error message

            if (!decimal.TryParse(txtLuongCB.Text.Trim(), out luongCB))
            {
                lblMessage.Text = "Lương cơ bản không hợp lệ.";
                return;
            }
            if (luongCB <= 0)
            {
                lblMessage.Text = "Lương cơ bản phải lớn hơn 0.";
                return;
            }

            if (!decimal.TryParse(txtThuong.Text.Trim(), out thuong))
            {
                lblMessage.Text = "Thưởng không hợp lệ.";
                return;
            }
            if (thuong <= 0)
            {
                lblMessage.Text = "Thưởng phải lớn hơn 0.";
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd;

                if ((bool)ViewState["EditMode"])
                {
                    cmd = new SqlCommand("UPDATE tblLuongNV SET sTenNV=@sTenNV, fLuongCB=@fLuongCB, fThuong=@fThuong WHERE sMaNV=@sMaNV", con);
                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO tblLuongNV (sMaNV, sTenNV, fLuongCB, fThuong) VALUES (@sMaNV, @sTenNV, @fLuongCB, @fThuong)", con);
                }

                cmd.Parameters.AddWithValue("@sMaNV", maNV);
                cmd.Parameters.AddWithValue("@sTenNV", tenNV);
                cmd.Parameters.AddWithValue("@fLuongCB", luongCB);
                cmd.Parameters.AddWithValue("@fThuong", thuong);

                cmd.ExecuteNonQuery();
                con.Close();
            }

            // Reset form and reload data
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtLuongCB.Text = "";
            txtThuong.Text = "";
            ViewState["EditMode"] = false;
            btnSave.Text = "Lưu";
            btnSave.Attributes["data-editmode"] = "false";
            LoadData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Reset form
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtLuongCB.Text = "";
            txtThuong.Text = "";
            ViewState["EditMode"] = false;
            btnSave.Text = "Lưu";
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string maNV = btn.CommandArgument;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblLuongNV WHERE sMaNV=@sMaNV", con);
                cmd.Parameters.AddWithValue("@sMaNV", maNV);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtMaNV.Text = reader["sMaNV"].ToString();
                    txtTenNV.Text = reader["sTenNV"].ToString();
                    txtLuongCB.Text = reader["fLuongCB"].ToString();
                    txtThuong.Text = reader["fThuong"].ToString();
                    ViewState["EditMode"] = true;
                    btnSave.Text = "Cập nhật";
                    // Set the data-editmode attribute to true
                    btnSave.Attributes["data-editmode"] = "true";
                }

                reader.Close();
                con.Close();
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string maNV = btn.CommandArgument;

            // Use ScriptManager to call confirm JavaScript function
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmDelete", $"if(confirmDelete()) {{ __doPostBack('{btn.UniqueID}', ''); }}", true);
            ViewState["DeleteID"] = maNV;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (IsPostBack && ViewState["DeleteID"] != null)
            {
                string maNV = ViewState["DeleteID"].ToString();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM tblLuongNV WHERE sMaNV=@sMaNV", con);
                    cmd.Parameters.AddWithValue("@sMaNV", maNV);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                LoadData();
                ViewState["DeleteID"] = null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchMaNV = txtSearchMaNV.Text.Trim();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblLuongNV WHERE sMaNV LIKE @sMaNV", con);
                cmd.Parameters.AddWithValue("@sMaNV", "%" + searchMaNV + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                con.Close();
            }
        }
    }
}
