using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu
{
    public partial class qlPhongBan : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridData(txtSearch.Text);
        }

        protected void gv_phongBan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_phongBan.EditIndex = e.NewEditIndex;
            LoadGridData(txtSearch.Text);
        }

        protected void gv_phongBan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gv_phongBan.Rows[e.RowIndex];
            string maPB = ((TextBox)row.FindControl("txtMaPB")).Text;
            string tenPB = ((TextBox)row.FindControl("txtTenPB")).Text;
            string mota = ((TextBox)row.FindControl("txtMota")).Text;

            string strConnect = @"server=MSI\SQLEXPRESS;database=QLNS;integrated security=true";

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "UPDATE tbl_PHONGBAN SET sTenPB = @TenPB, sMota = @Mota WHERE PK_sMaPB = @MaPB";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@MaPB", maPB);
                    sqlCommand.Parameters.AddWithValue("@TenPB", tenPB);
                    sqlCommand.Parameters.AddWithValue("@Mota", mota);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Cập nhật thành công!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Có lỗi xảy ra khi cập nhật!";
                    }

                    gv_phongBan.EditIndex = -1;
                    LoadGridData(txtSearch.Text);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Đã tồn tại mã phòng ban !!!";
                }
            }
        }

        protected void gv_phongBan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_phongBan.EditIndex = -1;
            LoadGridData(txtSearch.Text);
        }

        protected void gv_phongBan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string maPB = gv_phongBan.DataKeys[e.RowIndex].Value.ToString();

            string strConnect = @"server=MSI\SQLEXPRESS;database=QLNS;integrated security=true";

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "DELETE FROM tbl_PHONGBAN WHERE PK_sMaPB = @MaPB";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@MaPB", maPB);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Xóa thành công!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Có lỗi xảy ra khi xóa!";
                    }

                    LoadGridData(txtSearch.Text);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtMaPB.Text = string.Empty;
            txtTenPB.Text = string.Empty;
            txtMota.Text = string.Empty;

            lblErrorMaPB.Text = string.Empty;
            lblErrorTenPB.Text = string.Empty;
            lblErrorMoTa.Text = string.Empty;

            lblMessage.Text = string.Empty;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtTenPB.Text.Trim();
            string description = txtMota.Text.Trim();
            string maPB = txtMaPB.Text.Trim();

            bool hasError = false;

            // Validate Mã Phòng Ban
            if (string.IsNullOrEmpty(maPB))
            {
                lblErrorMaPB.Text = "Mã phòng ban không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorMaPB.Text = string.Empty;
            }

            // Validate Tên Phòng Ban
            if (string.IsNullOrEmpty(name))
            {
                lblErrorTenPB.Text = "Tên phòng ban không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorTenPB.Text = string.Empty;
            }

            // Validate Mô Tả
            if (string.IsNullOrEmpty(description))
            {
                lblErrorMoTa.Text = "Mô tả không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorMoTa.Text = string.Empty;
            }

            if (hasError)
            {
                return;
            }
            string strConnect = @"server=MSI\SQLEXPRESS;database=QLNS;integrated security=true";

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "INSERT INTO tbl_PHONGBAN (PK_sMaPB, sTenPB, sMota) VALUES (@MaPB, @TenPB, @Mota)";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@MaPB", txtMaPB.Text);
                    sqlCommand.Parameters.AddWithValue("@TenPB", txtTenPB.Text);
                    sqlCommand.Parameters.AddWithValue("@Mota", txtMota.Text);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Thêm mới thành công!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        LoadGridData(txtSearch.Text);
                        txtMaPB.Text = string.Empty;
                        txtTenPB.Text = string.Empty;
                        txtMota.Text = string.Empty;
                    }
                    else
                    {
                        lblMessage.Text = "Có lỗi xảy ra khi thêm mới!";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
            }
        }

        private void LoadGridData(string searchQuery = "")
        {
            string strConnect = @"server=MSI\SQLEXPRESS;database=QLNS;integrated security=true";

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "SELECT * FROM tbl_PHONGBAN";
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        sql += " WHERE sTenPB LIKE @SearchQuery";
                    }

                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        sqlCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    gv_phongBan.DataSource = sqlDataReader;
                    gv_phongBan.DataBind();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
            }
        }
    }
}
