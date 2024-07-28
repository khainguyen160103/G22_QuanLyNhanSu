using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Configuration;
//using QuanLyNhanSu.DangNhap;

namespace UnitTestLogin
{
    [TestClass]
    public class UnitTestLogin
    {
        // Class tương đương với FrmDangNhap
        public class FrmDangNhap
        {
            public void btnDangNhap_Click(object sender, EventArgs e)
            {
                string MaQ = "";
                string MS_001 = "Không để bỏ trống !";
                string DN_02 = "Tài khoản không tồn tại !";
                string DN_03 = "Sai mật khẩu !";

                string user = "admin"; // Thay thế bằng giá trị test
                string pass = "admin123"; // Thay thế bằng giá trị test

                if (!checkValidDangNhap(user, pass))
                {
                    // Xử lý lỗi
                }
                else if (login(user, pass) == 0)
                {
                    // Xử lý lỗi
                }
                else if (login(user, pass) == 2)
                {
                    // Xử lý lỗi
                }
                else
                {
                    setSession(MaQ, user, pass);
                    // Redirect
                }
            }

            public bool checkValidDangNhap(string tenTK, string mk)
            {
                if (string.IsNullOrEmpty(tenTK) || string.IsNullOrEmpty(mk))
                {
                    return false;
                }
                return true;
            }

            public int isUserNameExit(string tenTK)
            {
                string connectionString = "Data Source=DELL;Initial Catalog=QLNS;Integrated Security=True";
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    string query = "SELECT COUNT(*) FROM tbl_TAIKHOAN WHERE sTaikhoan = @taikhoan";
                    using (SqlCommand cmd = new SqlCommand(query, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@taikhoan", tenTK);

                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0)
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }

            public int login(string tenTK, string mk)
            {
                int check = isUserNameExit(tenTK);
                if (check == 0)
                {
                    return 0;
                }
                else
                {
                    string qr = "SELECT COUNT(*) FROM tbl_TAIKHOAN WHERE sTaikhoan = @tenTK and sMatkhau COLLATE Latin1_General_CS_AS = @matkhau";
                    string connectionString = "Data Source=DELL;Initial Catalog=QLNS;Integrated Security=True";

                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                    {
                        sqlcon.Open();
                        using (SqlCommand commam = new SqlCommand(qr, sqlcon))
                        {
                            commam.Parameters.AddWithValue("@tenTK", tenTK);
                            commam.Parameters.AddWithValue("@matkhau", mk);

                            int n = (int)commam.ExecuteScalar();
                            if (n == 0)
                            {
                                return 2;
                            }
                            else
                            {
                                return 1;
                            }
                        }
                    }
                }
            }

            public void setSession(string Maquyen, string tenTK, string MK)
            {
                // Giả lập việc thiết lập session
            }
        }

        private FrmDangNhap _frmDangNhap;

        [TestInitialize]
        public void Setup()
        {
            _frmDangNhap = new FrmDangNhap();
        }

        [TestMethod]
        public void TestMethod_DangNhap_Successfully()
        {
            // Arrange
            string tenTK = "admin";
            string matKhau = "admin123";
            int expected = 1;

            // Act
            int result = _frmDangNhap.login(tenTK, matKhau);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethod_DangNhap_TenTK_Does_Not_Exist()
        {
            // Arrange
            string tenTK = "Hoadaika";
            string matKhau = "admin123";
            int expected = 0;

            // Act
            int result = _frmDangNhap.login(tenTK, matKhau);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethod_DangNhap_MK_Does_Not_Exist()
        {
            // Arrange
            string tenTK = "admin";
            string matKhau = "12333333";
            int expected = 2;

            // Act
            int result = _frmDangNhap.login(tenTK, matKhau);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethodDALFindByUsernameNotExists()
        {
            // Arrange
            string tenTK = "Hoadaika";
            int expected = 0;

            // Act
            int result = _frmDangNhap.isUserNameExit(tenTK);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethodDALFindByUsernameExists()
        {
            // Arrange
            string tenTK = "admin";
            int expected = 1;

            // Act
            int result = _frmDangNhap.isUserNameExit(tenTK);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
