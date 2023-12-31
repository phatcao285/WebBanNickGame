USE [master]
GO
/****** Object:  Database [ShopBanNickGame]    Script Date: 10/10/2023 9:39:59 CH ******/
CREATE DATABASE [ShopBanNickGame]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShopBanNickGame', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MAYAO\MSSQL\DATA\ShopBanNickGame.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShopBanNickGame_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MAYAO\MSSQL\DATA\ShopBanNickGame_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ShopBanNickGame] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShopBanNickGame].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShopBanNickGame] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShopBanNickGame] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShopBanNickGame] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShopBanNickGame] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShopBanNickGame] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET RECOVERY FULL 
GO
ALTER DATABASE [ShopBanNickGame] SET  MULTI_USER 
GO
ALTER DATABASE [ShopBanNickGame] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShopBanNickGame] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShopBanNickGame] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShopBanNickGame] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ShopBanNickGame] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ShopBanNickGame] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShopBanNickGame', N'ON'
GO
ALTER DATABASE [ShopBanNickGame] SET QUERY_STORE = OFF
GO
USE [ShopBanNickGame]
GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 10/10/2023 9:39:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[MaCT] [int] IDENTITY(1,1) NOT NULL,
	[TaiKhoan] [varchar](255) NULL,
	[Matkhau] [varchar](255) NULL,
	[MaDH] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhMuc]    Script Date: 10/10/2023 9:39:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMuc](
	[MaDM] [int] IDENTITY(1,1) NOT NULL,
	[TenDM] [nvarchar](255) NULL,
	[TrangThai] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhMucCT]    Script Date: 10/10/2023 9:39:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMucCT](
	[MaCTDanhMuc] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](255) NULL,
	[MaDM] [int] NULL,
	[HinhAnh] [nvarchar](255) NULL,
	[TrangThai] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCTDanhMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 10/10/2023 9:39:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[ThoiGianMua] [datetime] NULL,
	[MaKH] [int] NULL,
	[GiaTien] [decimal](18, 3) NOT NULL,
	[MaNick] [int] NULL,
	[MaDH] [bigint] NOT NULL,
 CONSTRAINT [PK_DonHang_MaDH] PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiDung]    Script Date: 10/10/2023 9:39:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[TenKH] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[Tendangnhap] [nvarchar](255) NULL,
	[matkhau] [varchar](255) NULL,
	[chucvu] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NickLM]    Script Date: 10/10/2023 9:39:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NickLM](
	[MaNick] [int] IDENTITY(1,1) NOT NULL,
	[TaiKhoan] [varchar](255) NULL,
	[MatKhau] [varchar](255) NULL,
	[Rank] [nvarchar](50) NULL,
	[Tuong] [int] NULL,
	[TrangPhuc] [nvarchar](255) NULL,
	[HinhAnh] [nvarchar](255) NULL,
	[MaCTDanhMuc] [int] NULL,
	[Gia] [decimal](18, 3) NULL,
	[ThuVienChuaHinh] [nvarchar](50) NULL,
	[TrangThai] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNick] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChiTietDonHang] ON 

INSERT [dbo].[ChiTietDonHang] ([MaCT], [TaiKhoan], [Matkhau], [MaDH]) VALUES (3, N'aa', N'bb', 638325422021209742)
SET IDENTITY_INSERT [dbo].[ChiTietDonHang] OFF
GO
SET IDENTITY_INSERT [dbo].[DanhMuc] ON 

INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [TrangThai]) VALUES (1, N'Liên minh huyền thoại', N'Hoạt động')
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [TrangThai]) VALUES (2, N'Liên minh tốc chiến', N'Hoạt động')
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [TrangThai]) VALUES (3, N'Liên quân', N'Hoạt động')
SET IDENTITY_INSERT [dbo].[DanhMuc] OFF
GO
SET IDENTITY_INSERT [dbo].[DanhMucCT] ON 

