CREATE DATABASE QLNS

USE QLNS
GO

CREATE TABLE tbl_PHONGBAN
(
    PK_sMaPB VARCHAR(10) NOT NULL,
    sTenPB NVARCHAR(20),
    sMota NVARCHAR(50),
    PRIMARY KEY(PK_sMaPB)
)

CREATE TABLE tbl_CHUCVU
(
    PK_sMaCV VARCHAR(10) NOT NULL,
    sTenCV NVARCHAR(20),
    fHSL FLOAT,
    PRIMARY KEY (PK_sMaCV)
)

CREATE TABLE tbl_NHANVIEN
(
    PK_sMaNV VARCHAR(10) NOT NULL,
    sTenNV NVARCHAR(30) NOT NULL,
    dNgaysinh DATETIME,
    sGioitinh NVARCHAR(3),
    sCCCD VARCHAR(12),
    sDiachia NVARCHAR(20),
    sSDT VARCHAR(10),
    sEmail VARCHAR(20),
    dNgayvaolam DATETIME,
    fLuongcb FLOAT,
    sTinhtrang NVARCHAR(20),
    FK_sMaCV VARCHAR(10),
    FK_sMaPB VARCHAR(10),
    PRIMARY KEY (PK_sMaNV),
    FOREIGN KEY (FK_sMaCV) REFERENCES tbl_CHUCVU(PK_sMaCV),
    FOREIGN key (FK_sMaPB) REFERENCES tbl_PHONGBAN(PK_sMaPB)
)
GO

CREATE TABLE tbl_QUYEN
(
    PK_sMaquyen VARCHAR(10) NOT NULL,
    sTenquyen NVARCHAR(10),
    PRIMARY KEY (PK_sMaquyen)
)

CREATE TABLE tbl_TAIKHOAN
(
    PK_sMaTK VARCHAR(10) NOT NULL,
    FK_sMaNV VARCHAR(10) NOT NULL,
    sTaikhoan VARCHAR(20),
    sMatkhau VARCHAR(20),
    sTinhtrang NVARCHAR(20),
    FK_sMaquyen VARCHAR(10) NOT NULL,
    PRIMARY KEY (PK_sMaTK),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NHANVIEN(PK_sMaNV),
    FOREIGN KEY (FK_sMaquyen) REFERENCES tbl_QUYEN(PK_sMaquyen)
)

CREATE TABLE tbl_LUONG
(
    PK_sMaL Varchar(10) NOT NULL,
    FK_sMaNV Varchar(10),
    sThoigian Varchar(10),
    sThuong Varchar(10),
    sPhat Varchar(10),
    fTongthunhap FLOAT,
    PRIMARY KEY(PK_sMaL),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NHANVIEN(PK_sMaNV)
)

CREATE TABLE tbl_CHAMCONG
(
    PK_sMachamcong Varchar(10),
    FK_sMaNV Varchar(10),
    dNgaychamcong DATETIME,
    dGiovao DATETIME,
    dGiora DATETIME,
    PRIMARY KEY(PK_sMachamcong),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NHANVIEN(PK_sMaNV)
)

CREATE TABLE tbl_BIENBAN
(
    PK_sMabienban Varchar(10),
    dNgaylap DATETIME,
    FK_sMaNV Varchar(10),
    sNddanhgia Nvarchar(20),
    PRIMARY KEY (PK_sMabienban),
    FOREIGN KEY(FK_sMaNV) REFERENCES tbl_NHANVIEN(PK_sMaNV),
)

CREATE TABLE tbl_HOPDONG
(
    PK_sMaHD Varchar(10),
    dNgaykyhd DATETIME,
    dThoihan DATETIME,
    FK_sMaNV Varchar(10),
    FK_sMaCV Varchar(10),
    FK_sMaPB Varchar(10),
    fLuongcb FLOAT,
    PRIMARY KEY(PK_sMaHD),
    FOREIGN KEY(FK_sMaNV) REFERENCES tbl_NHANVIEN(PK_sMaNV),
    FOREIGN KEY(FK_sMaCV) REFERENCES tbl_CHUCVU(PK_sMaCV),
    FOREIGN KEY(FK_sMaPB) REFERENCES tbl_PHONGBAN(PK_sMaPB),
)

CREATE TABLE tblBONHIEM
(
    PK_sMabonhiem Varchar(10),
    dNgaylap Datetime,
    dNgaycohieuluc Datetime,
    FK_sMaNV Varchar(10),
    FK_sMaCV Varchar(10),
    FK_sMaPB Varchar(10),
    PRIMARY KEY(PK_sMabonhiem),
    FOREIGN KEY(FK_sMaNV) REFERENCES tbl_NHANVIEN(PK_sMaNV),
    FOREIGN KEY(FK_sMaCV) REFERENCES tbl_CHUCVU(PK_sMaCV),
    FOREIGN KEY(FK_sMaPB) REFERENCES tbl_PHONGBAN(PK_sMaPB),
)

