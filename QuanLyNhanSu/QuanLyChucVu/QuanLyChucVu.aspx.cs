using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu.QuanLyChucVu
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
           if(Session["ChucVuList"] == null)
            {
                Session["ChucVuList"] = new List<ChucVu>();
            }    
            LoadDataChucVu();

        }
        private void LoadDataChucVu(string filter = "")
        {
            string queryStr = "pr_SelectChucVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        using (DataTable dt_ChucVu = new DataTable("tbl_CHUCVU"))
                        {
                            dt_ChucVu.Rows.Clear();
                            adapter.Fill(dt_ChucVu);
                            if (dt_ChucVu.Rows.Count > 0)
                            {

                                // if (filter != null) dataVBanDoc.RowFilter = filter;

                               // gvCacChucVu.DataSource = (List<ChucVu>)Session["ChucVuList"];
                                gvCacChucVu.DataBind();



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
            if(KiemTraKhoaChinh())
            {
                Response.Write("<script>alert('Mã chức vụ đã tồn tại');</script>");
                return;
            }
            if (!checknull())
            {
                Response.Write("<script>alert('Các mục thông tin không được để trống');</script>");
                return;
            }
            if(!checkLuong())
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
            int check;
                string insert_proc = "pr_Them_Chucvu";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = insert_proc;
                        //khoi tao va truyen cac tham so
                        cmd.Parameters.Add("@sMaChucVu", SqlDbType.VarChar, 10).Value =txtMaChucVu.Text.ToString().Trim();
                        //cmd.Parameters.AddWithValue("@masv", maSV);
                        cmd.Parameters.AddWithValue("@sTenChucVu", txtTenChucVu.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("@fHeSoLuong", Math.Round (float.Parse(floatHSL.Text.ToString().Trim()) ,1));
                        conn.Open();
                        check = cmd.ExecuteNonQuery();
                        conn.Close();

                        return (check > 0);
                    }
                }
            }
       private bool SuaBanDoc()
        {
            int check;
            string Update_proc = "pr_Sua_ChucVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Update_proc;
                    //khoi tao va truyen cac tham so
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@sMaChucVu", SqlDbType.VarChar, 10).Value = txtMaChucVu.Text.ToString().Trim();
                    //cmd.Parameters.AddWithValue("@masv", maSV);
                    cmd.Parameters.AddWithValue("@sTenChucVu", txtTenChucVu.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@fHeSoLuong", Math.Round(float.Parse(floatHSL.Text.ToString().Trim()), 1));
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
            string Delete_proc = "pr_Xoa_ChucVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Delete_proc;
                    //khoi tao va truyen cac tham so
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@sMaChucVu", SqlDbType.VarChar, 10).Value = txtMaChucVu.Text.ToString().Trim();
                    //cmd.Parameters.AddWithValue("@masv", maSV);
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
                Response.Write("<script>alert('Mã chức vụ không tồn tại');</script>");
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
                Response.Write("<script>alert('Mã chức vụ không tồn tại');</script>");
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
            gvCacChucVu.DataBind();

        }
        private void ClearFields()
        {
            txtMaChucVu.Text = "";
            txtTenChucVu.Text = "";
            floatHSL.Text = "";
        }
        private bool KiemTraKhoaChinh()
        {
            string queryStr = "pr_SelectChucVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        using (DataTable dt_ChucVu = new DataTable("tbl_CHUCVU"))
                        {
                            dt_ChucVu.Rows.Clear();
                            adapter.Fill(dt_ChucVu);
                            if (dt_ChucVu.Rows.Count > 0)
                            {

                                dt_ChucVu.PrimaryKey = new DataColumn[] { dt_ChucVu.Columns["PK_sMaCV"] };
                                DataRow dr = dt_ChucVu.Rows.Find(txtMaChucVu.Text.ToString());

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
            txtMaChucVu.Text = null;
            txtTenChucVu.Text = null;
            floatHSL.Text = null;
        }
        private bool checknull()
        {
            if (txtMaChucVu.Text == null || txtTenChucVu.Text == null || floatHSL.Text == null) return false;
            return true;
        }
        private bool checkLuong()
        {
            float a = 0;
            if (float.Parse(floatHSL.Text.ToString()) < 0 || !float.TryParse(floatHSL.Text.ToString(), out a)) return false;
            return true;
        }
    }
}