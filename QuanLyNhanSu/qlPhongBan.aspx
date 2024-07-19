<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlPhongBan.aspx.cs" Inherits="QuanLyNhanSu.qlPhongBan" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý phòng ban nhân sự.</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <script type="text/javascript">
        function resetForm() {
            document.getElementById('<%= txtName.ClientID %>').value = '';
            document.getElementById('<%= txtDescription.ClientID %>').value = '';
        }
    </script>
</head>
<body>
    <form id="mainForm" runat="server">
        <div class="container">
            <h1 class="text-center mt-4">Quản lý phòng ban nhân sự</h1>
            <div class="mt-3">
                <div class="mb-3 col-md-6">
                    <label for="txtName" class="form-label">Tên phòng ban</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Nhập tên phòng ban" />
                    <asp:Label ID="lblErrorName" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="mb-3 col-md-6">
                    <label for="txtDescription" class="form-label">Mô tả</label>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" />
                    <asp:Label ID="lblErrorDescription" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary mb-3" Text="Thêm" OnClick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success mb-3" Text="Cập nhật" OnClick="btnUpdate_Click" Visible="false" />
                <asp:HiddenField ID="hfEditIndex" runat="server" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info mb-3" Text="Huỷ" OnClientClick="resetForm(); return false;" />
            </div>
            <div>
                <h2>Danh sách phòng ban</h2>
                <div class="mb-3 col-md-6">
                    <label for="search" class="form-label">Tìm kiếm phòng ban</label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Nhập từ khoá" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-secondary mt-2" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                </div>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Tên phòng ban</th>
                            <th>Mô tả</th>
                            <th>Hành động</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="departmentRepeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Name") %></td>
                                    <td><%# Eval("Description") %></td>
                                    <td>
                                        <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-warning" Text="Sửa" CommandArgument='<%# Container.ItemIndex %>' OnClick="btnEdit_Click" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Xoá" CommandArgument='<%# Container.ItemIndex %>' OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
</body>
</html>
