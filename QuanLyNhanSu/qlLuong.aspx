<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlLuong.aspx.cs" Inherits="QuanLyNhanSu.qlLuong" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Quản lý lương nhân viên</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f2f5;
            margin: 0;
            padding: 20px;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        h1 {
            text-align: center;
            color: #333;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        table, th, td {
            border: 1px solid #ddd;
        }
        th, td {
            padding: 12px;
            text-align: left;
        }
        th {
            background-color: #f4f4f4;
        }
        tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        .action-buttons {
            display: flex;
            gap: 5px;
        }
        .action-buttons button {
            padding: 5px 10px;
            border: none;
            background-color: #007bff;
            color: #fff;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .action-buttons button:hover {
            background-color: #0056b3;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        .form-group input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        .form-actions {
            display: flex;
            justify-content: flex-end;
            gap: 10px;
        }
        .form-actions button {
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .form-actions button.save {
            background-color: #28a745;
            color: #fff;
        }
        .form-actions button.save:hover {
            background-color: #218838;
        }
        .form-actions button.cancel {
            background-color: #dc3545;
            color: #fff;
        }
        .form-actions button.cancel:hover {
            background-color: #c82333;
        }
        .search-group {
            margin-bottom: 20px;
        }
        .search-group input {
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
    </style>
    <script type="text/javascript">
        function confirmAction(message) {
            return confirm(message);
        }
    </script>
</head>
<body>
    <div class="container">
        <h1>Quản lý lương nhân viên</h1>
        <form id="form1" runat="server">
            <div class="search-group">
                <asp:Label ID="lblSearch" runat="server" Text="Tìm kiếm theo Mã NV:" />
                <asp:TextBox ID="txtSearchMaNV" runat="server" CssClass="form-control" />
                <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
            </div>
            <table>
                <thead>
                    <tr>
                        <th>Mã NV</th>
                        <th>Tên NV</th>
                        <th>Lương Cơ Bản</th>
                        <th>Thưởng</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("sMaNV") %></td>
                                <td><%# Eval("sTenNV") %></td>
                                <td><%# Eval("fLuongCB") %></td>
                                <td><%# Eval("fThuong") %></td>
                                <td class="action-buttons">
                                    <asp:Button ID="EditButton" runat="server" Text="Sửa" CommandName="Edit" CommandArgument='<%# Eval("sMaNV") %>' OnClick="EditButton_Click"  />
                                    <asp:Button ID="DeleteButton" runat="server" Text="Xóa" CommandName="Delete" CommandArgument='<%# Eval("sMaNV") %>' OnClick="DeleteButton_Click" OnClientClick="return confirmAction('Bạn có chắc chắn muốn xóa thông tin này?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <h3>Thêm/Sửa Nhân Viên</h3>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            <div class="form-group">
                <asp:Label ID="lblMaNV" runat="server" Text="Mã NV:" AssociatedControlID="txtMaNV" />
                <asp:TextBox ID="txtMaNV" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblTenNV" runat="server" Text="Tên NV:" AssociatedControlID="txtTenNV" />
                <asp:TextBox ID="txtTenNV" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblLuongCB" runat="server" Text="Lương Cơ Bản:" AssociatedControlID="txtLuongCB" />
                <asp:TextBox ID="txtLuongCB" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblThuong" runat="server" Text="Thưởng:" AssociatedControlID="txtThuong" />
                <asp:TextBox ID="txtThuong" runat="server" CssClass="form-control" />
            </div>
            <div class="form-actions">
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="save" OnClick="btnSave_Click" OnClientClick="return confirmSave();" />
                <asp:Button ID="btnCancel" runat="server" Text="Hủy" CssClass="cancel" OnClick="btnCancel_Click" />
            </div>
        </form>
    </div>
</body>
    <script type="text/javascript">
        function confirmSave() {
            var btnSave = document.getElementById('<%= btnSave.ClientID %>');
            var isEditMode = btnSave.getAttribute('data-editmode') === 'true';
            var action = isEditMode ? 'sửa' : 'thêm mới';
            return confirm('Bạn có chắc chắn muốn ' + action + ' thông tin này?');
        }

        function confirmDelete() {
            return confirm('Bạn có chắc chắn muốn xóa thông tin này?');
        }
</script>
</html>
