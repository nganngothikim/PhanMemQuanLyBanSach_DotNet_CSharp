--Tạo database
create database QL_NhaSach
go

use QL_NhaSach
go

--Tạo bảng

--Tạo bảng - tạo ràng buộc bảng Nhân viên
create table NhanVien
(
	MaNhanVien varchar(10) NOT NULL,
	TenNhanVien nvarchar(100) NOT NULL,
	QueQuan nvarchar(200) NULL,
	NgaySinh date NULL,
	CMND varchar(10) NULL,
	constraint PK_NhanVien primary key(MaNhanVien)
)

ALTER TABLE NhanVien 
ADD CONSTRAINT DF_QueQuan_NhanVien DEFAULT N'Chưa xác định' for QueQuan,
	CONSTRAINT UNQ_CMND_NhanVien UNIQUE(CMND),
	CONSTRAINT CK_NgaySinh_NhanVien Check(NgaySinh < getdate())

--Tạo bảng - tạo ràng buộc bảng Khách hàng
CREATE TABLE KhachHang
(
	MaKhachHang varchar(10) NOT NULL,
	TenKhachHang nvarchar(100) NOT NULL,
	DiaChi nvarchar(100) NULL,
	SoDienThoai varchar(11) NULL,
	EMail varchar(100) NULL,
	constraint PK_KhachHang primary key (MaKhachHang)
)

