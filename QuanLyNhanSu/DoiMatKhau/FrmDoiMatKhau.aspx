<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmDoiMatKhau.aspx.cs" Inherits="QuanLyNhanSu.DoiMatKhau.FrmDoiMatKhau" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="../fontawesome-free-6.5.2-web/fontawesome-free-6.5.2-web/css/all.min.css" />
    <link rel="stylesheet" href="DoiMatKhau.css" />
</head>
<body>
    <div class="container">
        <i class="fa-solid fa-user"></i>
        <h2 class="title">Đổi mật khẩu</h2>
        <form id="LoginForm" runat="server">
            <div class="form-group">
                <label for="txtMatKhauCu">Mật khẩu cũ</label>
                <input type="text" id="txtMatKhauCu" name="txtMatKhauCu" runat="server">
                <p id="messageMK1" runat="server"></p>
            </div>

            <div class="form-group">
                <label for="txtMatKhauMoi">Mật khẩu mới</label>
                <input type="password" id="txtMatKhauMoi" name="txtMatKhauMoi" runat="server">
                <p id="messageMK2" runat="server"></p>
            </div>
            <div class="form-group">
                <label for="txtNhapLaiMatKhauMoi">Nhập lại mật khẩu </label>
                <input type="password" id="txtNhapLaiMatKhauMoi" name="txtNhapLaiMatKhauMoi" runat="server">
                <p id="messageMK3" runat="server"></p>
            </div>

            <button type="submit" id="btnDoiMatKhau" name="btnDoiMatKhau" onserverclick="btnDoiMatKhau_Click" runat="server">Đổi mật khẩu</button>
        </form>
    </div>
</body>
</html>
