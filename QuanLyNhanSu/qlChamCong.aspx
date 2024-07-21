<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlChamCong.aspx.cs" Inherits="QuanLyNhanSu.qlChamCong" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý chấm công</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 10px;
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
            padding: 10px;
            text-align: left;
        }
        th {
            background-color: #4CAF50;
            color: white;
        }
        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
        tr:hover {
            background-color: #ddd;
        }
        .action-buttons {
            display: flex;
            gap: 5px;
        }
        .action-buttons button {
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 5px;
        }
        .action-buttons button:hover {
            background-color: #45a049;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        .form-group input, .form-group select {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .form-actions {
            display: flex;
            gap: 10px;
        }
        .form-actions button {
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
        }
        .form-actions button:hover {
            background-color: #45a049;
        }
        .form-actions button.cancel {
            background-color: #f44336;
        }
        .form-actions button.cancel:hover {
            background-color: #e41f1f;
        }
    </style>
</head>
<body>
    <div class="container">
        <h2>Quản lý chấm công</h2>
        <form id="form1" runat="server">
            <div>
                <table>
                    <thead>
                        <tr>
                            <th>Mã NV</th>
                            <th>Tên NV</th>
                            <th>Ngày chấm công</th>
                            <th>Đi làm</th>
                            <th>Hành động</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%-- Nội dung sẽ được thêm động ở đây --%>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("sMaNV") %></td>
                                    <td><%# Eval("sTenNV") %></td>
                                    <td><%# Eval("dNgayChamCong", "{0:dd/MM/yyyy}") %></td>
                                    <td><%# Eval("bDiLam") %></td>
                                    <td class="action-buttons">
                                        <asp:Button ID="EditButton" runat="server" Text="Sửa" CommandName="Edit" CommandArgument='<%# Eval("sMaNV") + "~" + Eval("dNgayChamCong") %>' OnClick="EditButton_Click" />
                                        <asp:Button ID="DeleteButton" runat="server" Text="Xóa" CommandName="Delete" CommandArgument='<%# Eval("sMaNV") + "~" + Eval("dNgayChamCong") %>' OnClick="DeleteButton_Click" />

                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>

                <h3>Thêm/Sửa Chấm Công</h3>
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
                    <asp:Label ID="lblNgayChamCong" runat="server" Text="Ngày chấm công:" AssociatedControlID="txtNgayChamCong" />
                    <asp:TextBox ID="txtNgayChamCong" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblDiLam" runat="server" Text="Đi làm:" AssociatedControlID="ddlDiLam" />
                    <asp:DropDownList ID="ddlDiLam" runat="server">
                        <asp:ListItem Text="Có" Value="1" />
                        <asp:ListItem Text="Không" Value="0" />
                    </asp:DropDownList>
                </div>
                <div class="form-actions">
                    <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn btn-success" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Hủy" CssClass="btn btn-danger cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </form>
    </div>
</body>
</html>