ALTER TABLE KhachHang 
ADD CONSTRAINT DF_DiaChi_KhachHang DEFAULT N'Chưa xác định' for DiaChi,
	CONSTRAINT CK_SDT_KhachHang CHECK(SoDienThoai LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	CONSTRAINT CK_EMail_KhachHang CHECK(EMail LIKE '%@%')

--Tạo bảng - tạo ràng buộc bảng Nhà xuất bản
create table NhaXuatBan
(
	MaNhaXuatBan varchar(10) NOT NULL,
	TenXuatBan nvarchar(100) NOT NULL,
	DiaChi nvarchar(100) NULL,
	SoDienThoai varchar(11) NULL,
	constraint PK_NhaXuatBan primary key(MaNhaXuatBan)
)
ALTER TABLE NhaXuatBan 
ADD CONSTRAINT DF_DiaChi_NhaXuatBan DEFAULT N'Chưa xác định' for DiaChi,
	CONSTRAINT CK_SDT_NhaXuatBan CHECK(SoDienThoai LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')

--Tạo bảng - tạo ràng buộc bảng Nhà cung cấp
CREATE TABLE NhaCungCap
(
	MaNhaCungCap varchar(10) NOT NULL,
	TenNhaCungCap nvarchar(100) NOT NULL,
	DiaChi nvarchar(200) NULL,
	SoDienThoai varchar(11) NULL,
	EMail varchar(50) NULL,
	constraint PK_NhaCungCap primary key (MaNhaCungCap)
)
ALTER TABLE NhaCungCap 
ADD CONSTRAINT DF_DiaChi_NhaCungCap DEFAULT N'Chưa xác định' for DiaChi,
	CONSTRAINT CK_SDT_NhaCungCap CHECK(SoDienThoai LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	CONSTRAINT CK_EMail_NhaCungCap CHECK(EMail LIKE '%@%')

--Tạo bảng - tạo ràng buộc bảng Thể loại
create table TheLoai
(
	MaTheLoai varchar(10) NOT NULL,
	TenTheLoai nvarchar(50) NOT NULL,
	constraint PK_TheLoai primary key(MaTheLoai)
)

--Tạo bảng - tạo ràng buộc bảng Tác giả
create table TacGia
(
	MaTacGia varchar(10) NOT NULL,
	TenTacGia nvarchar(50) NOT NULL,
	GhiChu nvarchar(200) NULL,
	constraint PK_TacGia primary key(MaTacGia)
)

--Tạo bảng - tạo ràng buộc bảng Sách
create table Sach
(
	MaSach varchar(10) NOT NULL,
	TenSach nvarchar(100) NOT NULL,
	MaTheLoai varchar(10) NULL,
	MaTacGia varchar(10) NULL,
	MaNhaXuatBan varchar(10) NULL,
	NoiDungTomTat nvarchar(200) NULL,
	NamXuatBan char(4) NULL,
	GiaNhap int NULL,
	GiaBan int NULL,
	SoLuongCon int NULL,
	constraint PK_Sach primary key(MaSach)
)

alter table Sach
add constraint FK_Sach_TheLoai foreign key(MaTheLoai) references TheLoai(MaTheLoai),
	constraint FK_Sach_TacGia foreign key(MaTacGia) references TacGia(MaTacGia),
	constraint FK_Sach_NhaXuatBan foreign key(MaNhaXuatBan) references NhaXuatBan(MaNhaXuatBan)

create table PhieuNhap
(
	MaPhieuNhap varchar(10) NOT NULL,
	MaNhanVien varchar(10) NULL,
	MaNhaCungCap varchar(10) NULL,
	NgayNhap date NULL,
	TongTien int NULL,
	constraint PK_PhieuNhap primary key(MaPhieuNhap)
)

ALTER TABLE PhieuNhap 
ADD constraint FK_PhieuNhap_NhanVien foreign key(MaNhanVien) references NhanVien(MaNhanVien),
	constraint FK_PhieuNhap_NhaCungCap foreign key(MaNhaCungCap) references NhaCungCap(MaNhaCungCap),
	CONSTRAINT DF_NgayNhap DEFAULT (getdate()) for NgayNhap

CREATE TABLE ChiTietPhieuNhap(
	 MaPhieuNhap  varchar(10) NOT NULL,
	 MaSach  varchar(10) NOT NULL,
	 SoLuongNhap  int NULL,
	 DonGia  int NULL,
	 ThanhTien  int NULL,
	 constraint PK_ChiTietPhieuNhap primary key (MaPhieuNhap,MaSach)
)

alter table ChiTietPhieuNhap
add constraint FK_ChiTietPhieuNhap_PhieuNhap foreign key(MaPhieuNhap) references PhieuNhap(MaPhieuNhap),
	constraint FK_ChiTietPhieuNhap_Sach foreign key(MaSach) references Sach(MaSach)

CREATE TABLE HoaDon(
	SoHoaDon varchar(10) NOT NULL,
	MaNhanVien varchar(10) NULL,
	MaKhachHang varchar(10) NULL,
	NgayLap date NULL,
	TongTien int NULL,
	constraint PK_HoaDon primary key (SoHoaDon)
)

alter table HoaDon
add constraint FK_HoaDon_NhanVien foreign key(MaNhanVien) references NhanVien(MaNhanVien),
	constraint FK_HoaDon_KhachHang foreign key(MaKhachHang) references KhachHang(MaKhachHang),
	CONSTRAINT DF_NgayLap DEFAULT (getdate()) for NgayLap

CREATE TABLE ChiTietHoaDon (
	 SoHoaDon varchar(10) NOT NULL,
	 MaSach varchar(10) NOT NULL,
	 SoLuongBan int  NULL,
	 GiaBan int NULL,
	 ThanhTien int NULL,
	 constraint PK_ChiTietHoaDon primary key (SoHoaDon,MaSach)
)

alter table ChiTietHoaDon
add constraint FK_ChiTietHoaDon_HoaDon foreign key(SoHoaDon) references HoaDon(SoHoaDon),
	constraint FK_ChiTietHoaDon_Sach foreign key(MaSach) references Sach(MaSach)

CREATE TABLE DangNhap
(
	MaNhanVien varchar(10) NOT NULL,
	MatKhau varchar(10) NULL,
	Quyen nvarchar(20) NULL,
	constraint PK_DangNhap primary key (MaNhanVien)
)
alter table DangNhap
add constraint FK_DangNhap_NhanVien foreign key(MaNhanVien) references NhanVien(MaNhanVien)

-------------------------------------------------------------------
-- TRIGGER
-------------------------------------------------------------------
----Trigger cập nhật giá nhập từ phiếu nhập xuống bảng sách --Như
-------------------------------------------------------------------
CREATE VIEW NGAYNHAP --Bảng ảo lưu trữ ngày nhập gần nhất theo mã sách
AS
select MaSach,MAX(NgayNhap) as ngay from ChiTietPhieuNhap,PhieuNhap
	where ChiTietPhieuNhap.MaPhieuNhap=PhieuNhap.MaPhieuNhap group by MaSach
go

CREATE VIEW GIANHAP --Bảng ảo lưu trữ giá nhập theo ngày nhập gần nhất
AS
	select ChiTietPhieuNhap.MaSach,DonGia from ChiTietPhieuNhap,PhieuNhap,NGAYNHAP
	where ChiTietPhieuNhap.MaPhieuNhap=PhieuNhap.MaPhieuNhap and ChiTietPhieuNhap.MaSach=NGAYNHAP.MaSach and NgayNhap=ngay
go
--Trigger cập nhật giá nhập từ phiếu nhập xuống sách
CREATE TRIGGER Trigger_CapNhatGiaNhap_Sach 
ON ChiTietPhieuNhap
FOR INSERT
AS 
	update Sach
	set GiaNhap = (select DonGia from GIANHAP where GIANHAP.MaSach=Sach.MaSach)
	where MaSach= (select MaSach from inserted)	
GO
-------------------------------------------------------------------
--select* from GIANHAP
----drop view GIANHAP
--drop trigger Trigger_CapNhatGiaNhap_Sach 

-----------------------------------------------------------------------
--Trigger cập nhật số lượng khi nhập sách --Ngân
-----------------------------------------------------------------------
CREATE TRIGGER Trigger_CapNhatSoLuongKhiNhap 
ON ChiTietPhieuNhap
FOR INSERT
AS 
	Update Sach Set SoLuongCon = SoLuongCon + (select SoLuongNhap from inserted)
	where MaSach= (select MaSach from inserted)	
GO
-----------------------------------------------------------------------
--select* from ChiTietPhieuNhap
--select* from Sach
--drop TRIGGER Trigger_CapNhatSoLuongKhiNhap 


-----------------------------------------------------------------------
--Trigger cập nhật số lượng khi bán --Phát
-----------------------------------------------------------------------

CREATE TRIGGER Trigger_CapNhatSoLuongKhiBan 
ON ChiTietHoaDon
FOR INSERT, UPDATE
AS 
	Update Sach Set SoLuongCon = SoLuongCon - (select SoLuongBan from inserted)
	where MaSach= (select MaSach from inserted)	
GO

-----------------------------------------------------------------------
drop trigger Trigger_CapNhatSoLuongKhiBan 
--select* from ChiTietHoaDon
--select* from Sach

--delete from ChiTietHoaDon where SoHoaDon='HD004' and MaSach='SH002'
--Insert into HoaDon 
--values('HD008', 'NV003', 'KH002', '2022-12-14', 0)

--Insert into ChiTietHoaDon 
--values ('HD008', 'SH001', 10, 75000, 0)
--Insert into ChiTietHoaDon 
--values('HD008', 'SH003', 1, 60000, 0)

-----------------------------------------------------------------------
---- Trigger kiểm tra số lượng sách khi bán (Kiểm tra từng dòng) --Phát
-----------------------------------------------------------------------

CREATE TRIGGER Trigger_KiemTraSoLuongBan_CTHoaDon 
ON ChiTietHoaDon 
FOR INSERT,UPDATE
AS 
	if(select SoLuongCon from Sach 
		where MaSach = (select MaSach from inserted)) < (select SoLuongBan from inserted, HoaDon, Sach 
																		where HoaDon.SoHoaDon = (select SoHoaDon from inserted) and
																		Sach.MaSach = (select MaSach from inserted))
	BEGIN
		print N'Không đủ số lượng còn của sách'
		rollback tran
	END
GO

--drop trigger Trigger_KiemTraSoLuongBan_CTHoaDon 
--select* from ChiTietHoaDon
--select* from Sach

---------------------------------------------------------------------
-- NHẬP LIỆU
---------------------------------------------------------------------

-- Nhân viên

Insert into NhanVien
values('NV001', N'Lê Tâm Như', N'Bến Tre', '2002-01-01', '194526681'),
	('NV002', N'Lê Minh Phát', N'Đồng Tháp', '1990-08-14', '194526682'),
	('NV003', N'Ngô Thị Kim Ngân', N'Vũng Tàu', '1991-05-24', '194526683')

-- Khách hàng

Insert into KhachHang
values ('KH001', N'Lưu Văn Cường', N'TPHCM', '0985512236', 'LuuCuong.1994@Gmail.Com'),
	('KH002', N'Hoàng Minh Cường', N'Quảng Bình', '0985512223', 'HoangMinhCuong@Gmail.Com'),
	('KH003', N'Nguyễn Phương Duy', N'Đà Nẵng', '0985554523', 'PhuongDuy@Gmail.Com')

-- Nhà xuất bản

Insert into NhaXuatBan 
values('NXB001', N'Hà Nội', N'Đà Nẵng', '0984512365'),
	('NXB002', N'Giáo Dục', N'Đà Nẵng', '0981237283'),
	('NXB003', N'Hội Nhà Văn', N'Đà Nẵng', '0128372333')

-- Nhà cung cấp 

Insert into NhaCungCap 
values ('NCC001', N'Hoàng Ân ', N'Đà Nẵng', '0984384378', 'hoangan@gmail.com'),
	('NCC002', N'Sao Việt', N'Đà Nẵng', '0123892833', 'saoviet@gmail.com'),
	('NCC003', N'Trung Tín', N'Đà Nẵng', '0123892382', 'trungtin@gmail.com')

-- Thể loại 

Insert into TheLoai 
values('TL001', N'Truyện Tranh'),
	('TL002', N'Sách Văn Học'),
	('TL003', N'Sách Tham Khảo')

-- Tác giả 

Insert into TacGia
values('TG001', N'Nguyễn Du', NULL),
	('TG002', N'Trần Trọng Kim', NULL),
	('TG003', N'Nguyễn Ngọc Tư', NULL),
	('TG004', N'Higashino Keigo', NULL),
	('TG005', N'Nhóm Tác Giả', NULL)

-- Sách

Insert into Sach
values('SH001', N'Truyện Kiều', 'TL002', 'TG001', 'NXB001', N'Truyện nói đến số phận bất hạnh của Thúy Kiều...', N'1995', 0, 75000, 0),
	('SH002', N'Việt Nam sử lược', 'TL002', 'TG002', 'NXB002', N'Dựa trên những nghiên cứu trước đó như Nam sử tiểu học và Sơ học An Nam sử lược', N'2015', 0, 75000, 0),
	('SH003', N'Cánh đồng bất tận', 'TL002', 'TG001', 'NXB002', N'Cánh đồng bất tận gồm hai truyện vừa là Cánh đồng bất tận và Đất', N'1995', 0, 75000, 0),
	('SH004', N'Truyện cổ Grim', N'TL001', 'TG005', 'NXB003', N'Truyện cổ tích', N'2020', 0, 75000, 0),
	('SH005', N'Thỏ bảy màu', 'TL001', 'TG005', 'NXB003', N'Những câu chuyện xảy ra xung quanh cuộc sống ảo diệu của Thỏ', N'2021', 0, 70000, 0),
	('SH006', N'Phía sau nghi can X', 'TL002', 'TG004', 'NXB003', N'Phía sau nghi can X gây hứng thú cho người đọc bởi nó khác rất nhiều so với những cuốn trinh thám thông thường', N'2019', 0, 85000, 0),
	('SH007', N'Bạch dạ hành', 'TL002', 'TG004', 'NXB001', N'Vụ án mạng ông chủ tiệm cầm đồ rơi vào bế tắc và bị bỏ xó.', N'2020', 0, 105000, 0),
	('SH008', N'Khoa học và phát minh', 'TL003', 'TG005', 'NXB002', N'Cuốn sách này miêu tả ngắn gọn cuộc đời và ý chí của hơn 300 vĩ nhân', N'2001', 0, 45000, 0),
	('SH009', N'Văn hoá và nghệ thuật', 'TL003', 'TG005', 'NXB002', N'Cuốn sách này giới thiệu hơn 300 người nổi tiếng trong nhiều lĩnh vực văn hóa, nghệ thuật, ca hát, biểu diễn', N'2022', 0, 85000, 0)

-- Phiếu nhập sách

Insert into PhieuNhap
values('PN001', 'NV001', 'NCC002', '2022-07-20', 0)
Insert into PhieuNhap
values('PN002', 'NV001', 'NCC001', '2022-10-23', 0)
Insert into PhieuNhap
values('PN003', 'NV001', 'NCC002', '2022-11-23', 0)
Insert into PhieuNhap
values('PN004', 'NV001', 'NCC003', '2022-12-05', 0)

--Chi tiết phiếu nhập sách

Insert into ChiTietPhieuNhap 
values ('PN001', 'SH003', 100, 60000, 0)
Insert into ChiTietPhieuNhap 
values('PN001', 'SH001', 50, 60000, 0)
Insert into ChiTietPhieuNhap 
values('PN001', 'SH004', 30, 60000, 0)
Insert into ChiTietPhieuNhap 
values('PN002', 'SH002', 110, 60000, 0)
Insert into ChiTietPhieuNhap 
values('PN002', 'SH004', 40, 70000, 0)
Insert into ChiTietPhieuNhap 
values('PN003', 'SH007', 45, 90000, 0)
Insert into ChiTietPhieuNhap 
values('PN003', 'SH005', 45, 60000, 0)
Insert into ChiTietPhieuNhap 
values('PN004', 'SH006', 48, 60000, 0)
Insert into ChiTietPhieuNhap 
values('PN004', 'SH008', 48, 60000, 0)
Insert into ChiTietPhieuNhap 
values('PN004', 'SH009', 48, 60000, 0)
Insert into ChiTietPhieuNhap 
values('PN004', 'SH005', 100, 60000, 0)

--Hoá đơn bán sách

Insert into HoaDon 
values ('HD001', 'NV002', 'KH001', '2022-12-10' , 0)
Insert into HoaDon 
values('HD002', 'NV002', 'KH002', '2022-12-11', 0)
Insert into HoaDon 
values('HD003', 'NV002', 'KH003', '2022-12-12', 0)
Insert into HoaDon 
values('HD004', 'NV003', 'KH002', '2022-12-13', 0)
Insert into HoaDon 
values('HD005', 'NV003', 'KH002', '2022-12-14', 0)

-- Chi tiết hoá đơn

Insert into ChiTietHoaDon 
values ('HD001', 'SH001', 10, 75000, 0)
Insert into ChiTietHoaDon 
values('HD001', 'SH003', 1, 60000, 0)
Insert into ChiTietHoaDon 
values('HD002', 'SH005', 5, 70000, 0)
Insert into ChiTietHoaDon 
values('HD003', 'SH002', 1, 75000, 0)
Insert into ChiTietHoaDon 
values('HD003', 'SH005', 1, 70000, 0)
Insert into ChiTietHoaDon 
values('HD004', 'SH005', 1, 70000, 0)
Insert into ChiTietHoaDon 
values('HD004', 'SH002', 1, 75000, 0)
Insert into ChiTietHoaDon 
values('HD005', 'SH002', 1, 75000, 0)
-- Thông tin đăng nhập

INSERT INTO DangNhap 
VALUES ('NV001', N'123', N'admin'),
	('NV002', '123', N'user'),
	('NV003', '123', N'user')



---------------------------------------------------------------------
-- PROCEDURE
---------------------------------------------------------------------

---------------------------------------------------------------------
-- Cursor cập nhật thành tiền của 1 sản phẩm trong chi tiết hóa đơn --Như
---------------------------------------------------------------------
declare TinhThanhTien_CTHD cursor 
	dynamic 
	for 
	select SoHoaDon, MaSach
	from ChiTietHoaDon
	open TinhThanhTien_CTHD
	declare @hd varchar(10), @ma varchar(10)
	fetch next from  TinhThanhTien_CTHD into @hd, @ma
	While (@@FETCH_STATUS = 0)
	Begin
	update ChiTietHoaDon
	set ThanhTien = SoLuongBan*GiaBan 
	where SoHoaDon=@hd and MaSach=@ma
	fetch next from  TinhThanhTien_CTHD into @hd, @ma
	end
close TinhThanhTien_CTHD

deallocate TinhThanhTien_CTHD

---------------------------------------------------------------------
-- Proc cập nhật thành tiền của 1 hóa đơn --Phát
---------------------------------------------------------------------
Create Proc Proc_CapNhatThanhTien_HoaDon
AS	
	update HoaDon
	set TongTien = (select SUM(ThanhTien) from ChiTietHoaDon where ChiTietHoaDon.SoHoaDon = HoaDon.SoHoaDon)
go
--Thực thi
exec Proc_CapNhatThanhTien_HoaDon
select * from HoaDon

--drop proc Proc_CapNhatThanhTien_HoaDon

---------------------------------------------------------------------
-- Proc cập nhật đơn giá của 1 sản phẩm trong chi tiết Phiếu nhập --Ngân
---------------------------------------------------------------------
create proc Proc_CapNhatDonGia_PhieuNhap 
AS	
	update ChiTietPhieuNhap
	set ThanhTien = SoLuongNhap * DonGia
go
--Thực thi
exec Proc_CapNhatDonGia_PhieuNhap
select * from ChiTietPhieuNhap

--drop proc Proc_CapNhatDonGia_PhieuNhap

---------------------------------------------------------------------
-- Proc cập nhật thành tiền của 1 Phiếu nhập --Ngân
---------------------------------------------------------------------
create proc Proc_CapNhatThanhTien_PhieuNhap
AS	
	update PhieuNhap
	set TongTien = (select SUM(ThanhTien) from ChiTietPhieuNhap where ChiTietPhieuNhap.MaPhieuNhap = PhieuNhap.MaPhieuNhap)
go
--Thực thi
EXEC Proc_CapNhatThanhTien_PhieuNhap
select * from PhieuNhap

--drop proc Proc_CapNhatThanhTien_PhieuNhap


---------------------------------------------------------------------
-- Proc insert sách mới --Như
---------------------------------------------------------------------
drop proc Insert_Sach

create proc Insert_Sach @mash varchar(10), @tensh nvarchar(100), @matl varchar(10), 
						@matg varchar(10), @manxb varchar(10), @noidung nvarchar(200), 
						@namxb char(4), @giaban int
as
	begin
		insert into Sach
		values(@mash, @tensh, @matl, @matg, @manxb, @noidung, @namxb, 0, @giaban, '')
	end
go
--Thực thi
exec Insert_Sach '01', N'sa', 'TL002', 'TG001', 'NXB001', N'Truyện nói đến số phận bất hạnh của Thúy Kiều...', '1995', 0, 75000, ''
select * from Sach


---------------------------------------------------------------------
-- Proc Update sách --Như
---------------------------------------------------------------------
drop proc Update_Sach

create proc Update_Sach @mash varchar(10), @tensh nvarchar(100), @matl varchar(10), 
						@matg varchar(10), @manxb varchar(10), @noidung nvarchar(200), 
						@namxb char(4), @giaban int
as
	begin
		update Sach
		set TenSach = @tensh, 
			MaTheLoai = @matl, 
			MaTacGia =  @matg, 
			MaNhaXuatBan = @manxb, 
			NoiDungTomTat = @noidung, 
			NamXuatBan = @namxb, 
			GiaBan = @giaban
		where MaSach = @mash
	end
go
--Thực thi
exec Update_Sach '01', N'samg', 'TL002', 'TG001', 'NXB001', N'Truyện nói đến số phận bất hạnh của Thúy Kiều...', '1995', 60000, 75000

---------------------------------------------------------------------
-- Proc delete sách --Như
---------------------------------------------------------------------
drop proc DELETE_SACH

CREATE PROC DELETE_SACH @mash varchar(10) 
as
	BEGIN 
		delete from Sach where MaSach = @mash
	END
go
--Thực thi
exec DELETE_SACH '01'
select * from Sach

---------------------------------------------------------------------
--Thông kê sách gần hết --Phát
---------------------------------------------------------------------
Create Proc Proc_ThongKeSachGanHet
AS
	select top(3) TenSach as N'Tên Sách', SoLuongCon as N'Số Lượng Còn'  from Sach 
	order by SoLuongCon asc
go
--Thực thi
exec Proc_ThongKeSachGanHet

select * from ChiTietHoaDon
select * from Sach

--drop Proc Proc_ThongKeSachGanHet
---------------------------------------------------------------------
--Thông kê sách hết --Như
---------------------------------------------------------------------
Create Proc Proc_ThongKeSachHet
AS
	select TenSach as N'Tên Sách', SoLuongCon as N'Số Lượng Còn' from Sach 
	Where SoLuongCon = 0
go
--Thực thi
exec Proc_ThongKeSachHet

select * from ChiTietHoaDon
select * from Sach

---------------------------------------------------------------------
-- FUNCTION
---------------------------------------------------------------------

---------------------------------------------------------------------
--Thông kê sách theo mã thể loại --Như
---------------------------------------------------------------------
create function ThongKeDoanhThuSach_TheoTheLoai(@matl varchar(10))
returns table
as
	return(select Sum(ChiTietHoaDon.ThanhTien) as N'Tổng doanh thu' from Sach, ChiTietHoaDon
		where Sach.MaSach = ChiTietHoaDon.MaSach and MaTheLoai = @matl)
go
--Thực thi
select * from dbo.ThongKeDoanhThuSach_TheoTheLoai('TL002')
go

---------------------------------------------------------------------
--Thông kê sách theo mã Nhà xuất bản --Ngân
---------------------------------------------------------------------
create function ThongKeDoanhThuSach_TheoNXB(@manxb varchar(10))
returns table
as
	return(select Sum(ChiTietHoaDon.ThanhTien) as N'Tổng doanh thu' from Sach, ChiTietHoaDon
		where Sach.MaSach = ChiTietHoaDon.MaSach and Sach.MaNhaXuatBan = @manxb)
go
--Thực thi
select * from dbo.ThongKeDoanhThuSach_TheoNXB('NXB002')
go


---------------------------------------------------------------------
--Hàm tìm kiếm theo mã sách hoặc tên sách --Như
---------------------------------------------------------------------

CREATE FUNCTION TimKiemSach(@mash varchar(10), @tensh nvarchar(100))
RETURNS TABLE
AS
	RETURN (SELECT* FROM Sach
			WHERE @mash=MaSach OR @tensh = TenSach)
go
--Thực thi
select * from TimKiemSach('',N'Truyện cổ Grim')
go

---------------------------------------------------------------------
--Hàm tìm kiếm theo mã Phiếu nhập --Ngân
---------------------------------------------------------------------

CREATE FUNCTION TimKiemPhieuNhap(@maph varchar(10))
RETURNS TABLE
AS
	RETURN (SELECT* FROM PhieuNhap
			WHERE @maph=MaPhieuNhap)
go
--Thực thi
select * from dbo.TimKiemPhieuNhap('PN001')
go

---------------------------------------------------------------------
--Hàm tìm kiếm theo mã Hóa đơn --Phát
---------------------------------------------------------------------

CREATE FUNCTION TimKiemHoaDon(@mahd varchar(10))
RETURNS TABLE
AS
	RETURN (SELECT* FROM HoaDon
			WHERE @mahd=SoHoaDon)
go
--Thực thi
select * from TimKiemHoaDon('HD001')
go
---------------------------------------------------------------------
--In họ tên khách hàng và số lượng sách đã mua khi truyền vào tham số mã khách hàng --Phát
---------------------------------------------------------------------
CREATE FUNCTION SL_SachKHMua(@makh varchar(10))
RETURNS TABLE
AS
	RETURN (SELECT MaSach , COUNT(SoLuongBan) as N'Số lượng mua' FROM KhachHang, HoaDon, ChiTietHoaDon
			WHERE @makh=KhachHang.MaKhachHang and KhachHang.MaKhachHang=HoaDon.MaKhachHang and HoaDon.SoHoaDon=ChiTietHoaDon.SoHoaDon
			Group by MaSach)
go

--Thực thi
select * from SL_SachKHMua('KH003')
go
select * from HoaDon

--drop function SL_SachKHMua
--------------------------------------------------------------------------
---------------------------------------------------------------------
-- Fuction tính doanh thu theo ngày trong 1 tháng --Ngân
---------------------------------------------------------------------
drop function DoanhThu_TinhTheoNgay

create function DoanhThu_TinhTheoNgay (@TuNgay date, @DenNgay date)
returns table
as
	return(select Sum(TongTien) as N'Tổng doanh thu'
			from HoaDon
			where (Day(NgayLap) >= Day(@TuNgay) and Day(NgayLap) <= Day(@DenNgay)) and 
			(MONTH(NgayLap) =  MONTH(@TuNgay) and MONTH(NgayLap) =  MONTH(@DenNgay)) and 
			(year(NgayLap) =  year(@TuNgay) and year(NgayLap) =  year(@DenNgay))
			)
go
--Thực thi
select * from DoanhThu_TinhTheoNgay('2022-12-11','2022-12-13')
go

select * from HoaDon