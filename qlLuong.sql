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

CREATE TABLE tblChamCong (
    sMaNV NVARCHAR(50) PRIMARY KEY,
    sTenNV NVARCHAR(100),
    dNgayChamCong DATE,
    bDiLam BIT
);


INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV001', N'Nguy?n V?n A', '2023-07-01', 1);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV002', N'Tr?n Th? B', '2023-07-01', 1);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV003', N'Lê V?n C', '2023-07-01', 0);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV004', N'Ph?m Th? D', '2023-07-01', 1);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV005', N'Nguy?n V?n A', '2023-07-02', 0);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV006', N'Tr?n Th? B', '2023-07-02', 1);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV007', N'Lê V?n C', '2023-07-02', 1);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV008', N'Ph?m Th? D', '2023-07-02', 0);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV009', N'Nguy?n V?n A', '2023-07-03', 1);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV010', N'Tr?n Th? B', '2023-07-03', 0);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV011', 'Lê V?n C', '2023-07-03', 1);
INSERT INTO tblChamCong (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV012', 'Ph?m Th? D', '2023-07-03', 1);