CREATE TABLE tbl_DONXINNGHI
(
    PK_sMaDon Varchar(10),
    dNgaylap Datetime,
    sLoaidon Nvarchar(20),
    dNgaybatdau Datetime,
    dNgayketthuc Datetime,
    FK_sMaNV Varchar(10),
    sLydo Nvarchar(30),
    PRIMARY KEY (PK_sMaDon),
    FOREIGN KEY(FK_sMaNV) REFERENCES tbl_NHANVIEN(PK_sMaNV),
)

CREATE TABLE tbl_KHENTHUONG
(
    PK_sMaKT Varchar(10),
    dNgaylap Datetime,
    sLoaidon Nvarchar(20),
    FK_sMaNV Varchar(10),
    sMucKTKL Nvarchar(20),
    sLydo Nvarchar(30),
    PRIMARY KEY(PK_sMaKT),
    FOREIGN KEY(FK_sMaNV) REFERENCES tbl_NHANVIEN(PK_sMaNV),
)

INSERT INTO tbl_PHONGBAN  VALUES
('PB01', N'Phòng Kỹ thuật', N'Chuyên về kỹ thuật'),
('PB02', N'Phòng Nhân sự', N'Quản lý nhân sự'),
('PB03', N'Phòng Kinh doanh', N'Phụ trách kinh doanh'),
('PB04', N'Phòng Tài chính', N'Quản lý tài chính'),
('PB05', N'Phòng Marketing', N'Truyền thông và quảng cáo');



INSERT INTO tbl_CHUCVU VALUES
('CV01', N'Giám đốc', 4.5),
('CV02', N'Trưởng phòng', 3.0),
('CV03', N'Nhân viên', 2.0),
('CV04', N'Quản lý', 3.5),
('CV05', N'Phó Giám đốc', 4.0);

INSERT INTO tbl_NHANVIEN  VALUES
('NV01', N'Nguyễn Văn A', '1985-01-01', N'Nam', '012345678901', N'123 ABC', '0901234567', 'nva@example.com', '2020-01-01', 15000000, N'Đang làm việc', 'CV01', 'PB01'),
('NV02', N'Trần Thị B', '1990-05-10', N'Nữ', '012345678902', N'456 DEF', '0901234568', 'ttb@example.com', '2021-02-15', 12000000, N'Đang làm việc', 'CV02', 'PB02'),
('NV03', N'Lê Văn C', '1988-03-15', N'Nam', '012345678903', N'789 GHI', '0901234569', 'lvc@example.com', '2022-03-01', 13000000, N'Đang làm việc', 'CV03', 'PB03'),
('NV04', N'Nguyễn Thị D', '1992-08-22', N'Nữ', '012345678904', N'101 JKL', '0901234570', 'ntd@example.com', '2022-04-01', 14000000, N'Đang làm việc', 'CV04', 'PB04'),
('NV05', N'Trần Văn E', '1985-12-10', N'Nam', '012345678905', N'123 MNO', '0901234571', 'tve@example.com', '2022-05-01', 12500000, N'Đang làm việc', 'CV05', 'PB05');

INSERT INTO tbl_QUYEN  VALUES
('Q01', N'Admin'),
('Q02', N'User'),
('Q03', N'Guest'),
('Q04', N'Manager'),
('Q05', N'Staff');
INSERT INTO tbl_TAIKHOAN  VALUES
('TK01', 'NV01', 'admin', 'admin123', N'Hoạt động', 'Q01'),
('TK02', 'NV02', 'user', 'user123', N'Hoạt động', 'Q02'),
('TK03', 'NV03', 'levanc', 'levanc123', N'Hoạt động', 'Q03'),
('TK04', 'NV04', 'nguyentd', 'nguyentd123', N'Hoạt động', 'Q04'),
('TK05', 'NV05', 'tranve', 'tranve123', N'Hoạt động', 'Q05');
INSERT INTO tbl_LUONG  VALUES
('L01', 'NV01', '2024-06', '1000000', '0', 16000000),
('L02', 'NV02', '2024-06', '500000', '0', 12500000),
('L03', 'NV03', '2024-06', '700000', '0', 13700000),
('L04', 'NV04', '2024-06', '800000', '0', 14800000),
('L05', 'NV05', '2024-06', '600000', '0', 13100000);

