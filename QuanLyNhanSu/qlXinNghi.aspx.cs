using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
                LoadEmployeeIDs();
                BindEmployeeDropdown(ddlMaNV);
            }
        }

        private DataTable GetEmployeeData()
        {
            string strConnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                string sql = "SELECT PK_sMaNV FROM tbl_NHANVIEN";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                return dt;
            }
        }

        private void BindEmployeeDropdown(DropDownList ddl)
        {
            DataTable dt = GetEmployeeData();
            ddl.DataSource = dt;
            ddl.DataTextField = "PK_sMaNV";
            ddl.DataValueField = "PK_sMaNV";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Chọn mã nhân viên", ""));
        }

        private void LoadEmployeeIDs()
        {
            string strConnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                try
                {
                    sqlConnection.Open();
                    string sql = "SELECT PK_sMaNV FROM tbl_NHANVIEN";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    ddlMaNV.DataSource = reader;
                    ddlMaNV.DataTextField = "PK_sMaNV"; 
                    ddlMaNV.DataValueField = "PK_sMaNV"; 
                    ddlMaNV.DataBind();

                    ddlMaNV.Items.Insert(0, new ListItem("Chọn mã nhân viên", ""));
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Đã có lỗi: Mã nhân viên";
                }
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridData(txtSearch.Text);
        }



        protected void gv_donXinNghi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the GridView to edit mode
            gv_donXinNghi.EditIndex = e.NewEditIndex;

            // Reload the GridView data
            LoadGridData(txtSearch.Text);

            // Get the GridView row being edited
            GridViewRow row = gv_donXinNghi.Rows[e.NewEditIndex];

            // Find the DropDownList and Label controls
            DropDownList ddlMaNV = (DropDownList)row.FindControl("ddlMaNV");
            Label lblMaNV = (Label)row.FindControl("lblMaNV");

            if (ddlMaNV != null)
            {
                // Bind the DropDownList with employee data
                BindEmployeeDropdown(ddlMaNV);

                if (lblMaNV != null)
                {
                    // Get the current MaNV value
                    string currentMaNV = lblMaNV.Text;

                    // Ensure the DropDownList contains the currentMaNV value
                    if (ddlMaNV.Items.FindByValue(currentMaNV) != null)
                    {
                        ddlMaNV.SelectedValue = currentMaNV;
                    }
                }
            }
            else
            {
                // Handle the case where ddlMaNV is not found
                lblMessage.Text = "DropDownList 'ddlMaNV' not found.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }


        protected void gv_donXinNghi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gv_donXinNghi.Rows[e.RowIndex];
            string maDon = ((TextBox)row.FindControl("txtMaDon")).Text;
            string ngayLapTxt = ((TextBox)row.FindControl("txtNgayLap")).Text;
            string loaiDon = ((TextBox)row.FindControl("txtLoaiDon")).Text;
            string ngayBDTxt = ((TextBox)row.FindControl("txtNgayBD")).Text;
            string ngayKTTxt = ((TextBox)row.FindControl("txtNgayKT")).Text;
            string maNV = ((DropDownList)row.FindControl("ddlMaNV")).SelectedValue;
            string lyDo = ((TextBox)row.FindControl("txtLyDo")).Text;

            DateTime ngayLap;
            bool isValidNgayLap = DateTime.TryParseExact(
                ngayLapTxt,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out ngayLap
            );

            if (!isValidNgayLap || ngayLap > DateTime.Now)
            {
                lblMessage.Text = "Ngày lập không hợp lệ hoặc lớn hơn ngày hiện tại.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            DateTime ngayBD;
            bool isValidNgayBD = DateTime.TryParseExact(
                ngayBDTxt,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out ngayBD
            );

            if (!isValidNgayBD || ngayBD > DateTime.Now)
            {
                lblMessage.Text = "Ngày bắt đầu không hợp lệ hoặc lớn hơn ngày hiện tại.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            DateTime ngayKT;
            bool isValidNgayKT = DateTime.TryParseExact(
                ngayKTTxt,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out ngayKT
            );

            if (!isValidNgayKT || ngayKT > DateTime.Now)
            {
                lblMessage.Text = "Ngày kết thúc không hợp lệ hoặc lớn hơn ngày hiện tại.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (ngayLap > ngayBD)
            {
                lblMessage.Text = "Ngày lập phải nhỏ hơn hoặc bằng ngày bắt đầu.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (ngayBD >= ngayKT)
            {
                lblMessage.Text = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

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
                    lblMessage.Text = "Đã có lỗi: Mã nhân viên không tồn tại !!!";
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
            lblErrorLyDo.Text = string.Empty;
            ddlMaNV.SelectedIndex = 0;
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

            DateTime ngayLap = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtNgayLap.Text.Trim()))
            {
                lblErrorNgayLap.Text = "Ngày lập không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorNgayLap.Text = string.Empty;

                bool isValidNgayLap = DateTime.TryParseExact(
                    txtNgayLap.Text,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out ngayLap
                );

                if (!isValidNgayLap)
                {
                    lblErrorNgayLap.Text = "Ngày lập không hợp lệ.";
                    hasError = true;
                }
                else if (ngayLap > DateTime.Now)
                {
                    lblErrorNgayLap.Text = "Ngày lập không thể lớn hơn ngày hiện tại.";
                    hasError = true;
                }
            }

            DateTime ngayBD = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtNgayBD.Text.Trim()))
            {
                lblErrorNgayBD.Text = "Ngày bắt đầu không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorNgayBD.Text = string.Empty;

                bool isValidNgayBD = DateTime.TryParseExact(
                    txtNgayBD.Text,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out ngayBD
                );

                if (!isValidNgayBD)
                {
                    lblErrorNgayBD.Text = "Ngày bắt đầu không hợp lệ.";
                    hasError = true;
                }
                else if (ngayBD > DateTime.Now)
                {
                    lblErrorNgayBD.Text = "Ngày bắt đầu không thể lớn hơn ngày hiện tại.";
                    hasError = true;
                }
            }

            DateTime ngayKT = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtNgayKT.Text.Trim()))
            {
                lblErrorNgayKT.Text = "Ngày kết thúc không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorNgayKT.Text = string.Empty;

                bool isValidNgayKT = DateTime.TryParseExact(
                    txtNgayKT.Text,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out ngayKT
                );

                if (!isValidNgayKT)
                {
                    lblErrorNgayKT.Text = "Ngày kết thúc không hợp lệ.";
                    hasError = true;
                }
                else if (ngayKT > DateTime.Now)
                {
                    lblErrorNgayKT.Text = "Ngày kết thúc không thể lớn hơn ngày hiện tại.";
                    hasError = true;
                }
            }

            if (ngayLap != DateTime.MinValue && ngayBD != DateTime.MinValue && ngayLap > ngayBD)
            {
                lblErrorNgayLap.Text = "Ngày lập phải nhỏ hơn hoặc bằng ngày bắt đầu.";
                lblErrorNgayBD.Text = "Ngày bắt đầu phải lớn hơn ngày lập.";
                hasError = true;
            }

            if (ngayBD != DateTime.MinValue && ngayKT != DateTime.MinValue && ngayBD >= ngayKT)
            {
                lblErrorNgayBD.Text = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.";
                lblErrorNgayKT.Text = "Ngày kết thúc phải lớn hơn ngày bắt đầu.";
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(ddlMaNV.SelectedValue))
            {
                lblErrorMaNV.Text = "Vui lòng chọn mã nhân viên";
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
            ddlMaNV.SelectedIndex = 0;
            txtLyDo.Text = string.Empty;
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            DateTime ngayLap, ngayBD, ngayKT;
            bool isNgayLapValid = DateTime.TryParseExact(txtNgayLap.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayLap);
            bool isNgayBDValid = DateTime.TryParseExact(txtNgayBD.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayBD);
            bool isNgayKTValid = DateTime.TryParseExact(txtNgayKT.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayKT);

            if (!isNgayLapValid || !isNgayBDValid || !isNgayKTValid)
            {
                lblMessage.Text = "Ngày chưa hợp lệ";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string selectedMaNV = ddlMaNV.SelectedValue;

            if (string.IsNullOrEmpty(selectedMaNV))
            {
                lblMessage.Text = "Vui lòng chọn mã nhân viên.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
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
                    sqlCommand.Parameters.AddWithValue("@Ngaylap", ngayLap);
                    sqlCommand.Parameters.AddWithValue("@Loaidon", txtLoaiDon.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@NgayBD", ngayBD);
                    sqlCommand.Parameters.AddWithValue("@NgayKT", ngayKT);
                    sqlCommand.Parameters.AddWithValue("@MaNV", selectedMaNV);
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
                    lblMessage.Text = "Trùng mã đơn hoặc không tồn tại Mã nhân viên";
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