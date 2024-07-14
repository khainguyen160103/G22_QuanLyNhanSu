<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmDangNhap.aspx.cs" Inherits="QuanLyNhanSu.DangNhap.FrmDangNhap" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="../fontawesome-free-6.5.2-web/fontawesome-free-6.5.2-web/css/all.min.css" />
    <link rel="stylesheet" href="Login.css" />
</head>
<body>
    <div class="container">
        <i class="fa-solid fa-user"></i>
        <h2 class="title">Đăng nhập</h2>
        <form id="LoginForm" runat="server">
            <div class="form-group">
                <label for="txtTenTaiKhoan">Tên đăng nhập:</label>
                <input type="text" id="txtTenTaiKhoan" name="txtTenTaiKhoan" runat="server">
            </div>
            <p id="messageTK" runat="server"></p>
            <div class="form-group">
                <label for="txtMatKhau">Mật khẩu:</label>
                <input type="password" id="txtMatKhau" name="txtMatKhau" runat="server">
            </div>
            <p id="messageMK" runat="server"></p>
            <button type="submit" id="btnDangNhap" runat="server" name="btnDangNhap" onserverclick="btnDangNhap_Click">Đăng nhập</button>
        </form>
    </div>
</body>
</html>