INSERT INTO tbl_CHAMCONG VALUES
('CC01', 'NV01', '2024-07-01', '2024-07-01 08:00:00', '2024-07-01 17:00:00'),
('CC02', 'NV02', '2024-07-01', '2024-07-01 08:00:00', '2024-07-01 17:00:00'),
('CC03', 'NV03', '2024-07-01', '2024-07-01 08:00:00', '2024-07-01 17:00:00'),
('CC04', 'NV04', '2024-07-01', '2024-07-01 08:00:00', '2024-07-01 17:00:00'),
('CC05', 'NV05', '2024-07-01', '2024-07-01 08:00:00', '2024-07-01 17:00:00');

INSERT INTO tbl_BIENBAN  VALUES
('BB01', '2024-07-01', 'NV01', N'Tốt'),
('BB02', '2024-07-01', 'NV02', N'Tốt'),
('BB03', '2024-07-01', 'NV03', N'Tốt'),
('BB04', '2024-07-01', 'NV04', N'Rất tốt'),
('BB05', '2024-07-01', 'NV05', N'Xuất sắc');

INSERT INTO tbl_HOPDONG  VALUES
('HD01', '2024-01-01', '2025-01-01', 'NV01', 'CV01', 'PB01', 15000000),
('HD02', '2024-01-01', '2025-01-01', 'NV02', 'CV02', 'PB02', 12000000),
('HD03', '2024-01-01', '2025-01-01', 'NV03', 'CV03', 'PB03', 13000000),
('HD04', '2024-01-01', '2025-01-01', 'NV04', 'CV04', 'PB04', 14000000),
('HD05', '2024-01-01', '2025-01-01', 'NV05', 'CV05', 'PB05', 12500000);

INSERT INTO tblBONHIEM  VALUES
('BN01', '2024-07-01', '2024-07-01', 'NV01', 'CV01', 'PB01'),
('BN02', '2024-07-01', '2024-07-01', 'NV02', 'CV02', 'PB02'),
('BN03', '2024-07-01', '2024-07-01', 'NV03', 'CV03', 'PB03'),
('BN04', '2024-07-01', '2024-07-01', 'NV04', 'CV04', 'PB04'),
('BN05', '2024-07-01', '2024-07-01', 'NV05', 'CV05', 'PB05');

INSERT INTO tbl_DONXINNGHI  VALUES
('DN01', '2024-07-01', N'Nghỉ phép', '2024-07-10', '2024-07-20', 'NV01', N'Nghỉ mát'),
('DN02', '2024-07-01', N'Nghỉ bệnh', '2024-07-15', '2024-07-25', 'NV02', N'Ốm'),
('DN03', '2024-07-01', N'Nghỉ phép', '2024-07-10', '2024-07-20', 'NV03', N'Nghỉ mát'),
('DN04', '2024-07-01', N'Nghỉ bệnh', '2024-07-15', '2024-07-25', 'NV04', N'Ốm'),
('DN05', '2024-07-01', N'Nghỉ phép', '2024-07-20', '2024-07-30', 'NV05', N'Đi du lịch');

INSERT INTO tbl_KHENTHUONG VALUES
('KT01', '2024-07-01', N'Khen thưởng', 'NV01', N'500000', N'Làm việc xuất sắc'),
('KT02', '2024-07-01', N'Khen thưởng', 'NV02', N'300000', N'Hoàn thành dự án'),
('KT03', '2024-07-01', N'Khen thưởng', 'NV03', N'700000', N'Làm việc xuất sắc'),
('KT04', '2024-07-01', N'Khen thưởng', 'NV04', N'800000', N'Hoàn thành dự án'),
('KT05', '2024-07-01', N'Khen thưởng', 'NV05', N'600000', N'Cống hiến nhiều');
--------------------------------------------------Storued Procedure---------------------------------------
------------------------------------------------------------------CHUCVU------------------------------------------------

CREATE PROC pr_SelectChucVu
AS 
SELECT * FROM tbl_CHUCVU
CREATE PROC pr_Them_Chucvu (  @sMaChucVu VARCHAR(10) , @sTenChucVu NVARCHAR(30) , @fHeSoLuong float )
AS 
BEGIN
 if exists (select * from tbl_CHUCVU where PK_sMaCV = @sMaChucVu)
 BEGIN
 RAISERROR(N'Mã chức vụ không được trùng lặp !!!', 16, 10)
;
 END
 else 
      BEGIN
	   INSERT INTO tbl_CHUCVU 
       VALUES(@sMaChucVu , @sTenChucVu , @fHeSoLuong);
	   print N'Thêm thành công';
	   END
	   
END


CREATE PROC pr_Sua_ChucVu ( @sMaChucVu VARCHAR(10) , @sTenChucVu NVARCHAR(30) , @fHeSoLuong float)

