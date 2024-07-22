﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace QuanLyNhanSu
{
    public partial class qlXinNghi : System.Web.UI.Page
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

        protected void gv_donXinNghi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_donXinNghi.EditIndex = e.NewEditIndex;
            LoadGridData(txtSearch.Text);
        }

        protected void gv_donXinNghi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gv_donXinNghi.Rows[e.RowIndex];
            string maDon = ((TextBox)row.FindControl("txtMaDon")).Text;
            string ngayLap = ((TextBox)row.FindControl("txtNgayLap")).Text;
            string loaiDon = ((TextBox)row.FindControl("txtLoaiDon")).Text;
            string ngayBD = ((TextBox)row.FindControl("txtNgayBD")).Text;
            string ngayKT = ((TextBox)row.FindControl("txtNgayKT")).Text;
            string maNV = ((TextBox)row.FindControl("txtMaNV")).Text;
            string lyDo = ((TextBox)row.FindControl("txtlyDo")).Text;

            string strConnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "UPDATE tbl_DONXINNGHI SET dNgaylap = @NgayLap, sLoaidon = @LoaiDon, dNgaybatdau = @NgayBD, dNgayketthuc = @NgayKT, FK_sMaNV = @MaNV, sLydo = @LyDo WHERE PK_sMaDon = @MaDon";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@MaDon", maDon);
                    sqlCommand.Parameters.AddWithValue("@NgayLap", ngayLap);
                    sqlCommand.Parameters.AddWithValue("@LoaiDon", loaiDon);
                    sqlCommand.Parameters.AddWithValue("@NgayBD", ngayBD);
                    sqlCommand.Parameters.AddWithValue("@NgayKT", ngayKT);
                    sqlCommand.Parameters.AddWithValue("@MaNV", maNV);
                    sqlCommand.Parameters.AddWithValue("@LyDo", lyDo);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Cập nhật thành công!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Có lỗi xảy ra khi cập nhật!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }

                    gv_donXinNghi.EditIndex = -1;
                    LoadGridData(txtSearch.Text);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Đã có lỗi: Mã nhân viên không tồn tại";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gv_donXinNghi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_donXinNghi.EditIndex = -1;
            LoadGridData(txtSearch.Text);
        }

        protected void gv_donXinNghi_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string maDon = gv_donXinNghi.DataKeys[e.RowIndex].Value.ToString();

            string strConnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "DELETE FROM tbl_DONXINNGHI WHERE PK_sMaDon = @MaDon";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@MaDon", maDon);

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
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();

            lblErrorMaDon.Text = string.Empty;
            lblErrorNgayLap.Text = string.Empty;
            lblErrorLoaiDon.Text = string.Empty;
            lblErrorNgayBD.Text = string.Empty;
            lblErrorNgayKT.Text = string.Empty;
            lblErrorMaNV.Text = string.Empty;
            lblErrorLyDo.Text = string.Empty;

            lblMessage.Text = string.Empty;
        }

        private bool ValidateForm()
        {
            bool hasError = false;

            if (string.IsNullOrEmpty(txtMaDon.Text.Trim()))
            {
                lblErrorMaDon.Text = "Mã đơn không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorMaDon.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(txtLoaiDon.Text.Trim()))
            {
                lblErrorLoaiDon.Text = "Loại đơn không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorLoaiDon.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(txtNgayLap.Text.Trim()))
            {
                lblErrorNgayLap.Text = "Ngày lập không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorNgayLap.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(txtNgayBD.Text.Trim()))
            {
                lblErrorNgayBD.Text = "Ngày bắt đầu không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorNgayBD.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(txtNgayKT.Text.Trim()))
            {
                lblErrorNgayKT.Text = "Ngày kết thúc không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorNgayKT.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(txtMaNV.Text.Trim()))
            {
                lblErrorMaNV.Text = "Mã nhân viên không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorMaNV.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(txtLyDo.Text.Trim()))
            {
                lblErrorLyDo.Text = "Lý do không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorLyDo.Text = string.Empty;
            }

            return !hasError;
        }

        private void ClearForm()
        {
            txtMaDon.Text = string.Empty;
            txtNgayLap.Text = string.Empty;
            txtLoaiDon.Text = string.Empty;
            txtNgayBD.Text = string.Empty;
            txtNgayKT.Text = string.Empty;
            txtMaNV.Text = string.Empty;
            txtLyDo.Text = string.Empty;
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }
            string strConnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "INSERT INTO tbl_DONXINNGHI (PK_sMaDon, dNgaylap, sLoaidon, dNgaybatdau, dNgayketthuc, FK_sMaNV, sLydo) VALUES (@MaDon, @Ngaylap, @Loaidon, @NgayBD, @NgayKT, @MaNV, @Lydo)";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@MaDon", txtMaDon.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@Ngaylap", txtNgayLap.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@Loaidon", txtLoaiDon.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@NgayBD", txtNgayBD.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@NgayKT", txtNgayKT.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@MaNV", txtMaNV.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@Lydo", txtLyDo.Text.Trim());

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Thêm mới thành công!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        LoadGridData(txtSearch.Text);
                        ClearForm();
                    }
                    else
                    {
                        lblMessage.Text = "Có lỗi xảy ra khi thêm mới!";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Mã đơn không được trùng hoặc không tồn tại Mã nhân viên";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void LoadGridData(string searchQuery = "")
        {
            string strConnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "SELECT * FROM tbl_DONXINNGHI";
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        sql += " WHERE PK_sMaDon LIKE @SearchQuery";
                    }

                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        sqlCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    gv_donXinNghi.DataSource = sqlDataReader;
                    gv_donXinNghi.DataBind();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
            }
        }
    }
}