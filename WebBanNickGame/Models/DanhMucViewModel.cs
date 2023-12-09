using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanNickGame.Models
{
    public class DanhMucViewModel
    {
        public IEnumerable<DanhMuc> DanhMucList { get; set; }
        public IEnumerable<DanhMucCT> ChiTietDanhMucList { get; set; }
    }
}