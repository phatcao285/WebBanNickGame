using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNickGame.Models;
using PagedList;
using System.IO;

namespace WebBanNickGame.Controllers
{
    public class AdminController : Controller
    {
        DbDataContext dataContext = new DbDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XoaNick(int id)
        {
            var xoa = dataContext.NickLMs.FirstOrDefault(u=> u.MaNick == id);
            xoa.TrangThai = "";
            dataContext.SubmitChanges();
            return RedirectToAction("Nick", "Admin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNick(NickLM nick, HttpPostedFileBase image)
        {
            var HinhAnhTatCa = Request.Files.GetMultiple("HinhAnhTatCa");
            if (ModelState.IsValid)
            {
                dataContext.NickLMs.InsertOnSubmit(nick);
                dataContext.SubmitChanges();
                var dt = dataContext.NickLMs.OrderByDescending(x => x.MaNick).FirstOrDefault();
                // Xác định tên thư mục dựa vào mã chi tiết
                switch (dt.MaCTDanhMuc)
                {
                    case 1:
                    case 2:
                            dt.ThuVienChuaHinh= "LienMinh";
                            break;
                    case 3:
                    case 4:
                        dt.ThuVienChuaHinh = "TocChien";
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        dt.ThuVienChuaHinh = "LQ";
                        break;
                    default:
                        dt.ThuVienChuaHinh = "KhongXacDinh"; // Hoặc xử lý thêm trường hợp khác nếu cần
                        break;
                }
                // Lưu đối tượng NickLM vào cơ sở dữ liệu

                // Lưu hình đại diện vào thư mục tương ứng
                if (image != null && image.ContentLength > 0)
                {
                    var directoryPath = Server.MapPath("~/Image/Nick/" + dt.ThuVienChuaHinh + "/" + dt.MaNick);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var imagePath = Path.Combine(directoryPath, Path.GetFileName(image.FileName));
                    dt.HinhAnh = Path.GetFileName(image.FileName);
                    image.SaveAs(imagePath);

                    // Có thể xử lý hình đại diện ở đây nếu cần.
                }

                // Lưu tất cả hình vào thư mục tương ứng
                if (HinhAnhTatCa != null)
                {
                    foreach (var hinh in HinhAnhTatCa)
                    {
                        if (hinh != null && hinh.ContentLength > 0)
                        {
                            var directoryPath = Server.MapPath("~/Image/Nick/" + dt.ThuVienChuaHinh + "/" + dt.MaNick);

                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            var imagePath = Path.Combine(directoryPath, Path.GetFileName(hinh.FileName));
                            hinh.SaveAs(imagePath);

                            // Có thể xử lý từng hình ảnh ở đây nếu cần.
                        }
                    }
                }
                dt.TrangThai = "Chưa bán";
                dataContext.SubmitChanges();

            }

            return RedirectToAction("Nick");
        }

        public ActionResult ThemNick()
        {
            var danhMucList = dataContext.DanhMucCTs.Select(dm => new SelectListItem
            {
                Value = dm.MaCTDanhMuc.ToString(), // Giá trị
                Text = dm.Ten // Hiển thị
            }).ToList();

            // Thêm một tùy chọn mặc định vào đầu danh sách

            ViewBag.DanhMucList = danhMucList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaNick(NickLM nick, HttpPostedFileBase image)
        {
            var HinhAnhTatCa = Request.Files.GetMultiple("HinhAnhTatCa");
            if (ModelState.IsValid)
            {
                var dt = dataContext.NickLMs.FirstOrDefault(u => u.MaNick == nick.MaNick);
                dt.Rank = nick.Rank;
                dt.Tuong = nick.Tuong;
                dt.TrangPhuc = nick.TrangPhuc;
                dt.MaCTDanhMuc = nick.MaCTDanhMuc;
                dt.Gia = nick.Gia;
                // Xác định tên thư mục dựa vào mã chi tiết
                switch (dt.MaCTDanhMuc)
                {
                    case 1:
                    case 2:
                        dt.ThuVienChuaHinh = "LienMinh";
                        break;
                    case 3:
                    case 4:
                        dt.ThuVienChuaHinh = "TocChien";
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        dt.ThuVienChuaHinh = "LQ";
                        break;
                    default:
                        dt.ThuVienChuaHinh = "KhongXacDinh"; // Hoặc xử lý thêm trường hợp khác nếu cần
                        break;
                }
                // Lưu đối tượng NickLM vào cơ sở dữ liệu

                // Lưu hình đại diện vào thư mục tương ứng
                if (image != null && image.ContentLength > 0)
                {
                    var directoryPath = Server.MapPath("~/Image/Nick/" + dt.ThuVienChuaHinh + "/" + dt.MaNick);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var imagePath = Path.Combine(directoryPath, Path.GetFileName(image.FileName));
                    dt.HinhAnh = Path.GetFileName(image.FileName);
                    image.SaveAs(imagePath);

                    // Có thể xử lý hình đại diện ở đây nếu cần.
                }

                // Lưu tất cả hình vào thư mục tương ứng
                if (HinhAnhTatCa != null)
                {
                    foreach (var hinh in HinhAnhTatCa)
                    {
                        if (hinh != null && hinh.ContentLength > 0)
                        {
                            var directoryPath = Server.MapPath("~/Image/Nick/" + dt.ThuVienChuaHinh + "/" + dt.MaNick);

                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            var imagePath = Path.Combine(directoryPath, Path.GetFileName(hinh.FileName));
                            hinh.SaveAs(imagePath);

                            // Có thể xử lý từng hình ảnh ở đây nếu cần.
                        }
                    }
                }
                dataContext.SubmitChanges();

            }

            return RedirectToAction("Nick");
        }
        [HttpPost]
        public ActionResult ThemDM(DanhMuc dm)
        {
            dm.TrangThai = "Hoạt động";
            dataContext.DanhMucs.InsertOnSubmit(dm);
            dataContext.SubmitChanges();
            return RedirectToAction("DanhMuc");
        }
        public ActionResult ThemDM()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemCTDM(DanhMucCT dm, HttpPostedFileBase image)
        {
            dm.TrangThai = "Hoạt động";
            if (image != null && image.ContentLength > 0)
            {
                var directoryPath = Server.MapPath("~/Image/GiaRe");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var imagePath = Path.Combine(directoryPath, Path.GetFileName(image.FileName));
                dm.HinhAnh = Path.GetFileName(image.FileName);
                image.SaveAs(imagePath);

                // Có thể xử lý hình đại diện ở đây nếu cần.
            }
            dataContext.DanhMucCTs.InsertOnSubmit(dm);
            dataContext.SubmitChanges();
            return RedirectToAction("ChiTietDanhMuc");
        }
        public ActionResult ThemCTDM()
        {
            var danhMucList = dataContext.DanhMucs.Select(dm => new SelectListItem
            {
                Value = dm.MaDM.ToString(), // Giá trị
                Text = dm.TenDM // Hiển thị
            }).ToList();

            // Thêm một tùy chọn mặc định vào đầu danh sách

            ViewBag.DanhMucList = danhMucList;
            return View();
        }
        public ActionResult XoaDanhMuc(int id)
        {
            var dm = dataContext.DanhMucs.FirstOrDefault(u => u.MaDM == id);
            dm.TrangThai = "Dừng hoạt động";
            dataContext.SubmitChanges();
            return RedirectToAction("DanhMuc");
        }
        public ActionResult HoatdongDanhMuc(int id)
        {
            var dm = dataContext.DanhMucs.FirstOrDefault(u => u.MaDM == id);
            dm.TrangThai = "Hoạt động";
            dataContext.SubmitChanges();
            return RedirectToAction("DanhMuc");
        }
        public ActionResult XoaCTDanhMuc(int id)
        {
            var dm = dataContext.DanhMucCTs.FirstOrDefault(u => u.MaCTDanhMuc == id);
            dm.TrangThai = "Dừng hoạt động";
            dataContext.SubmitChanges();
            return RedirectToAction("ChiTietDanhMuc");
        }
        public ActionResult HoatdongCTDanhMuc(int id)
        {
            var dm = dataContext.DanhMucCTs.FirstOrDefault(u => u.MaCTDanhMuc == id);
            dm.TrangThai = "Hoạt động";
            dataContext.SubmitChanges();
            return RedirectToAction("ChiTietDanhMuc");
        }
        public ActionResult Update(int id)
        {
            var tk = dataContext.NguoiDungs.FirstOrDefault(u=>u.MaKH==id);
            tk.chucvu = "Quản lý";
            dataContext.SubmitChanges();
            return RedirectToAction("TaiKhoan");
        }
        public ActionResult SuaNick(int id) {
            var danhMucList = dataContext.DanhMucCTs.Select(dm => new SelectListItem
            {
                Value = dm.MaCTDanhMuc.ToString(), // Giá trị
                Text = dm.Ten // Hiển thị
            }).ToList();

            // Thêm một tùy chọn mặc định vào đầu danh sách

            ViewBag.DanhMucList = danhMucList;
            return View(dataContext.NickLMs.FirstOrDefault(u => u.MaNick == id));
        }
        public ActionResult Nick(int? page)
        {
            int pageSize = 5; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là trang 1 nếu không có tham số page

            var products = dataContext.NickLMs.Where(i=> i.TrangThai.Contains("Chưa bán")).ToPagedList(pageNumber, pageSize); // Thực hiện phân trang trực tiếp trên truy vấn LINQ

            return View(products);
        }
        public ActionResult DanhMuc()
        {
            return View(dataContext.DanhMucs.Where(u=> u.TrangThai.Contains("Hoạt động")).ToList());
        }
        public ActionResult ChiTietDanhMuc()
        {

            return View(dataContext.DanhMucCTs.Where(u => u.TrangThai.Contains("Hoạt động")).ToList());
        }
        public ActionResult TaiKhoan()
        {
            return View(dataContext.NguoiDungs.ToList());
        }
        public ActionResult DonHang()
        {
            return View(dataContext.DonHangs.ToList());
        }
    }
}