INSERT [dbo].[DanhMucCT] ([MaCTDanhMuc], [Ten], [MaDM], [HinhAnh], [TrangThai]) VALUES (1, N'Liên minh giá rẻ', 1, N'1.gif', N'Dừng hoạt động')
INSERT [dbo].[DanhMucCT] ([MaCTDanhMuc], [Ten], [MaDM], [HinhAnh], [TrangThai]) VALUES (2, N'Liên minh vip', 1, N'4.gif', N'Hoạt động')
INSERT [dbo].[DanhMucCT] ([MaCTDanhMuc], [Ten], [MaDM], [HinhAnh], [TrangThai]) VALUES (3, N'Tốc chiến giá rẻ', 2, N'6.gif', N'Hoạt động')
INSERT [dbo].[DanhMucCT] ([MaCTDanhMuc], [Ten], [MaDM], [HinhAnh], [TrangThai]) VALUES (4, N'Tốc chiến vip', 2, N'5.gif', N'Hoạt động')
INSERT [dbo].[DanhMucCT] ([MaCTDanhMuc], [Ten], [MaDM], [HinhAnh], [TrangThai]) VALUES (5, N'Liên quân giá rẻ', 3, N'2.gif', N'Hoạt động')
INSERT [dbo].[DanhMucCT] ([MaCTDanhMuc], [Ten], [MaDM], [HinhAnh], [TrangThai]) VALUES (6, N'Liên quân xả kho', 3, N'8.gif', N'Hoạt động')
INSERT [dbo].[DanhMucCT] ([MaCTDanhMuc], [Ten], [MaDM], [HinhAnh], [TrangThai]) VALUES (7, N'Liên quân sale', 3, N'3.gif', N'Hoạt động')
INSERT [dbo].[DanhMucCT] ([MaCTDanhMuc], [Ten], [MaDM], [HinhAnh], [TrangThai]) VALUES (8, N'Liên quân vip', 3, N'7.gif', N'Hoạt động')
SET IDENTITY_INSERT [dbo].[DanhMucCT] OFF
GO
INSERT [dbo].[DonHang] ([ThoiGianMua], [MaKH], [GiaTien], [MaNick], [MaDH]) VALUES (CAST(N'2023-10-10T13:43:22.120' AS DateTime), 1, CAST(100.000 AS Decimal(18, 3)), 2, 638325422021209742)
GO
SET IDENTITY_INSERT [dbo].[NguoiDung] ON 

INSERT [dbo].[NguoiDung] ([MaKH], [TenKH], [email], [Tendangnhap], [matkhau], [chucvu]) VALUES (1, N'Đức', N'kienpro0987@gmail.com', N'duc0', N'123', N'Quản lý')
INSERT [dbo].[NguoiDung] ([MaKH], [TenKH], [email], [Tendangnhap], [matkhau], [chucvu]) VALUES (2, N'khanh', N'abc@gmail.com', N'd1', N'111', NULL)
INSERT [dbo].[NguoiDung] ([MaKH], [TenKH], [email], [Tendangnhap], [matkhau], [chucvu]) VALUES (3, N'aaa', N'd@gmail.com', N'ngunhu', N'123', NULL)
SET IDENTITY_INSERT [dbo].[NguoiDung] OFF
GO
SET IDENTITY_INSERT [dbo].[NickLM] ON 

INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (1, N'abc', N'aaa', N'Chưa Rank', 58, N'33', N'HinhDaiDien.jpg', 2, CAST(200.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (2, N'aa', N'bb', N'Chưa Rank', 54, N'27', N'HinhDaiDien.jpg', 1, CAST(100.000 AS Decimal(18, 3)), N'LienMinh', N'Đã bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (3, N'a', N'1', N'Chưa Rank', 120, N'27', N'HinhDaiDien.jpg', 1, CAST(100.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (4, N'b', N'c', N'Chưa Rank', 239, N'26', N'HinhDaiDien.jpg', 1, CAST(200.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (5, N'as', N'akaak', N'Chưa Rank', 70, N'26', N'HinhDaiDien.jpg', 1, CAST(100.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (6, N'1j', N'1kk1k1', N'Chưa Rank', 67, N'25', N'HinhDaiDien.jpg', 1, CAST(100.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (7, N'akla', N'1299', N'Chưa Rank', 66, N'25', N'HinhDaiDien.jpg', 1, CAST(100.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (8, N'l11ll1', N'lqlqlql', N'Chưa Rank', 66, N'25', N'HinhDaiDien.jpg', 2, CAST(100.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (9, N'alalla', N'alalla', N'Kim cương', 162, N'615', N'HinhDaiDien.jpg', 2, CAST(500.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (10, N'aksks', N'12', N'Cao thủ', 160, N'500', N'HinhDaiDien.jpg', 2, CAST(500.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (11, N'jqjqj', N'1221', N'Cao thủ', 150, N'515', N'HinhDaiDien.jpg', 2, CAST(500.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (12, N'11kk', N'1k1k1', N'Vàng 2', 154, N'521', N'HinhDaiDien.jpg', 2, CAST(500.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (13, N'l1l1l1', N'1k1klk', N'Bạc 3', 132, N'535', N'HinhDaiDien.jpg', 2, CAST(500.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (14, N'l1l1', N'l1l1l', N'Đồng', 111, N'512', N'HinhDaiDien.jpg', 2, CAST(500.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (15, N'akak', N'kkqkqk', N'Kim Cương', 111, N'12', N'HinhDaiDien.jpg', 3, CAST(100.000 AS Decimal(18, 3)), N'TocChien', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (16, N'ASDSA', N'qddw', N'Chưa Rank', 123, N'123', N'HinhDaiDien.jpg', 3, CAST(150.000 AS Decimal(18, 3)), N'TocChien', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (17, N'acas', N'ddwqw', N'Chưa Rank', 11, N'23', N'HinhDaiDien.jpg', 3, CAST(240.000 AS Decimal(18, 3)), N'TocChien', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (18, N'asdasd', N'dqdqw', N'Chưa Rank', 25, N'23', N'HinhDaiDien.jpg', 4, CAST(300.000 AS Decimal(18, 3)), N'TocChien', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (19, N'112', N'dwdq', N'Chưa Rank', 63, N'22', N'HinhDaiDien.jpg', 4, CAST(500.000 AS Decimal(18, 3)), N'TocChien', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (20, N'sdad', N'dwqdq', N'Chưa Rank', 24, N'33', N'HinhDaiDien.jpg', 4, CAST(390.000 AS Decimal(18, 3)), N'TocChien', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (21, N'qdwk', N'sasd', N'Chưa Rank', 66, N'11', N'HinhDaiDien.jpg', 4, CAST(450.000 AS Decimal(18, 3)), N'TocChien', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (22, N'qkqkwkd', N'asda', N'Chưa Rank', 53, N'1', N'HinhDaiDien.jpg', 5, CAST(80.000 AS Decimal(18, 3)), N'LQ', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (23, N'qkkd', N'aaa', N'Chưa Rank', 52, N'22', N'HinhDaiDien.jpg', 6, CAST(90.000 AS Decimal(18, 3)), N'LQ', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (24, N'qdkwk', N'11', N'Chưa Rank', 34, N'23', N'HinhDaiDien.jpg', 7, CAST(100.000 AS Decimal(18, 3)), N'LQ', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (25, N'dqkk', N'aasd', N'Chưa Rank', 54, N'24', N'HinhDaiDien.jpg', 8, CAST(200.000 AS Decimal(18, 3)), N'LQ', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (26, N'dqd', N'asd', N'Chưa Rank', 55, N'25', N'HinhDaiDien.jpg', 8, CAST(1000.000 AS Decimal(18, 3)), N'LQ', N'Chưa bán')
INSERT [dbo].[NickLM] ([MaNick], [TaiKhoan], [MatKhau], [Rank], [Tuong], [TrangPhuc], [HinhAnh], [MaCTDanhMuc], [Gia], [ThuVienChuaHinh], [TrangThai]) VALUES (50, N'duc0', N'11', N'chua', 1, N'1', N'HinhDaiDien.jpg', 1, CAST(100.000 AS Decimal(18, 3)), N'LienMinh', N'Chưa bán')
SET IDENTITY_INSERT [dbo].[NickLM] OFF
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_DonHang] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_DonHang]
GO
ALTER TABLE [dbo].[DanhMucCT]  WITH CHECK ADD FOREIGN KEY([MaDM])
REFERENCES [dbo].[DanhMuc] ([MaDM])
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_NguoiDung] FOREIGN KEY([MaKH])
REFERENCES [dbo].[NguoiDung] ([MaKH])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_NguoiDung]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_NickLM] FOREIGN KEY([MaNick])
REFERENCES [dbo].[NickLM] ([MaNick])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_NickLM]
GO
ALTER TABLE [dbo].[NickLM]  WITH CHECK ADD  CONSTRAINT [FK_NickLM_DanhMucCT] FOREIGN KEY([MaCTDanhMuc])
REFERENCES [dbo].[DanhMucCT] ([MaCTDanhMuc])
GO
ALTER TABLE [dbo].[NickLM] CHECK CONSTRAINT [FK_NickLM_DanhMucCT]
GO
USE [master]
GO
ALTER DATABASE [ShopBanNickGame] SET  READ_WRITE 
GO
