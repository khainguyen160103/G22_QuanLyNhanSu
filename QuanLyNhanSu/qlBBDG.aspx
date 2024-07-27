<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlBBDG.aspx.cs" Inherits="qlBienBanDanhGia.qlBBDG" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Quản Lý Biên Bản Đánh Giá</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .d-none { display: none; }
        .alert { margin-top: 15px; }
        table { width: 100%; }
        th, td { text-align: left; padding: 8px; }
        tr:nth-child(even) { background-color: #f2f2f2; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h1>Quản Lý Biên Bản Đánh Giá</h1>
            
            <!-- Phần thông báo -->
            <asp:Label ID="lblMessage" runat="server" CssClass="alert d-none"></asp:Label>

            <!-- Phần tìm kiếm -->
            <div class="form-group">
                <label for="txtSearch">Tìm kiếm:</label>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
            </div>

            <!-- Phần danh sách biên bản đánh giá -->
            <asp:GridView ID="gvBienBan" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="gvBienBan_RowCommand">
                <Columns>
                    <asp:BoundField DataField="dNgaylap" HeaderText="Ngày lập" />
                    <asp:BoundField DataField="sTenNV" HeaderText="Nhân viên" />
                    <asp:BoundField DataField="sNddanhgia" HeaderText="Nội dung đánh giá" />
                    <asp:TemplateField HeaderText="Hành động">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("PK_sMabienban") %>' Text="Sửa" CssClass="btn btn-primary" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("PK_sMabienban") %>' Text="Xóa" CssClass="btn btn-danger" OnClientClick='<%# "return confirmDelete(\"" + Eval("PK_sMabienban") + "\");" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- Phần thêm/sửa biên bản đánh giá -->
            <asp:Panel ID="pnlAddEdit" runat="server" CssClass="border p-4 bg-light d-none">
                <h2><asp:Label ID="lblAddEdit" runat="server" Text="Thêm Biên Bản Đánh Giá"></asp:Label></h2>
                <div class="form-group">
                    <label for="txtDate">Ngày lập:</label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="ddlEmployee">Nhân viên:</label>
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="txtContent">Nội dung đánh giá:</label>
                    <asp:TextBox ID="txtContent" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:HiddenField ID="hfBienBanID" runat="server" />
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Lưu" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" OnClick="btnCancel_Click" Text="Hủy" />
            </asp:Panel>

            <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary" OnClick="btnAddNew_Click" Text="Thêm Biên Bản Đánh Giá Mới" />
        </div>

        <script type="text/javascript">
            function confirmDelete(bienBanID) {
                return confirm("Bạn có chắc chắn muốn xóa biên bản đánh giá này không?");
            }
        </script>
    </form>
</body>
</html>
