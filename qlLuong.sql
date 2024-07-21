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

CREATE TABLE tblChamCongNV (
    sMaNV NVARCHAR(50),
    sTenNV NVARCHAR(100),
    dNgayChamCong DATE,
    bDiLam BIT
);


INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV001', N'Nguyê?n V?n A', '2023-07-01', 1);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV002', N'Trâ?n Thi? B', '2023-07-01', 1);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV003', N'Lê V?n C', '2023-07-01', 0);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV004', N'Pha?m Thi? D', '2023-07-01', 1);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV001', N'Nguyê?n V?n A', '2023-07-02', 0);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV002', N'Trâ?n Thi? B', '2023-07-02', 1);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV003', N'Lê V?n C', '2023-07-02', 1);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV004', N'Pha?m Thi? D', '2023-07-02', 0);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV001', N'Nguyê?n V?n A', '2023-07-03', 1);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV002', N'Trâ?n Thi? B', '2023-07-03', 0);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV003', N'Lê V?n C', '2023-07-03', 1);
INSERT INTO tblChamCongNV (sMaNV, sTenNV, dNgayChamCong, bDiLam) VALUES ('NV004', N'Pha?m Thi? D', '2023-07-03', 1);
