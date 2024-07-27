<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlNV.aspx.cs" Inherits="qlNV.qlNV" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý nhân viên</title>
    <!-- Link đến Bootstrap CSS để tạo giao diện hiện đại -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <style>
        .action-btns button {
            margin-right: 5px;
        }
        .d-none {
            display: none !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4">Quản lý nhân viên</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="alert d-none" EnableViewState="False" role="alert"></asp:Label>
            
            <div class="form-group">
                <label for="txtSearch">Tìm kiếm:</label>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Nhập từ khóa tìm kiếm" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
            </div>
            <!-- Bảng HTML để hiển thị danh sách nhân viên -->
            <table class="table table-bordered">
                <thead class="table-dark">
                    <tr>
                        <th>Mã NV</th>
                        <th>Tên NV</th>
                        <th>Ngày sinh</th>
                        <th>Giới tính</th>
                        <th>CCCD</th>
                        <th>Địa chỉ</th>
                        <th>SĐT</th>
                        <th>Email</th>
                        <th>Ngày vào làm</th>
                        <th>Lương CB</th>
                        <th>Tình trạng</th>
                        <th>Chức vụ</th>
                        <th>Phòng ban</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    <!-- Dữ liệu nhân viên sẽ được tải từ code-behind -->
                    <asp:Repeater ID="rptEmployees" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("PK_sMaNV") %></td>
                                <td><%# Eval("sTenNV") %></td>
                                <td><%# Eval("dNgaysinh", "{0:dd/MM/yyyy}") %></td>
                                <td><%# Eval("sGioitinh") %></td>
                                <td><%# Eval("sCCCD") %></td>
                                <td><%# Eval("sDiachia") %></td>
                                <td><%# Eval("sSDT") %></td>
                                <td><%# Eval("sEmail") %></td>
                                <td><%# Eval("dNgayvaolam", "{0:dd/MM/yyyy}") %></td>
                                <td><%# Eval("fLuongcb", "{0:N0}") %></td>
                                <td><%# Eval("sTinhtrang") %></td>
                                <td><%# Eval("sTenCV") %></td>
                                <td><%# Eval("sTenPB") %></td>
                                <td class="action-btns">
                                    <asp:Button ID="btnEdit" runat="server" Text="Sửa" CssClass="btn btn-warning btn-sm" CommandName="Edit" CommandArgument='<%# Eval("PK_sMaNV") %>' OnClick="btnEdit_Click" />
                                    <asp:Button 
                                        ID="btnDelete" 
                                        runat="server" 
                                        Text="Xóa" 
                                        CssClass="btn btn-danger btn-sm" 
                                        OnClientClick="return confirmDelete();" 
                                        CommandArgument='<%# Eval("PK_sMaNV") %>' 
                                        OnClick="btnDelete_Click" />                                
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <!-- Nút để mở form thêm nhân viên -->
            <asp:Button ID="btnAddNew" runat="server" Text="Thêm nhân viên" CssClass="btn btn-primary mb-3" OnClick="btnAddNew_Click" />
            <asp:Button ID="btnRefesh" runat="server" Text="Làm mới" CssClass="btn btn-primary mb-3" OnClick="btnRefesh_Click" />

            <!-- Form thêm/sửa nhân viên -->
            <asp:Panel ID="pnlAddEdit" runat="server" CssClass="border p-4 bg-light d-none">
                <h3><%# string.IsNullOrEmpty(hfEmployeeID.Value) ? "Thêm mới nhân viên" : "Sửa thông tin nhân viên" %></h3>
                <asp:HiddenField ID="hfEmployeeID" runat="server" />
                <div class="mb-3">
                    <label for="txtMaNV" class="form-label">Mã nhân viên</label>
                    <asp:TextBox ID="txtMaNV" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtName" class="form-label">Tên nhân viên</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtBirthDate" class="form-label">Ngày sinh</label>
                    <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="ddlGender" class="form-label">Giới tính</label>
                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Nam" Value="Nam"></asp:ListItem>
                        <asp:ListItem Text="Nữ" Value="Nữ"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="txtIDCard" class="form-label">CCCD</label>
                    <asp:TextBox ID="txtIDCard" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtAddress" class="form-label">Địa chỉ</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtPhone" class="form-label">Số điện thoại</label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtStartDate" class="form-label">Ngày vào làm</label>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtSalary" class="form-label">Lương cơ bản</label>
                    <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtStatus" class="form-label">Tình trạng</label>
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtPositionID" class="form-label">Mã chức vụ</label>
                    <asp:TextBox ID="txtPositionID" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtDepartmentID" class="form-label">Mã phòng ban</label>
                    <asp:TextBox ID="txtDepartmentID" runat="server" CssClass="form-control" />
                </div>
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn btn-success" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Hủy bỏ" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />

                </div>
            </asp:Panel>
        </div>
    </form>

    <!-- Link đến jQuery và Bootstrap JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
    <script>
        $(document).ready(function () {
            // Hiển thị thông báo sau khi lưu hoặc xóa
            var lblMessage = $("#<%= lblMessage.ClientID %>");
            if (lblMessage.text().trim() !== "") {
                lblMessage.removeClass("d-none");
                setTimeout(function () {
                    lblMessage.addClass("d-none");
                }, 3000);
            }
        });

            // Hàm xác nhận trước khi xóa nhân viên
            function confirmDelete() {
            return confirm("Bạn có chắc chắn muốn xóa nhân viên này không?");
        }
   
    </script>
</body>
</html>