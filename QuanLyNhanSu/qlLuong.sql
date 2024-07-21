Create database Btl_CNPM
use Btl_CNPM

Create table tblLuongNV(
	sMaNV nvarchar(20),
	sTenNV nvarchar(50),
	fLuong float,
	fThuong float
)

INSERT INTO tblLuongNV 
Values ( 'NV001' , '????c' , 10000000 , 10000 ),
( 'NV002' , 'Trang' , 12000000 , 10000 ),
( 'NV003' , 'Ba?o' , 13000000 , 10000 ),
( 'NV004' , 'Vu?' , 14000000 , 10000 )

