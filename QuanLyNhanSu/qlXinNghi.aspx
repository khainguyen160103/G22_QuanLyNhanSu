<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qlXinNghi.aspx.cs" Inherits="QuanLyNhanSu.qlXinNghi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
</head>
<body>
    <h1 class="text-center mt-3 mb-5">
        QUẢN LÝ XIN NGHỈ NHÂN SỰ
    </h1>
    <form id="form1" runat="server" class="container mt-6 d-block">
        <h2>Tạo đơn xin nghỉ</h2>
          <div class="form-group my-6">
            <div class="col-md-6">
                <asp:Label ID="lblMaDon" runat="server" Text="Mã đơn" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtMaDon" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorMaDon" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblNgayLap" runat="server" Text="Ngày lập" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtNgayLap" runat="server" placeholder="yyyy-MM-dd" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorNgayLap" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblLoaiDon" runat="server" Text="Loại đơn" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtLoaiDon" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorLoaiDon" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblNgayBD" runat="server" Text="Ngày bắt đầu" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtNgayBD" runat="server" placeholder="yyyy-MM-dd" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorNgayBD" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblNgayKT" runat="server" Text="Ngày kết thúc" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtNgayKT" runat="server" placeholder="yyyy-MM-dd" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorNgayKT" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblMaNV" runat="server" Text="Mã nhân viên" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtMaNV" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorMaNV" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblLyDo" runat="server" Text="Lý do" CssClass="form-label mb-2 d-block"></asp:Label>
                <asp:TextBox ID="txtLyDo" runat="server" Rows="3" Columns="50" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblErrorLyDo" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="mt-3">
                <asp:Button ID="btnAdd" runat="server" Text="Thêm Mới" CssClass="btn btn-primary mr-3 d-inline-block" OnClick="btnAdd_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Huỷ" CssClass="btn btn-dark" OnClick="btnReset_Click" />
            </div>   
            <br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="my-6">
            <h2>Danh sách đơn xin nghỉ</h2>
            <div class="container mb-3">
                <div class="row">
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:Label ID="lblSearch" runat="server" Text="Tìm kiếm mã đơn:" CssClass="form-label"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm Kiếm" OnClick="btnSearch_Click" CssClass="btn btn-info" />
                    </div>
                </div>
            </div>
            <asp:GridView ID="gv_donXinNghi" runat="server" AutoGenerateColumns="False" 
                OnRowEditing="gv_donXinNghi_RowEditing" 
                OnRowUpdating="gv_donXinNghi_RowUpdating"
                OnRowCancelingEdit="gv_donXinNghi_RowCancelingEdit"
                OnRowDeleting="gv_donXinNghi_RowDeleting"
                CssClass="table table-striped table-hover"
                DataKeyNames="PK_sMaDon">
                <Columns>
                    <asp:TemplateField HeaderText="Mã đơn">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMaDon" runat="server" Text='<%# Bind("PK_sMaDon") %>' CssClass="form-control disabled" aria-label="Disabled input example"  ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMaDon" runat="server" Text='<%# Bind("PK_sMaDon") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày lập">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNgayLap" runat="server" Text='<%# Bind("dNgaylap") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNgayLap" runat="server" Text='<%# Bind("dNgaylap") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Loại đơn">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLoaiDon" runat="server" Text='<%# Bind("sLoaidon") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLoaiDon" runat="server" Text='<%# Bind("sLoaidon") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày bắt đầu">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNgayBD" runat="server" Text='<%# Bind("dNgaybatdau") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNgayBD" runat="server" Text='<%# Bind("dNgaybatdau") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày kết thúc">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNgayKT" runat="server" Text='<%# Bind("dNgayketthuc") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNgayKT" runat="server" Text='<%# Bind("dNgayketthuc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mã nhân viên">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMaNV" runat="server" Text='<%# Bind("FK_sMaNV") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMaNV" runat="server" Text='<%# Bind("FK_sMaNV") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lý do">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLyDo" runat="server" Text='<%# Bind("sLydo") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLyDo" runat="server" Text='<%# Bind("sLydo") %>'></asp:Label>
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
