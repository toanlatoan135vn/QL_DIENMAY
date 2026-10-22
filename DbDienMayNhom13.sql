-- 1. TẠO DATABASE
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DbDienMayNhom13')
BEGIN
    CREATE DATABASE DbDienMayNhom13;
END
GO

USE DbDienMayNhom13;
GO


IF OBJECT_ID('dbo.ChiTietDonHang', 'U') IS NOT NULL DROP TABLE dbo.ChiTietDonHang;
IF OBJECT_ID('dbo.DonHang', 'U') IS NOT NULL DROP TABLE dbo.DonHang;
IF OBJECT_ID('dbo.SanPham', 'U') IS NOT NULL DROP TABLE dbo.SanPham;
IF OBJECT_ID('dbo.LoaiSanPham', 'U') IS NOT NULL DROP TABLE dbo.LoaiSanPham;
IF OBJECT_ID('dbo.KhachHang', 'U') IS NOT NULL DROP TABLE dbo.KhachHang;
GO

-- 2. TẠO CÁC BẢNG (CHUẨN HÓA)
-- Bảng cho Thành viên 1
CREATE TABLE LoaiSanPham (
    MaLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(150) NOT NULL
);
GO

-- Bảng cho Thành viên 2
CREATE TABLE SanPham (
    MaSP INT IDENTITY(1,1) PRIMARY KEY,
    TenSP NVARCHAR(250) NOT NULL,
    GiaBan DECIMAL(18,2) NOT NULL,
    HinhAnh NVARCHAR(500) NULL,
    TrangThai BIT DEFAULT 1, -- 1: Còn hàng, 0: Hết hàng
    MaLoai INT NOT NULL,
    CONSTRAINT FK_SanPham_LoaiSanPham FOREIGN KEY (MaLoai) REFERENCES LoaiSanPham(MaLoai) ON DELETE CASCADE
);
GO

-- Các bảng cho Thành viên 3
CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(150) NOT NULL,
    TaiKhoan VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    DienThoai VARCHAR(20) NULL,
    Email VARCHAR(100) NULL,
    DiaChi NVARCHAR(250) NULL,
    LaAdmin BIT DEFAULT 0 -- 1: Admin, 0: Khách thường
);
GO

CREATE TABLE DonHang (
    MaDH INT IDENTITY(1,1) PRIMARY KEY,
    NgayDat DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) DEFAULT 0,
    TrangThaiDH INT DEFAULT 0, -- 0: Chờ duyệt, 1: Đang giao, 2: Thành công, 3: Đã hủy
    MaKH INT NOT NULL,
    CONSTRAINT FK_DonHang_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH) ON DELETE CASCADE
);
GO

CREATE TABLE ChiTietDonHang (
    MaDH INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (MaDH, MaSP),
    CONSTRAINT FK_CTDH_DonHang FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH) ON DELETE CASCADE,
    CONSTRAINT FK_CTDH_SanPham FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP) ON DELETE CASCADE
);
GO

-- 3. NẠP DỮ LIỆU MẪU ĐỂ NHÓM CODE GIAO DIỆN
INSERT INTO LoaiSanPham (TenLoai) VALUES 
(N'Tivi - Âm thanh'), (N'Tủ lạnh - Tủ đông'), (N'Máy lạnh'), (N'Máy giặt - Sấy');

INSERT INTO SanPham (TenSP, GiaBan, HinhAnh, TrangThai, MaLoai) VALUES
(N'Smart Tivi Samsung 4K 65 inch', 15990000, 'tivi-ss.jpg', 1, 1),
(N'Tủ lạnh Panasonic Inverter 234L', 7200000, 'tulanh-pan.jpg', 1, 2),
(N'Máy lạnh Daikin Inverter 1 HP', 9800000, 'maylanh-dk.jpg', 1, 3),
(N'Máy giặt lồng ngang Electrolux', 10500000, 'maygiat-el.jpg', 1, 4);

INSERT INTO KhachHang (HoTen, TaiKhoan, MatKhau, LaAdmin) VALUES 
(N'Quản trị viên', 'admin', '123456', 1),
(N'Khách Hàng A', 'khacha', '123456', 0);
GO