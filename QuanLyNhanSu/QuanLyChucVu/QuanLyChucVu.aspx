<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="QuanLyNhanSu.QuanLyChucVu.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="QuanLyChucVu.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="contaner">
            <h1 >Quản lý chức vụ nhân viên</h1>
            <div class="add_change">
                <div style="font-weight: bold">Thêm mới/Chỉnh sửa Tài khoản</div>
                <div>
                    <div>
                        <div>Mã chức vụ</div>
                        <asp:TextBox ID="txtMaChucVu" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <div>Tên chức vụ</div>
                        <asp:TextBox ID="txtTenChucVu" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <div>Hệ số lương</div>
                        <asp:TextBox ID="floatHSL" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnThemMoi" runat="server" Text="Thêm mới" OnClientClick="return validateInputs();" OnClick="btnThemMoi_Click" />
                    <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClientClick="return validateInputs();" OnClick="btnCapNhat_Click" />
                    <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới" OnClick="btnLamMoi_Click" />
                    <asp:Button ID="btnXoaTaiKhoan" runat="server" Text="Xóa" OnClick="btnXoaTaiKhoan_Click" />
                </div>
            </div>
        

        <div class="DanhSachChucVu">
            <table style ="width: 800px ;margin:auto;">
                <tr>
                    <td>
                       <asp:GridView ID="gvCacChucVu" runat="server" AutoGenerateColumns="False" DataKeyNames="PK_sMaCV" DataSourceID="SqlDataSource2" Width="100%">
                           <Columns>
                               <asp:BoundField DataField="PK_sMaCV" HeaderText="Mã chức vụ" ReadOnly="True" SortExpression="PK_sMaCV" />
                               <asp:BoundField DataField="sTenCV" HeaderText="Tên chức vụ" SortExpression="sTenCV" />
                               <asp:BoundField DataField="fHSL" HeaderText="Hệ số lương" SortExpression="fHSL" />
                           </Columns>

                       </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:QLNSConnectionString %>" SelectCommand="SELECT * FROM [tbl_CHUCVU]"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
           
        </div>
        </div>
    </form>
</body>
</html>
