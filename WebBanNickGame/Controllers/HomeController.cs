using Microsoft.Owin.BuilderProperties;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using WebBanNickGame.Models;
using System.Text.RegularExpressions;

namespace WebBanNickGame.Controllers
{
    public class HomeController : Controller
    {
        DbDataContext dataContext = new DbDataContext();
        public ActionResult DonHang(int? page)
        {
            int pageSize = 5; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là trang 1 nếu không có tham số page
            var nguoiDung = Session["TaiKhoan"] as NguoiDung;
            int? maNguoiDung = nguoiDung.MaKH;
            var donhang = dataContext.DonHangs.Where(d => d.MaKH == maNguoiDung).ToPagedList(pageNumber, pageSize);
            return View(donhang);
        }
        public ActionResult XemCTDonHang(long? maDonHang)
        {
            var chiTietDonHang = dataContext.ChiTietDonHangs.Where(s => s.MaDH == maDonHang).ToList();

            return View(chiTietDonHang);

        }
        public ActionResult Infor(int id)
        {
            var nd = dataContext.NguoiDungs.FirstOrDefault(u => u.MaKH == id);

            return View(nd);

        }

        [AcceptVerbs("GET", "POST")]
        public ActionResult LocTheoGia(int? idloc, int? page, bool? priceAll, bool? price1, bool? price2, bool? price3, bool? price4)
        {
            Session["price1"] = false;
            Session["price2"] = false;
            Session["price3"] = false;
            Session["price4"] = false;
            int pageSize = 6; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là trang 1 nếu không có tham số page
            var query = dataContext.NickLMs.Where(c => c.MaCTDanhMuc == idloc && c.TrangThai.Contains("Chưa bán"));
            if (priceAll != true)
            {
                if (price1 == true)
                {
                    query = query.Where(p => p.Gia >= 0 && p.Gia <= 100);
                    Session["price1"] = price1;
                }
                if (price2 == true)
                {
                    query = query.Where(p => p.Gia > 100 && p.Gia <= 200);
                    Session["price2"] = price2;
                }
                if (price3 == true)
                {
                    query = query.Where(p => p.Gia > 200 && p.Gia <= 500);
                    Session["price3"] = price3;
                }
                if (price4 == true)
                {
                    query = query.Where(p => p.Gia > 500);
                    Session["price4"] = price4;
                }
            }

            var shop = query.ToPagedList(pageNumber, pageSize);
            return View(shop);
        }
        [AcceptVerbs("GET", "POST")]
        public ActionResult LocTheoTuong(int? idloc, int? page, bool? tuongAll, bool? tuong1, bool? tuong2, bool? tuong3, bool? tuong4)
        {
            Session["tuong1"] = false;
            Session["tuong2"] = false;
            Session["tuong3"] = false;
            Session["tuong4"] = false;
            int pageSize = 6; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là trang 1 nếu không có tham số page
            var query = dataContext.NickLMs.Where(c => c.MaCTDanhMuc == idloc && c.TrangThai.Contains("Chưa bán"));
            if (tuongAll != true)
            {
                if (tuong1 == true)
                {
                    query = query.Where(p => p.Tuong >= 0 && p.Tuong <= 100);
                    Session["tuong1"] = tuong1;
                }
                if (tuong2 == true)
                {
                    query = query.Where(p => p.Tuong > 100 && p.Tuong <= 300);
                    Session["tuong2"] = tuong2;
                }
                if (tuong3 == true)
                {
                    query = query.Where(p => p.Tuong > 300 && p.Tuong <= 600);
                    Session["tuong3"] = tuong3;
                }
                if (tuong4 == true)
                {
                    query = query.Where(p => p.Tuong > 600);
                    Session["tuong4"] = tuong4;
                }
            }

            var shop = query.ToPagedList(pageNumber, pageSize);
            return View(shop);
        }
        public void GuiEmailDonHang(long maDonHang)
        {
            // Tìm đơn hàng dựa trên mã đơn hàng
            var donHang = dataContext.DonHangs.SingleOrDefault(dh => dh.MaDH == maDonHang);
            var kh = dataContext.NguoiDungs.SingleOrDefault(k => k.MaKH == donHang.MaKH);
            var ct = dataContext.ChiTietDonHangs.SingleOrDefault(c => c.MaDH == maDonHang);
            if (donHang != null)
            {
                string email = kh.email; // Lấy email của khách hàng từ đơn hàng
                string subject = "Xác nhận đơn hàng #" + maDonHang;
                string body = "Cảm ơn bạn đã đặt hàng. Dưới đây là thông tin đơn hàng của bạn:" +
                    "<table>" +
                    "<tr><td>Mã đơn hàng:</td><td>" + maDonHang + "</td></tr>" +
                    "<tr><td>Ngày đặt hàng:</td><td>" + donHang.ThoiGianMua.ToString("dd/MM/yyyy") + "</td></tr>" +
                    "<tr><td>Tổng giá trị:</td><td>" + donHang.GiaTien.ToString("N0") + ",000 VND</td></tr>" +
                    // Thêm thông tin khác của đơn hàng vào đây
                    "</table>";

                GuiEmailDonHang(email, subject, body);
            }
        }

