<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanLyHopDong.aspx.cs" Inherits="QuanLyNhanSu.QuanLyHopDong.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="QuanLyHopDong.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
         <div id="contaner">
            <h1 >Quản lý hợp đồng nhân viên</h1>
            <div class="add_change">
                <div style="font-weight: bold">Thêm mới/Chỉnh sửa Tài khoản</div>
                <div>
                    <div>
                        <div>Mã hợp đồng</div>
                        <asp:TextBox ID="txtMaHopDong" runat="server"  ></asp:TextBox>
                    </div>
                    <div>
                        <div>Ngày đăng ký</div>
                        <asp:TextBox ID="DateNgayDangKy" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                    <div>
                        <div>Thời hạn</div>
                        <asp:TextBox ID="DateHetHan" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                     <div>
                        <div>Mã nhân viên
                         </div>
                        <asp:DropDownList ID="DropDownListMaNV" runat="server" DataSourceID="SqlDataSource3" DataTextField="PK_sMaNV" DataValueField="PK_sMaNV" >
                            </asp:DropDownList>
                         <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:QLNSConnectionString %>" SelectCommand="SELECT * FROM [tbl_NHANVIEN]"></asp:SqlDataSource>
                    </div>
                     <div>
                        <div>Mã chức vụ</div>
                         <asp:DropDownList ID="DropDownListMaCV" runat="server" DataSourceID="SqlDataSource4" DataTextField="PK_sMaCV" DataValueField="PK_sMaCV" >
                            </asp:DropDownList>
                         <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:QLNSConnectionString %>" SelectCommand="SELECT * FROM [tbl_CHUCVU]"></asp:SqlDataSource>
                    </div>
                     <div>
                        <div>Mã phòng ban</div>
                         <asp:DropDownList ID="DropDownListMaPB" runat="server" DataSourceID="SqlDataSource5" DataTextField="PK_sMaPB" DataValueField="PK_sMaPB" >
                            </asp:DropDownList>
                         <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:QLNSConnectionString %>" SelectCommand="SELECT * FROM [tbl_PHONGBAN]"></asp:SqlDataSource>
                    </div>
                     <div>
                        <div>Hệ số lương</div>
                        <asp:TextBox ID="TextBoxLươngCB" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnThemMoi" runat="server" Text="Thêm mới" OnClientClick="return validateInputs();" OnClick="btnThemMoi_Click" />
                    <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" OnClientClick="return validateInputs();" OnClick="btnCapNhat_Click" />
                    <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới" OnClick="btnLamMoi_Click" />
                    <asp:Button ID="btnXoaTaiKhoan" runat="server" Text="Xóa" OnClick="btnXoaTaiKhoan_Click" />
                </div>
            </div>
       

        <div class="DanhSachHopDong">
            <table style ="width: 800px ;margin:auto;">
                <tr>
                    <td>
                       <asp:GridView ID="gvCacHopDong" runat="server" AutoGenerateColumns="False" DataKeyNames="PK_sMaHD" DataSourceID="SqlDataSource1" Width="100%">
                           <Columns>
                               <asp:BoundField DataField="PK_sMaHD" HeaderText="Mã hợp đồng" ReadOnly="True" SortExpression="PK_sMaHD" />
                               <asp:BoundField DataField="dNgaykyhd" HeaderText="Ngày đăng ký" SortExpression="dNgaykyhd" DataFormatString="{0:yyyy-MM-dd}"/>
                               <asp:BoundField DataField="dThoihan" HeaderText="Thời hạn" SortExpression="dThoihan" DataFormatString="{0:yyyy-MM-dd}"/>
                               <asp:BoundField DataField="FK_sMaNV" HeaderText="Mã nhân viên" SortExpression="FK_sMaNV" />
                               <asp:BoundField DataField="FK_sMaCV" HeaderText="Mã chức vụ" SortExpression="FK_sMaCV" />
                               <asp:BoundField DataField="FK_sMaPB" HeaderText="Mã phòng ban" SortExpression="FK_sMaPB" />
                               <asp:BoundField DataField="fLuongcb" HeaderText="lương cơ bản" SortExpression="fLuongcb" />
                           </Columns>

                       </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:QLNSConnectionString %>" SelectCommand="SELECT * FROM [tbl_HOPDONG]"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:QLNSConnectionString %>" SelectCommand="SELECT * FROM [tbl_CHUCVU]"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
           
        </div>
         </div>
    </form>
</body>
</html>
