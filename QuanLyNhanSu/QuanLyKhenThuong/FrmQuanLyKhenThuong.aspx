<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmQuanLyKhenThuong.aspx.cs" Inherits="QuanLyNhanSu.FrmQuanLyKhenThuong" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .container{
            display:flex;
            flex-direction:column;
        }
        .container>div{
            display:flex;
            flex-direction:row;
            margin-top:15px;
        }
        input{
            width:200px;
            height:15px;
            margin-left:20px;
        }
        #Button1{
            margin-right:10px;
            width:50px;
            height:25px;
        }
        #Button2{
         margin-right:10px;
         width:50px;
          height:25px;
        }
        form{
            margin-bottom:20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div>
                <label for="MaKT">Mã khen thưởng : </label>
                <input type="text" id="MaKT" runat="server" />
            </div>

            <div>
                <label for="LoaiDon">Loại đơn : </label>
                <input type="text" id="LoaiDon" runat="server" />
            </div>

            <div>
                <label for="MaNV">Mã nhân viên : </label>
                <input type="text" runat="server" id="MaNV"/>
            </div>

            <div>
                <label for="MucKhenThuong">Mức khen thưởng kỉ luật : </label>
                <input type="text" runat="server" id="MucKhenThuong" />
            </div>

            <div>
                <label for="LyDo">Lý do : </label>
                <input type="text" runat="server" id="LyDo" />
            </div>
        </div>
        <br />
        <asp:Label ID="Label1" runat="server" Text="" BackColor="Red"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Thêm" OnClick="Button1_Click"/>
        <asp:Button ID="Button2" runat="server" Text="Reset" OnClick="Button2_Click"/>
    </form>
    <asp:Table ID="Table1" runat="server" BorderColor="Black" BorderWidth="1px" CellSpacing="20" >
        <asp:TableRow>
            <asp:TableHeaderCell Text="Mã khen thưởng"/>
            <asp:TableHeaderCell Text="Ngày lập" />
            <asp:TableHeaderCell Text="Loại đơn" />
            <asp:TableHeaderCell Text="Mã nhân viên" />
            <asp:TableHeaderCell Text="Mức khen thưởng kỉ luật" />
            <asp:TableHeaderCell Text="Lý do" />
        </asp:TableRow>
    </asp:Table>
    
</body>
</html>

