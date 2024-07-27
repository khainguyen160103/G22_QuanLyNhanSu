using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace qlNV
{
    public partial class qlNV : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployees();
            }
        }

        private void LoadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"
                        SELECT 
                            nv.PK_sMaNV,
                            nv.sTenNV,
                            nv.dNgaysinh,
                            nv.sGioitinh,
                            nv.sCCCD,
                            nv.sDiachia,
                            nv.sSDT,
                            nv.sEmail,
                            nv.dNgayvaolam,
                            nv.fLuongcb,
                            nv.sTinhtrang,
                            cv.sTenCV,
                            pb.sTenPB
                        FROM 
                            tbl_NHANVIEN nv
                        JOIN 
                            tbl_CHUCVU cv ON nv.FK_sMaCV = cv.PK_sMaCV
                        JOIN 
                            tbl_PHONGBAN pb ON nv.FK_sMaPB = pb.PK_sMaPB";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    rptEmployees.DataSource = dt;
                    rptEmployees.DataBind();
                }
                catch (Exception ex)
                {
                    ShowMessage("Lỗi khi tải dữ liệu: " + ex.Message, true);
                }
            }
        }
        private void LoadEmployeeData(string searchKeyword = "")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT nv.PK_sMaNV, nv.sTenNV, nv.dNgaysinh, nv.sGioitinh, nv.sCCCD, nv.sDiachia, nv.sSDT, 
                           nv.sEmail, nv.dNgayvaolam, nv.fLuongcb, nv.sTinhtrang, cv.sTenCV, pb.sTenPB
                    FROM tbl_NHANVIEN nv
                    JOIN tbl_CHUCVU cv ON nv.FK_sMaCV = cv.PK_sMaCV
                    JOIN tbl_PHONGBAN pb ON nv.FK_sMaPB = pb.PK_sMaPB
                    WHERE (@SearchKeyword = '' OR 
                          nv.PK_sMaNV LIKE '%' + @SearchKeyword + '%' OR 
                          nv.sTenNV LIKE '%' + @SearchKeyword + '%' OR 
                          nv.dNgaysinh LIKE '%' + @SearchKeyword + '%' OR 
                          nv.sGioitinh LIKE '%' + @SearchKeyword + '%' OR 
                          nv.sCCCD LIKE '%' + @SearchKeyword + '%' OR 
                          nv.sDiachia LIKE '%' + @SearchKeyword + '%' OR 
                          nv.sSDT LIKE '%' + @SearchKeyword + '%' OR 
                          nv.sEmail LIKE '%' + @SearchKeyword + '%' OR 
                          nv.dNgayvaolam LIKE '%' + @SearchKeyword + '%' OR 
                          nv.fLuongcb LIKE '%' + @SearchKeyword + '%' OR 
                          nv.sTinhtrang LIKE '%' + @SearchKeyword + '%' OR 
                          cv.sTenCV LIKE '%' + @SearchKeyword + '%' OR 
                          pb.sTenPB LIKE '%' + @SearchKeyword + '%')
                    ORDER BY nv.sTenNV";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchKeyword", searchKeyword);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptEmployees.DataSource = dt;
                rptEmployees.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadEmployeeData(txtSearch.Text.Trim());
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ShowPanel(true);
            ClearFields();
            hfEmployeeID.Value = string.Empty; // Xóa ID nhân viên khi thêm mới
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string employeeID = btn.CommandArgument;
            hfEmployeeID.Value = employeeID;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM tbl_NHANVIEN WHERE PK_sMaNV = @MaNV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", employeeID);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtName.Text = reader["sTenNV"].ToString();
                        txtBirthDate.Text = Convert.ToDateTime(reader["dNgaysinh"]).ToString("yyyy-MM-dd");
                        ddlGender.SelectedValue = reader["sGioitinh"].ToString();
                        txtIDCard.Text = reader["sCCCD"].ToString();
                        txtAddress.Text = reader["sDiachia"].ToString();
                        txtPhone.Text = reader["sSDT"].ToString();
                        txtEmail.Text = reader["sEmail"].ToString();
                        txtStartDate.Text = Convert.ToDateTime(reader["dNgayvaolam"]).ToString("yyyy-MM-dd");
                        txtSalary.Text = reader["fLuongcb"].ToString();
                        txtStatus.Text = reader["sTinhtrang"].ToString();
                        txtPositionID.Text = reader["FK_sMaCV"].ToString();
                        txtDepartmentID.Text = reader["FK_sMaPB"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Lỗi khi tải dữ liệu nhân viên: " + ex.Message, true);
                }
                finally
                {
                    conn.Close();
                }
            }

            ShowPanel(true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string employeeID = btn.CommandArgument;

            // Ensure the employee ID is valid and not empty
            if (!string.IsNullOrEmpty(employeeID))
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyLuongNVConnectionString"].ConnectionString))
                {
                    try
                    {
                        // SQL query to delete the employee record
                        string query = "DELETE FROM tbl_NHANVIEN WHERE PK_sMaNV = @EmployeeID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Show success message
                        ShowMessage("Nhân viên đã được xóa thành công.", false);
                    }
                    catch (Exception ex)
                    {
                        // Show error message
                        ShowMessage("Lỗi khi xóa nhân viên: " + ex.Message, true);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                // Reload the employee list
                LoadEmployees();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string employeeID = hfEmployeeID.Value;
            string maNV = txtMaNV.Text.Trim();
            string name = txtName.Text.Trim();
            DateTime birthDate = DateTime.Parse(txtBirthDate.Text.Trim());
            string gender = ddlGender.SelectedValue;
            string idCard = txtIDCard.Text.Trim();
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            DateTime startDate = DateTime.Parse(txtStartDate.Text.Trim());
            float salary = float.Parse(txtSalary.Text.Trim());
            string status = txtStatus.Text.Trim();
            string positionID = txtPositionID.Text.Trim();
            string departmentID = txtDepartmentID.Text.Trim();

            if (string.IsNullOrEmpty(employeeID))
            {
                AddEmployee(maNV ,name, birthDate, gender, idCard, address, phone, email, startDate, salary, status, positionID, departmentID);
            }
            else
            {
                UpdateEmployee(employeeID, name, birthDate, gender, idCard, address, phone, email, startDate, salary, status, positionID, departmentID);
            }

            LoadEmployees();
            ShowPanel(false);
        }

        private void AddEmployee(string maNV, string name, DateTime birthDate, string gender, string idCard, string address, string phone, string email, DateTime startDate, float salary, string status, string positionID, string departmentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"
                        INSERT INTO tbl_NHANVIEN (PK_sMaNV, sTenNV, dNgaysinh, sGioitinh, sCCCD, sDiachia, sSDT, sEmail, dNgayvaolam, fLuongcb, sTinhtrang, FK_sMaCV, FK_sMaPB)
                        VALUES (@MaNV, @Name, @BirthDate, @Gender, @IDCard, @Address, @Phone, @Email, @StartDate, @Salary, @Status, @PositionID, @DepartmentID)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@BirthDate", birthDate);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@IDCard", idCard);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@PositionID", positionID);
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    ShowMessage("Thêm nhân viên thành công.", false);
                }
                catch (Exception ex)
                {
                    ShowMessage("Lỗi khi thêm nhân viên: " + ex.Message, true);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void UpdateEmployee(string employeeID, string name, DateTime birthDate, string gender, string idCard, string address, string phone, string email, DateTime startDate, float salary, string status, string positionID, string departmentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"
                        UPDATE tbl_NHANVIEN 
                        SET sTenNV = @Name, dNgaysinh = @BirthDate, sGioitinh = @Gender, sCCCD = @IDCard, 
                            sDiachia = @Address, sSDT = @Phone, sEmail = @Email, dNgayvaolam = @StartDate, 
                            fLuongcb = @Salary, sTinhtrang = @Status, FK_sMaCV = @PositionID, FK_sMaPB = @DepartmentID
                        WHERE PK_sMaNV = @EmployeeID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@BirthDate", birthDate);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@IDCard", idCard);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@PositionID", positionID);
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    ShowMessage("Cập nhật nhân viên thành công.", false);
                }
                catch (Exception ex)
                {
                    ShowMessage("Lỗi khi cập nhật nhân viên: " + ex.Message, true);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ShowPanel(false);
        }

        private void ShowPanel(bool isVisible)
        {
            pnlAddEdit.CssClass = isVisible ? "border p-4 bg-light" : "border p-4 bg-light d-none";
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtBirthDate.Text = "";
            ddlGender.SelectedIndex = 0;
            txtIDCard.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtStartDate.Text = "";
            txtSalary.Text = "";
            txtStatus.Text = "";
            txtPositionID.Text = "";
            txtDepartmentID.Text = "";
        }

        private void ShowMessage(string message, bool isError)
        {
            lblMessage.CssClass = isError ? "alert alert-danger" : "alert alert-success";
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }

        protected void btnRefesh_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }
    }
}