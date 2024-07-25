<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlTaiKhoan.aspx.cs" Inherits="quanLyTaiKhoanNV.qlTaiKhoan" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Quản lý tài khoản</title>
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
        <h1>Quản lý tài khoản</h1>
        <form id="form1" runat="server">
            <div class="search-group">
                <asp:Label ID="lblSearch" runat="server" Text="Tìm kiếm theo Mã tài khoản:" />
                <asp:TextBox ID="txtSearchMaTK" runat="server" CssClass="form-control" />
                <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
            </div>
            <table>
                <thead>
                    <tr>
                        <th>Mã tài khoản</th>
                        <th>Tên tài khoản</th>
                        <th>Mật khẩu</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("sMaTK") %></td>
                                <td><%# Eval("sTenTK") %></td>
                                <td><%# Eval("sMatKhau") %></td>
                                <td class="action-buttons">
                                    <asp:Button ID="EditButton" runat="server" Text="Sửa" CommandName="Edit" CommandArgument='<%# Eval("sMaTK") %>' OnClick="EditButton_Click"  />
                                    <asp:Button ID="DeleteButton" runat="server" Text="Xóa" CommandName="Delete" CommandArgument='<%# Eval("sMaTK") %>' OnClick="DeleteButton_Click" OnClientClick="return confirmAction('Bạn có chắc chắn muốn xóa thông tin này?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <h3>Thêm/Sửa Tài Khoản</h3>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            <div class="form-group">
                <asp:Label ID="lblMaTK" runat="server" Text="Mã tài khoản:" AssociatedControlID="txtMaTK" />
                <asp:TextBox ID="txtMaTK" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblTenTK" runat="server" Text="Tên tài khoản:" AssociatedControlID="txtTenTK" />
                <asp:TextBox ID="txtTenTK" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblMatKhau" runat="server" Text="Mật khẩu:" AssociatedControlID="txtMatKhau" />
                <asp:TextBox ID="txtMatKhau" runat="server" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="form-actions">
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="save" OnClick="btnSave_Click" OnClientClick="return confirmSave();" />
                <asp:Button ID="btnCancel" runat="server" Text="Hủy" CssClass="cancel" OnClick="btnCancel_Click" />
            </div>
        </form>
    </div>
</body>
    <script type="text/javascript">
        function confirmAction(message) {
            return confirm(message);
        }

        function validateForm() {
            var maTK = document.getElementById('<%= txtMaTK.ClientID %>').value.trim();
        var tenTK = document.getElementById('<%= txtTenTK.ClientID %>').value.trim();
        var matKhau = document.getElementById('<%= txtMatKhau.ClientID %>').value.trim();

        if (maTK === '') {
            alert('Mã tài khoản không được để trống.');
            return false;
        }
        if (tenTK === '') {
            alert('Tên tài khoản không được để trống.');
            return false;
        }
        if (matKhau === '') {
            alert('Mật khẩu không được để trống.');
            return false;
        }

        return true;
    }

    function confirmSave() {
        if (!validateForm()) {
            return false;
        }
        var action = document.getElementById('<%= btnSave.ClientID %>').getAttribute('data-editmode') === 'true' ? 'sửa' : 'thêm mới';
            return confirm('Bạn có chắc chắn muốn ' + action + ' thông tin này?');
        }
</script>
</html>