<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlTaiKhoan.aspx.cs" Inherits="quanLyTaiKhoanNV.qlTaiKhoan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="qlNhanVien.css" rel="stylesheet" />
    <script src="qlNV.js"></script>
</head>
<body>
    <form id="form1" runat="server">
       <div id="container">
            <h1>Quản lý tài khoản nhân viên</h1>
            <div class="add_change">
                <div style="font-weight: bold">Thêm mới/Chỉnh sửa Tài khoản</div>
                <div>
                    <div>
                        <div>Mã tài khoản</div>
                        <asp:TextBox ID="txtMaTaiKhoan" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <div>Tài khoản</div>
                        <asp:TextBox ID="txtTaiKhoan" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <div>Mật khẩu</div>
                        <asp:TextBox ID="txtMatKhau" runat="server" TextMode ="Password"></asp:TextBox>
                    </div>
                    <div>
                        <div>Tình trạng</div>
                        <asp:DropDownList ID="ddlTinhTrang" runat="server">
                            <asp:ListItem Value="hoatdong">Hoạt động</asp:ListItem>
                            <asp:ListItem Value="khonghoatdong">Không hoạt động</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div>
                        <div>Mã nhân viên</div>
                        <asp:TextBox ID="txtMaNhanVien" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <div>Mã quyền</div>
                        <asp:TextBox ID="txtMaQuyen" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnThemMoi" runat="server" Text="Thêm mới" OnClientClick="return validateInputs();" OnClick="btnThemMoi_Click" />
                    <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClientClick="return validateInputs();" OnClick="btnCapNhat_Click" />
                    <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới" OnClick="btnLamMoi_Click" />
                    <asp:Button ID="btnXoaTaiKhoan" runat="server" Text="Xóa" OnClick="btnXoaTaiKhoan_Click" />
                </div>
            </div>
            <div class="dsTaikhoan">
                <div style="font-weight: bold; margin-left:100px">Danh sách tài khoản</div>
                <div style ="margin-left : 100px">
                    <asp:TextBox ID="txtTimKiemTaiKhoan" runat="server" Placeholder="Tìm kiếm tài khoản"></asp:TextBox>
                    <asp:Button ID="btnTimKiemTaiKhoan" runat="server" Text="Tìm kiếm" OnClick="btnTimKiemTaiKhoan_Click" />
                </div>
                <asp:GridView ID="gvDanhSachTaiKhoan" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="MaTaiKhoan" HeaderText="Mã tài khoản" />
                        <asp:BoundField DataField="TaiKhoan" HeaderText="Tài khoản" />
                        <asp:BoundField DataField="TinhTrang" HeaderText="Tình trạng" />
                        <asp:BoundField DataField="MaNhanVien" HeaderText="Mã nhân viên" />
                        <asp:BoundField DataField="MaQuyen" HeaderText="Mã quyền" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <script>
        function validateInputs() {
        var maTaiKhoan = document.getElementById('<%= txtMaTaiKhoan.ClientID %>').value.trim();
        var taiKhoan = document.getElementById('<%= txtTaiKhoan.ClientID %>').value.trim();
        var matKhau = document.getElementById('<%= txtMatKhau.ClientID %>').value.trim();
        var maNhanVien = document.getElementById('<%= txtMaNhanVien.ClientID %>').value.trim();
        var maQuyen = document.getElementById('<%= txtMaQuyen.ClientID %>').value.trim();

        if (maTaiKhoan === '' || taiKhoan === '' || matKhau === '' || maNhanVien === '' || maQuyen === '') {
            alert('Vui lòng nhập đầy đủ thông tin.');
            return false;
        }
        return true;
    }
        </script>

    </form>
</body>
</html>
