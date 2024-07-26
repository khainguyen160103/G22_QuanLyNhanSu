using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu.QuanLyHopDong
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connectionString =
           ConfigurationManager.ConnectionStrings["QLNSConnectionString"].ConnectionString;
        public class ChucVu
        {
            public string MaChucVu { get; set; }
            public string TenChucVu { get; set; }
            public float HeSoLuong { get; set; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            LoadDataChucVu();
            

        }
        private void LoadDataChucVu(string filter = "")
        {
            string queryStr = "pr_SelectHopDong";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        using (DataTable dt_HopDong = new DataTable("tbl_CHUCVU"))
                        {
                            dt_HopDong.Rows.Clear();
                            adapter.Fill(dt_HopDong);
                            if (dt_HopDong.Rows.Count > 0)
                            {

                                // if (filter != null) dataVBanDoc.RowFilter = filter;

                                // gvCacChucVu.DataSource = (List<ChucVu>)Session["ChucVuList"];
                                gvCacHopDong.DataBind();



                                //  MessageBox.Show(DataGR_BanDoc.Rows[1].Cells[4].Value.ToString());
                                /* for (int i = 0; i < DataGR_BanDoc.Rows.Count; i++)
                                 {
                                     if (DataGR_BanDoc.Rows[i].Cells[4].Value.ToString() == "False")
                                     {
                                         //DataGR_BanDoc.Rows[i].Cells[4].Value = "Nam";
                                         MessageBox.Show(DataGR_BanDoc.Rows[1].Cells[4].Value.ToString());
                                     }
                                     else
                                     {
                                       //  DataGR_BanDoc.Rows[i].Cells[4].Value = "Nu";
                                     }
                                 }*/

                            }
                            else
                            {
                                /* MessageBox.Show("Không có bản ghi nào tồn tại");*/
                            }
                        }

                    }
                }
            }
        }
        protected void btnThemMoi_Click(object sender, EventArgs e)
        {

            if (KiemTraKhoaChinh())
            {
                Response.Write("<script>alert('Mã hợp đồng đã tồn tại');</script>");
                return;
            }
            if (!checknull())
            {
                Response.Write("<script>alert('Các mục thông tin không được để trống');</script>");
                return;
            }
            if (!checkLuong())
            {
                Response.Write("<script>alert('Lương phải là số và lớn hơn 0');</script>");
                return;
            }
            if (ThemBanDoc() && checknull() && checkLuong())
            {
                Response.Write("<script>alert('Thêm thành công');</script>");
                BindGrid();
            }

            else
            {
                Response.Write("<script>alert('Thêm không thành công');</script>");
            }



        }
        private bool ThemBanDoc()
        {
            DateTime Realtime1;
            Realtime1 = DateTime.Parse(DateNgayDangKy.Text.ToString());
            DateTime Realtime2;
            Realtime2 = DateTime.Parse(DateHetHan.Text.ToString());
            int check;
            string insert_proc = "pr_Them_HopDong";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = insert_proc;
                    cmd.Parameters.Clear();
                    //khoi tao va truyen cac tham so
                    cmd.Parameters.Add("@sMaHopDong", SqlDbType.VarChar, 10).Value = txtMaHopDong.Text.ToString().Trim();
                    //cmd.Parameters.AddWithValue("@masv", maSV);
                    cmd.Parameters.AddWithValue("@dNgayKyHD", Realtime1.ToString("MM/dd/yyy"));
                    cmd.Parameters.AddWithValue("@dThoiHan", Realtime2.ToString("MM/dd/yyy"));
                    cmd.Parameters.AddWithValue("@sMaNV", DropDownListMaNV.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sMaCV", DropDownListMaCV.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sMaPB", DropDownListMaPB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@fLuongCB", float.Parse(TextBoxLươngCB.Text.ToString()));
                    conn.Open();
                    check = cmd.ExecuteNonQuery();
                    conn.Close();

                    return (check > 0);
                }
            }
        }
        private bool SuaBanDoc()
        {
            DateTime Realtime1;
            Realtime1 = DateTime.Parse(DateNgayDangKy.Text.ToString());
            DateTime Realtime2;
            Realtime2 = DateTime.Parse(DateHetHan.Text.ToString());
            int check;
            string Update_proc = "pr_Sua_HopDong";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Update_proc;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@sMaHopDong", SqlDbType.VarChar, 10).Value = txtMaHopDong.Text.ToString().Trim();
                    //cmd.Parameters.AddWithValue("@masv", maSV);
                    cmd.Parameters.AddWithValue("@dNgayKyHD", Realtime1.ToString("MM/dd/yyy"));
                    cmd.Parameters.AddWithValue("@dThoiHan", Realtime2.ToString("MM/dd/yyy"));
                    cmd.Parameters.AddWithValue("@sMaNV", DropDownListMaNV.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sMaCV", DropDownListMaCV.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sMaPB", DropDownListMaPB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@fLuongCB", float.Parse(TextBoxLươngCB.Text.ToString()));
                    conn.Open();
                    check = cmd.ExecuteNonQuery();
                    conn.Close();

                    return (check > 0);
                }
            }
        }

        private bool XoaChucVu()
        {
            int check;
            string Delete_proc = "pr_Xoa_HopDong";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Delete_proc;
                    //khoi tao va truyen cac tham so
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@sMaHD", SqlDbType.VarChar, 10).Value = txtMaHopDong.Text.ToString().Trim();
                    conn.Open();
                    check = cmd.ExecuteNonQuery();
                    conn.Close();
                    return (check > 0);
                }
            }
        }
        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraKhoaChinh())
            {
                Response.Write("<script>alert('Mã hợp đồng không tồn tại');</script>");
                return;
            }
            if (SuaBanDoc())
            {
                Response.Write("<script>alert('Sửa thành công');</script>");
                BindGrid();
            }
            else
            {
                Response.Write("<script>alert('Sửa không thành công');</script>");
            }

        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFile();

        }
        protected void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (!KiemTraKhoaChinh())
            {
                Response.Write("<script>alert('Mã hợp đồng không tồn tại');</script>");
                return;
            }
            if (XoaChucVu())
            {
                Response.Write("<script>alert('Xóa thành công');</script>");
                BindGrid();
            }
            else
            {
                Response.Write("<script>alert('Xóa không thành công');</script>");
            }

        }
        private void BindGrid()
        {
            //gvCacChucVu.DataSource = (List<ChucVu>)Session["ChucVuList"];
            gvCacHopDong.DataBind();

        }
        private void ClearFields()
        {
           
        }
        private bool KiemTraKhoaChinh()
        {
            string queryStr = "pr_SelectHopDong";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        using (DataTable dt_HopDong = new DataTable("tbl_HOPDONG"))
                        {
                            dt_HopDong.Rows.Clear();
                            adapter.Fill(dt_HopDong);
                            if (dt_HopDong.Rows.Count > 0)
                            {

                                dt_HopDong.PrimaryKey = new DataColumn[] { dt_HopDong.Columns["PK_sMaHD"] };
                                DataRow dr = dt_HopDong.Rows.Find(txtMaHopDong.Text.ToString());

                                if (dr != null) return true;
                                else if (dr == null) return false;

                            }
                            else
                            {
                                /* MessageBox.Show("Không có bản ghi nào tồn tại");*/
                            }
                        }

                    }
                }
            }
            return false;
        }
        private void ClearFile()
        {
            txtMaHopDong.Text = null;
            DateNgayDangKy.Text = null;
            DateHetHan.Text = null;
            TextBoxLươngCB.Text = null;
        }
        private bool checknull()
        {
            if (txtMaHopDong.Text == "" || TextBoxLươngCB.Text == "" || DateHetHan.Text =="" || DateNgayDangKy.Text == "") return false;
            return true;
        }
        private bool checkLuong()
        {
            float a = 0;
            if (float.Parse(TextBoxLươngCB.Text.ToString()) < 0 || !float.TryParse(TextBoxLươngCB.Text.ToString(), out a)) return false;
            return true;
        }

    }
}