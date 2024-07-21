<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlPhongBan.aspx.cs" Inherits="QuanLyNhanSu.qlPhongBan" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý phòng ban nhân sự</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
</head>
<body>
    <h1 class="text-center mt-3 mb-5">
        QUẢN LÝ PHÒNG BAN NHÂN SỰ
    </h1>
    <form id="form1" runat="server" class="container mt-6 d-block">
        <div class="form-group mb-6">
            <div class="col-md-6">
                <asp:Label ID="lblMaPB" runat="server" Text="Mã Phòng Ban" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtMaPB" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorMaPB" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblTenPB" runat="server" Text="Tên Phòng Ban" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtTenPB" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorTenPB" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblMota" runat="server" Text="Mô Tả" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtMota" runat="server" TextMode="MultiLine" Rows="3" Columns="50" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorMoTa" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="mt-3">
                <asp:Button ID="btnAdd" runat="server" Text="Thêm Mới" CssClass="btn btn-primary mr-3 d-inline-block" OnClick="btnAdd_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Huỷ" CssClass="btn btn-dark" OnClick="btnReset_Click" />
            </div>   
            <br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="my-6">
            <h2>Danh sách phòng ban</h2>
            <div class="container mb-3">
                <div class="row">
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:Label ID="lblSearch" runat="server" Text="Tìm kiếm tên Phòng Ban:" CssClass="form-label"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm Kiếm" OnClick="btnSearch_Click" CssClass="btn btn-info" />
                    </div>
                </div>
            </div>
            <asp:GridView ID="gv_phongBan" runat="server" AutoGenerateColumns="False" 
                OnRowEditing="gv_phongBan_RowEditing" 
                OnRowUpdating="gv_phongBan_RowUpdating"
                OnRowCancelingEdit="gv_phongBan_RowCancelingEdit"
                OnRowDeleting="gv_phongBan_RowDeleting"
                CssClass="table table-striped table-hover"
                DataKeyNames="PK_sMaPB">
                <Columns>
                    <asp:TemplateField HeaderText="Mã Phòng Ban">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMaPB" runat="server" Text='<%# Bind("PK_sMaPB") %>' CssClass="form-control disabled" aria-label="Disabled input example"  ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMaPB" runat="server" Text='<%# Bind("PK_sMaPB") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tên Phòng Ban">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTenPB" runat="server" Text='<%# Bind("sTenPB") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTenPB" runat="server" Text='<%# Bind("sTenPB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mô Tả">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMota" runat="server" Text='<%# Bind("sMota") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMota" runat="server" Text='<%# Bind("sMota") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hành động">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn btn-warning btn-sm">Sửa</asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-sm">Xóa</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success btn-sm">Cập Nhật</asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-secondary btn-sm">Hủy</asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
</html>
