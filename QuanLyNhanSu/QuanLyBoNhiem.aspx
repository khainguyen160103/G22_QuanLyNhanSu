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


        </style>
</head>
<body>
    <div class="container">
        <h1>Quyết định bổ nhiệm</h1>
          <div id="notification" runat="server" class="notification"></div>
        <div class="column">
            <label for="nhanvien">Nhân viên:</label>
            <input runat="server" type="text" id="nhanvien" name="nhanvien">
            <label for="chucvu">Chức vụ mới:</label>
            <input  runat="server" type="text" id="chucvu" name="chucvu">
            <label for="phongban">Phòng ban mới:</label>
            <input  runat="server" type="text" id="phongban" name="phongban">
            <label for="luong">Mức lương mới:</label>
            <input  runat="server" type="text" id="luong" name="luong">
            <label for="ngaycohieuluc">Ngày có hiệu lực:</label>
            <input  runat="server" type="date" id="ngaycohieuluc" name="ngaycohieuluc">
        </div>
      <div class="column">
    <label for="noidung">Nội dung:</label>
    <textarea runat="server" id="noidung" name="noidung"></textarea>
    <button type="button" runat="server" id="btnXoa" OnClick="btnXoa_Click">Xóa</button>
    <button type="button" runat="server" id="btnThemMoi" OnClick="btnThemMoi_Click">Thêm mới</button>
    <button type="button" runat="server" id="btnCapNhat" OnClick="btnCapNhat_Click">Cập nhật</button>
    <button type="button" runat="server" id="btnLamMoi" OnClick="btnLamMoi_Click">Làm mới</button>
</div>
        <div class="column">
            <label for="mabonhiem">Nhập mã bổ nhiệm để tìm kiếm hoặc xóa:</label>
             <input type="text" runat="server" id="mabonhiem" name="mabonhiem">
             <button type="button" runat="server" id="btnTimKiem" OnClick="btnTimKiem_Click">Tìm kiếm</button>
        </div>
    </div>
</body>
</html>
