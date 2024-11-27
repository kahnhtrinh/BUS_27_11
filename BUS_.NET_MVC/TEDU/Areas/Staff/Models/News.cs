using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Staff.Models
{
    public class News
    {
        public int NewsId { get; set; } // Mã tin tức
        public string Title { get; set; } // Tiêu đề bài viết
        public string Content { get; set; } // Nội dung bài viết
        public DateTime PublishedDate { get; set; } // Ngày đăng
    }

}