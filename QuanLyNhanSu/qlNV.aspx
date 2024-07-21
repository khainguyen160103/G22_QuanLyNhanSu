<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qlNV.aspx.cs" Inherits="qlNV.qlNV" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý nhân viên</title>
    <link href="qlNV.css" rel="stylesheet" type="text/css" />
    <script src="qlNV.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <h1 class="header">Quản lý nhân viên</h1>
            <div class="header2">
                <div class="form-group">
                    <label for="txtMaNhanVien">Mã nhân viên:</label>
                    <asp:TextBox ID="txtMaNhanVien" runat="server" CssClass="form-control1"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="imgNhanVien">Ảnh nhân viên:</label>
                    <asp:FileUpload ID="imgNhanVien" runat="server" CssClass="form-control1" OnChange="previewImage()" />
                    <asp:Image ID="previewImg" runat="server" Style="display:none;" />
                </div>
            </div>
            <div class="row">
                <!-- Cột 1 -->
                <div class="column">
                    <div class="form-group">
                        <label for="txtTenNhanVien">Tên nhân viên:</label>
                        <asp:TextBox ID="txtTenNhanVien" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ddlGioiTinh">Giới tính:</label>
                        <asp:DropDownList ID="ddlGioiTinh" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Nam" Value="Nam"></asp:ListItem>
                            <asp:ListItem Text="Nữ" Value="Nữ"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="txtCCCD">CCCD:</label>
                        <asp:TextBox ID="txtCCCD" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtNgayVaoLam">Ngày vào làm:</label>
                        <asp:TextBox ID="txtNgayVaoLam" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="ceNgayVaoLam" runat="server" TargetControlID="txtNgayVaoLam" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    </div>
                    <div class="form-group">
                        <label for="txtPhongBan">Phòng ban:</label>
                        <asp:TextBox ID="txtPhongBan" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ddlTinhTrang">Tình trạng:</label>
                        <asp:DropDownList ID="ddlTinhTrang" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Đang làm việc" Value="Đang làm việc"></asp:ListItem>
                            <asp:ListItem Text="Nghỉ việc" Value="Nghỉ việc"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Cột 2 -->
                <div class="column">
                    <div class="form-group">
                        <label for="txtDiaChi">Địa chỉ:</label>
                        <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtSDT">SĐT:</label>
                        <asp:TextBox ID="txtSDT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtChucVu">Chức vụ:</label>
                        <asp:TextBox ID="txtChucVu" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtLuong">Lương:</label>
                        <asp:TextBox ID="txtLuong" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!-- Cột 3 -->
                <div class="column">
                    <asp:GridView ID="gvNhanVien" runat="server" AutoGenerateColumns="False" CssClass="table" OnSelectedIndexChanged="gvNhanVien_SelectedIndexChanged" AutoGenerateSelectButton="True">
                        <Columns>
                            <asp:BoundField DataField="MaNhanVien" HeaderText="Mã nhân viên" />
                            <asp:BoundField DataField="TenNhanVien" HeaderText="Tên nhân viên" />
                            <asp:BoundField DataField="ChucVu" HeaderText="Chức vụ" />
                            <asp:BoundField DataField="TinhTrang" HeaderText="Tình trạng" />
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="buttons">
                <asp:Button ID="btnThem" runat="server" Text="Thêm" CssClass="button" OnClick="btnThem_Click" />
                <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" CssClass="button" OnClick="btnCapNhat_Click" />
                <asp:Button ID="btnXoa" runat="server" Text="Xóa" CssClass="button" OnClick="btnXoa_Click" />
                <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới" CssClass="button" OnClick="btnLamMoi_Click" />
                <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="button" OnClick="btnTimKiem_Click" />
            </div>
        </div>
    </form>
</body>
</html>