AS 
if not exists (select * from tbl_CHUCVU where PK_sMaCV = @sMaChucVu)
 BEGIN
 RAISERROR(N'Không tồn tại mã nhan vien !!!', 16, 10);
 END
 else
 BEGIN
      
	  UPDATE tbl_CHUCVU SET PK_sMaCV = @sMaChucVu , sTenCV = @sTenChucVu , fHSL = @fHeSoLuong
	  FROM tbl_CHUCVU
	  WHERE PK_sMaCV = @sMaChucVu;
	  
 END


  CREATE PROC pr_Xoa_ChucVu (  @sMaChucVu NVARCHAR(10))

AS 
if exists (select PK_sMaCV from tbl_CHUCVU WHERE PK_sMaCV = @sMaChucVu)
BEGIN
        if exists (select FK_sMaCV from tbl_HOPDONG where FK_sMaCV = @sMaChucVu)
    BEGIN
           
           DELETE FROM tbl_HOPDONG  WHERE FK_sMaCV = @sMaChucVu 
         
     END
	   if exists (select FK_sMaCV from tblBONHIEM where FK_sMaCV = @sMaChucVu)
	   BEGIN
           
           DELETE FROM tblBONHIEM  WHERE FK_sMaCV = @sMaChucVu 
         
       END
	    if exists (select FK_sMaCV from tbl_NHANVIEN where FK_sMaCV = @sMaChucVu)
		     BEGIN
			      if exists (select tblBONHIEM.FK_sMaNV from tblBONHIEM , tbl_CHUCVU , tbl_NHANVIEN where tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tblBONHIEM.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV )
				    BEGIN
					 DELETE FROM tblBONHIEM FROM tbl_NHANVIEN , tbl_CHUCVU  WHERE tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tblBONHIEM.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV
					END
				  if exists (select tbl_HOPDONG.FK_sMaNV from tbl_HOPDONG , tbl_CHUCVU , tbl_NHANVIEN where tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_HOPDONG.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV )
				    BEGIN
					 DELETE FROM tbl_HOPDONG FROM tbl_NHANVIEN , tbl_CHUCVU  WHERE tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_HOPDONG .FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV
					END
			      if exists (select tbl_DONXINNGHI.FK_sMaNV from tbl_DONXINNGHI , tbl_CHUCVU , tbl_NHANVIEN where tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_DONXINNGHI.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV )
				    BEGIN
					 DELETE FROM tbl_DONXINNGHI FROM tbl_NHANVIEN , tbl_CHUCVU  WHERE tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_DONXINNGHI .FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV
					END
			      if exists (select tbl_KHENTHUONG.FK_sMaNV from tbl_KHENTHUONG , tbl_CHUCVU , tbl_NHANVIEN where tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_KHENTHUONG.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV )
				    BEGIN
					 DELETE FROM tbl_KHENTHUONG FROM tbl_NHANVIEN , tbl_CHUCVU  WHERE tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_KHENTHUONG.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV
					END
                   if exists (select tbl_LUONG.FK_sMaNV from tbl_LUONG, tbl_CHUCVU , tbl_NHANVIEN where tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_LUONG.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV )
				    BEGIN
					 DELETE FROM tbl_LUONG FROM tbl_NHANVIEN , tbl_CHUCVU  WHERE tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_LUONG.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV
					END
				  if exists (select tbl_BIENBAN.FK_sMaNV from tbl_BIENBAN, tbl_CHUCVU , tbl_NHANVIEN where tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_BIENBAN.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV )
				    BEGIN
					 DELETE FROM tbl_BIENBAN FROM tbl_NHANVIEN , tbl_CHUCVU  WHERE tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_BIENBAN.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV
					END
                 if exists (select tbl_TAIKHOAN.FK_sMaNV from tbl_TAIKHOAN, tbl_CHUCVU , tbl_NHANVIEN where tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_TAIKHOAN.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV )
				    BEGIN
					 DELETE FROM tbl_TAIKHOAN FROM tbl_NHANVIEN , tbl_CHUCVU  WHERE tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_TAIKHOAN.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV
					END
		         if exists (select tbl_CHAMCONG.FK_sMaNV from tbl_CHAMCONG, tbl_CHUCVU , tbl_NHANVIEN where tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_CHAMCONG.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV )
				    BEGIN
					 DELETE FROM tbl_CHAMCONG FROM tbl_NHANVIEN , tbl_CHUCVU  WHERE tbl_CHUCVU.PK_sMaCV = @sMaChucVu AND tbl_CHAMCONG.FK_sMaNV = PK_sMaNV AND tbl_NHANVIEN.FK_sMaCV = tbl_CHUCVU.PK_sMaCV
					END
              DELETE FROM tbl_NHANVIEN  WHERE FK_sMaCV = @sMaChucVu 
			 END   
			  DELETE FROM tbl_CHUCVU  WHERE PK_sMaCV = @sMaChucVu;
END
