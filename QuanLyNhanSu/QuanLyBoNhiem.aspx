    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanLyBoNhiem.aspx.cs" Inherits="QuanLyNhanSu.QuanLyNhanSu" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
         <style>
        .notification {
        margin-bottom: 20px;
        padding: 10px;
        border-radius: 5px;
        display: none; /* Ẩn thông báo mặc định */
    }
    .notification.success {
        background-color: #dff0d8;
        color: #3c763d;
    }
    .notification.error {
        background-color: #f2dede;
        color: #a94442;
    }
                .container {
                    width: 80%;
                    margin: 0 auto;
                    display: flex;
                }

                .column {
                    flex: 1;
                    padding: 20px;
                }

                label {
                    display: block;
                    margin-bottom: 5px;
                }

                input[type="text"], textarea {
                    width: 100%;
                    padding: 10px;
                    margin-bottom: 10px;
                }
                 .column {
                flex: 1;
                padding: 20px;
            }

            label {
                display: block;
                margin-bottom: 5px;
            }

            input[type="text"], textarea {
                width: 100%;
                padding: 10px;
                margin-bottom: 10px;
            }


            </style>
    </head>
    <body>
        <form runat="server">
            <div class="container">
                <h1>Quyết định bổ nhiệm</h1>
                <div id="notification" runat="server" class="notification"></div>
                <div class="column">
                    <label for="nhanvien">Nhân viên:</label>
                    <asp:TextBox runat="server" ID="nhanvien" />
                    <label for="chucvu">Chức vụ mới:</label>
                    <asp:TextBox runat="server" ID="chucvu" />
                    <label for="phongban">Phòng ban mới:</label>
                    <asp:TextBox runat="server" ID="phongban" />
                    <label for="luong">Mức lương mới:</label>
                    <asp:TextBox runat="server" ID="luong" />
                    <label for="ngaycohieuluc">Ngày có hiệu lực:</label>
                    <asp:TextBox runat="server" ID="ngaycohieuluc" />
                </div>
                <div class="column">
                    <label for="noidung">Nội dung:</label>
                    <asp:TextBox runat="server" ID="noidung" TextMode="MultiLine" Rows="5" />
                    <asp:Button runat="server" ID="btnXoa" Text="Xóa" OnClick="btnXoa_Click" />
                    <asp:Button runat="server" ID="btnThemMoi" Text="Thêm mới" OnClick="btnThemMoi_Click" />
                    <asp:Button runat="server" ID="btnCapNhat" Text="Cập nhật" OnClick="btnCapNhat_Click" />
                    <asp:Button runat="server" ID="btnLamMoi" Text="Làm mới" OnClick="btnLamMoi_Click" />
                </div>
                <div class="column">
                    <label for="mabonhiem">Nhập mã bổ nhiệm để tìm kiếm hoặc xóa:</label>
                    <asp:TextBox runat="server" ID="mabonhiem" />
                    <asp:Button runat="server" ID="btnTimKiem" Text="Tìm kiếm" OnClick="btnTimKiem_Click" />
                </div>
                <div class="column">
                    <h2>Danh sách bổ nhiệm</h2>
                    <asp:GridView ID="GridViewBoNhiem" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="PK_sMabonhiem" HeaderText="Mã bổ nhiệm" />
                            <asp:BoundField DataField="dNgaylap" HeaderText="Ngày lập" />
                            <asp:BoundField DataField="dNgaycohieuluc" HeaderText="Ngày có hiệu lực" />
                            <asp:BoundField DataField="FK_sMaNV" HeaderText="Mã nhân viên" />
                            <asp:BoundField DataField="FK_sMaCV" HeaderText="Mã chức vụ" />
                            <asp:BoundField DataField="FK_sMaPB" HeaderText="Mã phòng ban" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </form>
    </body>
    </html>
