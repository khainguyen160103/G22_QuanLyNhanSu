using QuanLyNhanSu.QuanLyKhenThuong;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu
{
    public partial class FrmQuanLyKhenThuong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<KhenThuong> lsKT = GetKhenThuongs();
                foreach (var kt in lsKT)
                {
                    TableRow row = new TableRow();

                    TableCell cellId = new TableCell();
                    cellId.Text = kt.maKT.ToString();
                    row.Cells.Add(cellId);

                    TableCell cellNgayLap = new TableCell();
                    cellNgayLap.Text = kt.ngaylap.ToString();
                    row.Cells.Add(cellNgayLap);

                    TableCell cellLoaiDon = new TableCell();
                    cellLoaiDon.Text = kt.loaidon.ToString();
                    row.Cells.Add(cellLoaiDon);

                    TableCell cellMaNV = new TableCell();
                    cellMaNV.Text = kt.maNV.ToString();
                    row.Cells.Add(cellMaNV);

                    TableCell cellMucKT = new TableCell();
                    cellMucKT.Text = kt.mucKTKL.ToString();
                    row.Cells.Add(cellMucKT);

                    TableCell cellLyDo = new TableCell();
                    cellLyDo.Text = kt.lydo.ToString();
                    row.Cells.Add(cellLyDo);

                    Table1.Controls.Add(row);
                }
            }

        }
        protected List<KhenThuong> GetKhenThuongs()
        {
            List<KhenThuong> listKT = new List<KhenThuong>() {  };
            string query = "Select* from tbl_KHENTHUONG";
            string stringConnnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection sqlcon = new SqlConnection(stringConnnect))
            {
                sqlcon.Open();
                using (SqlCommand cmd = new SqlCommand(query , sqlcon)) {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        KhenThuong kt = new KhenThuong
                        {
                            maKT = dr.GetString(0),
                            ngaylap = dr.GetDateTime(1),
                            loaidon = dr.GetString(2),
                            maNV = dr.GetString(3),
                            mucKTKL = dr.GetString(4),
                            lydo = dr.GetString(5)
                        };
                        listKT.Add(kt);
                    }
                    dr.Close();
                }
            }


                return listKT;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ma = MaKT.Value;
            string loaidon = LoaiDon.Value;
            string maNV = MaNV.Value;
            string MucKT = MucKhenThuong.Value;
            string ldo = LyDo.Value;
            if (ma == "" || loaidon == "" || maNV == "" || MucKT == "" || ldo == "")
            {
                Label1.Text = "Mời nhập đầy đủ thông tin !";
            }
            else {
                string query = $"insert into tbl_KHENTHUONG values ('{ma}' , '{DateTime.Now}' , N'{loaidon}' , '{maNV}' , N'{MucKT}' , N'{ldo}')";
                string stringConnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection sqlcon = new SqlConnection(stringConnect))
                {
                    sqlcon.Open();

                    using (SqlCommand cmd = new SqlCommand(query, sqlcon))
                    {
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Label1.Text = "Thêm thành công !";
                            List<KhenThuong> lsKT = GetKhenThuongs();
                            foreach (var kt in lsKT)
                            {
                                TableRow row = new TableRow();

                                TableCell cellId = new TableCell();
                                cellId.Text = kt.maKT.ToString();
                                row.Cells.Add(cellId);

                                TableCell cellNgayLap = new TableCell();
                                cellNgayLap.Text = kt.ngaylap.ToString();
                                row.Cells.Add(cellNgayLap);

                                TableCell cellLoaiDon = new TableCell();
                                cellLoaiDon.Text = kt.loaidon.ToString();
                                row.Cells.Add(cellLoaiDon);

                                TableCell cellMaNV = new TableCell();
                                cellMaNV.Text = kt.maNV.ToString();
                                row.Cells.Add(cellMaNV);

                                TableCell cellMucKT = new TableCell();
                                cellMucKT.Text = kt.mucKTKL.ToString();
                                row.Cells.Add(cellMucKT);

                                TableCell cellLyDo = new TableCell();
                                cellLyDo.Text = kt.lydo.ToString();
                                row.Cells.Add(cellLyDo);

                                Table1.Controls.Add(row);
                            }
                        }
                        else
                        {
                            Label1.Text = "Thêm không thành công !";
                        }
                    }
                }
            }

            

            
            

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MaKT.Value = "";
            LoaiDon.Value = "";
            MaNV.Value = "";
            MucKhenThuong.Value = "";
            LyDo.Value = "";
        }
    }
}