using System;
using System.Collections.Generic;
using System.Web.UI;

namespace quanLyTaiKhoanNV
{
    public class TaiKhoanNV
    {
        public string MaTaiKhoan { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string TinhTrang { get; set; }
        public string MaNhanVien { get; set; }
        public string MaQuyen { get; set; }
        public string HoatDong { get; set; }
    }

    public partial class qlTaiKhoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize or retrieve session list of accounts
                if (Session["TaiKhoanList"] == null)
                {
                    Session["TaiKhoanList"] = new List<TaiKhoanNV>();
                }

                BindGrid();
            }

            // Display alert message if passed via query string
            if (!string.IsNullOrEmpty(Request.QueryString["alertMessage"]))
            {
                string alertMessage = Request.QueryString["alertMessage"];
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);
            }
        }

        protected void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!validateInputs())
            {
                return; // Do not proceed if inputs are not valid
            }

            List<TaiKhoanNV> taiKhoanList = (List<TaiKhoanNV>)Session["TaiKhoanList"];
            TaiKhoanNV newTaiKhoan = new TaiKhoanNV
            {
                MaTaiKhoan = txtMaTaiKhoan.Text,
                TaiKhoan = txtTaiKhoan.Text,
                MatKhau = txtMatKhau.Text,
                TinhTrang = ddlTinhTrang.SelectedItem.Text,
                MaNhanVien = txtMaNhanVien.Text,
                MaQuyen = txtMaQuyen.Text
            };

            // Check if account with the same ID already exists
            if (taiKhoanList.Exists(t => t.MaTaiKhoan == newTaiKhoan.MaTaiKhoan))
            {
                Response.Redirect("qlTaiKhoan.aspx?alertMessage=Tài khoản đã tồn tại");
            }
            else
            {
                taiKhoanList.Add(newTaiKhoan);
                Session["TaiKhoanList"] = taiKhoanList;

                BindGrid();
                ClearFields();
            }
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!validateInputs())
            {
                return; // Do not proceed if inputs are not valid
            }

            List<TaiKhoanNV> taiKhoanList = (List<TaiKhoanNV>)Session["TaiKhoanList"];
            TaiKhoanNV taiKhoan = taiKhoanList.Find(t => t.MaTaiKhoan == txtMaTaiKhoan.Text);

            if (taiKhoan != null)
            {
                // Update account information
                taiKhoan.TaiKhoan = txtTaiKhoan.Text;
                taiKhoan.MatKhau = txtMatKhau.Text;
                taiKhoan.TinhTrang = ddlTinhTrang.SelectedItem.Text;
                taiKhoan.MaNhanVien = txtMaNhanVien.Text;
                taiKhoan.MaQuyen = txtMaQuyen.Text;

                Session["TaiKhoanList"] = taiKhoanList;
                BindGrid();

                ClearFields();
            }
            else
            {
                Response.Redirect("qlTaiKhoan.aspx?alertMessage=Tài khoản không tồn tại");
            }
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
           List<TaiKhoanNV> taiKhoanList = (List<TaiKhoanNV>)Session["TaiKhoanList"];
            TaiKhoanNV taiKhoan = taiKhoanList.Find(t => t.MaTaiKhoan == txtMaTaiKhoan.Text);

            if (taiKhoan != null)
            {
                taiKhoanList.Remove(taiKhoan);
                Session["TaiKhoanList"] = taiKhoanList;
                BindGrid();

                ClearFields();
            }
            else
            {
                Response.Redirect("qlTaiKhoan.aspx?alertMessage=Tài khoản không tồn tại");
            }
        }

        protected void btnTimKiemTaiKhoan_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemTaiKhoan.Text.Trim().ToLower();
            List<TaiKhoanNV> taiKhoanList = (List<TaiKhoanNV>)Session["TaiKhoanList"];

            List<TaiKhoanNV> resultList = taiKhoanList.FindAll(t =>
                t.MaTaiKhoan.ToLower().Contains(keyword) ||
                t.TaiKhoan.ToLower().Contains(keyword) ||
                t.MaNhanVien.ToLower().Contains(keyword));

            gvDanhSachTaiKhoan.DataSource = resultList;
            gvDanhSachTaiKhoan.DataBind();
        }

        private void BindGrid()
        {
            gvDanhSachTaiKhoan.DataSource = (List<TaiKhoanNV>)Session["TaiKhoanList"];
            gvDanhSachTaiKhoan.DataBind();
        }

        private void ClearFields()
        {
            txtMaTaiKhoan.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            txtMaNhanVien.Text = "";
            txtMaQuyen.Text = "";
        }

        private bool validateInputs()
        {
            // Implement your validation logic here (e.g., checking for empty fields)
            // Example:
            if (string.IsNullOrEmpty(txtMaTaiKhoan.Text))
            {
                Response.Write("<script>alert('Mã tài khoản không được để trống');</script>");
                return false;
            }
            // Add more validation as per your requirements
            return true;
        }
    }
}