        private void GuiEmailDonHang(string email, string subject, string body)
        {
            var fromAddress = new MailAddress("2124802010317@student.tdmu.edu.vn", "Phát");
            var toAddress = new MailAddress(email, "Recipient Name");

            const string fromPassword = "uemz bpmj hzor yyyf"; // Điền mật khẩu email của bạn
            const string smtpServer = "smtp.gmail.com"; // Điền SMTP server của bạn (ở đây dùng Gmail)
            const int smtpPort = 587; // Cổng SMTP của bạn (ở đây dùng Gmail)

            var smtp = new SmtpClient
            {
                Host = smtpServer,
                Port = smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
        [AcceptVerbs("GET", "POST")]
        public ActionResult TimKiem(int? uid, int? page,string s)
        {
            Session["s"] = s;
            Session["UID"] = uid;
            int pageSize = 6; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là trang 1 nếu không có tham số page
            var query = dataContext.NickLMs.Where(c => c.MaCTDanhMuc == uid && c.TrangThai.Contains("Chưa bán") && c.Rank.Contains(s)).ToPagedList(pageNumber, pageSize);
            return View(query);
        }
        [AcceptVerbs("GET", "POST")]
        public ActionResult SapXepTangDan(int? uid, int? page)
        {
            Session["UID"] = uid;
            int pageSize = 6; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là trang 1 nếu không có tham số page
            var query = dataContext.NickLMs.Where(c => c.MaCTDanhMuc == uid && c.TrangThai.Contains("Chưa bán")).OrderBy(g => g.Gia).ToPagedList(pageNumber, pageSize);
            return View(query);
        }
        [AcceptVerbs("GET", "POST")]
        public ActionResult SapXepGiamDan(int? uid, int? page)
        {
            Session["UID"] = uid;
            int pageSize = 6; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là trang 1 nếu không có tham số page
            var query = dataContext.NickLMs.Where(c => c.MaCTDanhMuc == uid && c.TrangThai.Contains("Chưa bán")).OrderByDescending(g => g.Gia).ToPagedList(pageNumber, pageSize);
            return View(query);
        }
        public ActionResult ShopLM(int? uid, int? page)
        {
            Session["UID"] = uid;
            int pageSize = 6; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là trang 1 nếu không có tham số page
            var query = dataContext.NickLMs.Where(c => c.MaCTDanhMuc == uid && c.TrangThai.Contains("Chưa bán")).ToPagedList(pageNumber, pageSize);
            return View(query);
        }
        public ActionResult Ngung()
        {
            return View();
        }
        public int ConvertDecimalToInteger(decimal decimalValue)
        {
            // Ép kiểu số DECIMAL thành số nguyên và trả về kết quả
            return (int)decimalValue;
        }
        public ActionResult SendMail(string name, string email, string subject, string message)
        {
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "thanhphat282003llml@gmail.com";
            string smtpPassword = "lyky berc ilct ofuj";
            Session["EmailSuccessMessage"] = "Email đã được gửi thành công.";
            // Tạo đối tượng SmtpClient để gửi email
            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                // Tạo đối tượng MailMessage để gửi email
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(email);
                mailMessage.To.Add("thanhphat282003llml@gmail.com"); // Địa chỉ email của bạn
                mailMessage.Subject = subject;
                mailMessage.Body = $"Name: {name}\nEmail: {email}\nMessage: {message}";
                
                // Gửi email
                client.Send(mailMessage);
            }

            return RedirectToAction("Contact");
        }
        public ActionResult ThanhToan(int id)
        {

            var nguoidung = Session["TaiKhoan"] as NguoiDung;
            int makh = nguoidung.MaKH;
            var nick = dataContext.NickLMs.SingleOrDefault(u => u.MaNick == id);
            DonHang dh = new DonHang
            {
                MaDH = DateTime.Now.Ticks,
                ThoiGianMua = DateTime.Now,
                MaKH = makh,
                GiaTien = nick.Gia,
                MaNick = id
            };
            dataContext.DonHangs.InsertOnSubmit(dh);
            dataContext.SubmitChanges();
            var urlPayment = "";
            Session["MaDH"] = dh.MaDH;
                //Get Config Info
                string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
                string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
                string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

                //Get payment input
                //Save order to db

                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();
                long amount = ConvertDecimalToInteger(dh.GiaTien)*1000;
            Debug.WriteLine(amount);
                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (amount*100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
                long madonhang = (long)dh.MaDH;
                vnpay.AddRequestData("vnp_CreateDate", dh.ThoiGianMua.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());

                vnpay.AddRequestData("vnp_Locale", "vn");
                vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + madonhang);
                vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_TxnRef", madonhang.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                //Add Params of 2.1.0 Version
                //Billing

                urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                return Redirect(urlPayment);
        }
        public ActionResult Return()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        long code = (long)Session["MaDH"];

                        var order = dataContext.DonHangs.FirstOrDefault(x => x.MaDH == code);
                        var nick = dataContext.NickLMs.FirstOrDefault(x => x.MaNick == order.MaNick);
                        ChiTietDonHang ct = new ChiTietDonHang
                        {
                            MaDH = code,
                            TaiKhoan = nick.TaiKhoan,
                            Matkhau = nick.MatKhau
                        };
                        dataContext.ChiTietDonHangs.InsertOnSubmit(ct); 
                        dataContext.SubmitChanges();
                        nick.TrangThai = "Đã bán";
                        dataContext.SubmitChanges();
                        GuiEmailDonHang(order.MaDH);
                        return View(order);
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                    }
                }
            }

            return RedirectToAction("Failed");
        }
        public ActionResult ShopDetailsLM(int id,string hinh)
        {
            Session["Ma"] = id;
            Session["hinh"] = hinh;
            var nick = dataContext.NickLMs.SingleOrDefault(v=> v.MaNick==id);
            List<NickLM> danhSachNickLM = dataContext.NickLMs.Where(u => u.Gia == nick.Gia).ToList();
            Session["DanhSachNickLM"] = danhSachNickLM;

            return View(nick);
        }
        public ActionResult Contact()
        {
            return View();
        }
        // GET: Home
        public ActionResult Index()

        {
            Session["banner1"] = false;
            Session["banner2"] = false;
            Session["banner3"] = false;
            var danhMucList = dataContext.DanhMucs.Where(u => !u.TrangThai.Contains("Dừng hoạt động")).ToList();
            var chiTietDanhMucList = dataContext.DanhMucCTs.Where(u=> !u.TrangThai.Contains("Dừng hoạt động") && danhMucList.Select(dm => dm.MaDM).Contains(u.MaDM.Value)).ToList();
            foreach (var item in chiTietDanhMucList)
            {
                switch (item.MaCTDanhMuc) {
                    case 1:
                        Session["banner1"] = true;
                            break;
                    case 3:
                        Session["banner2"] = true;
                        break;
                    case 7:
                        Session["banner3"] = true;
                        break;
                }
            }
            var viewModel = new DanhMucViewModel
            {
                DanhMucList = danhMucList,
                ChiTietDanhMucList = chiTietDanhMucList
            };
            return View(viewModel);
        }
        
        private bool IsPasswordValid(string password)
        {
            // Sử dụng biểu thức chính quy để kiểm tra
            var regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$");
            return regex.IsMatch(password);
        }
        public ActionResult TaoTK(string username, string name, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(username))
            {
                ViewData["Err4"] = "Tên đăng nhập không được để trống";
                ViewData["Err3"] = "Đăng ký thất bại bấm vào nút đăng ký để xem lý do";
            }
            else if (String.IsNullOrEmpty(name))
            {
                ViewData["Err6"] = "Họ tên không được để trống";
                ViewData["Err3"] = "Đăng ký thất bại bấm vào nút đăng ký để xem lý do";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Err7"] = "Email không được để trống";
                ViewData["Err3"] = "Đăng ký thất bại bấm vào nút đăng ký để xem lý do";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Errpass"] = "Mật khẩu không được để trống";
                ViewData["Err3"] = "Đăng ký thất bại bấm vào nút đăng ký để xem lý do";
            }
            else if (!IsPasswordValid(password))
            {
                ViewData["Errpass"] = "Mật khẩu phải 8 chũ cái trong đó có 1 kí tự viết hoa, 1 kí tự số và 1 kí tự đặc biệt.";
                ViewData["Err3"] = "Đăng ký thất bại bấm vào nút đăng ký để xem lý do";
            }
            else if (password != confirmPassword)
            {
                ViewData["Err5"] = "Mật khẩu không trùng khớp.";
                ViewData["Err3"] = "Đăng ký thất bại bấm vào nút đăng ký để xem lý do";
            }
            else
            {
                var nguoiDung = new NguoiDung()
                {
                    TenKH = name,
                    Tendangnhap = username,
                    email = email,
                    matkhau = password
                };

                dataContext.NguoiDungs.InsertOnSubmit(nguoiDung);

                // Lưu thay đổi vào cơ sở dữ liệu
                dataContext.SubmitChanges();
                ViewData["Err3"] = "Đăng ký thành công xin mời đăng nhập";
                return View("DangNhap");
            }
            return View("DangNhap");
            // Trả về trang đăng nhập
        }

        public ActionResult Logout()
        {
            // Xóa dữ liệu từ Session
            Session.Remove("TaiKhoan");

            // Hoặc bạn có thể sử dụng Session.Clear() để xóa toàn bộ dữ liệu trong Session
            // Session.Clear();

            // Điều hướng đến trang đăng nhập hoặc trang chính (tuỳ theo logic của bạn)
            return RedirectToAction("Index"); // Thay thế "Login" và "Account" bằng tên action và controller tương ứng của trang đăng

        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tenDN = collection["TenDN"];
            var matKhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                NguoiDung kh = dataContext.NguoiDungs.SingleOrDefault(n => n.Tendangnhap == tenDN && n.matkhau == matKhau);
                if (kh != null)
                {
                    ViewData["Err3"] = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    return Redirect("~/Home/Index");
                }
                else
                {
                    ViewData["Err3"] = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
    }
}