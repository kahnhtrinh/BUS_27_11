using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Admin.Models
{
    public class Contact
    {
        public int ContactId { get; set; } // Mã liên hệ
        public string CustomerName { get; set; } // Tên khách hàng
        public string Email { get; set; } // Email khách hàng
        public string Phone { get; set; } // Số điện thoại
        public string Message { get; set; } // Nội dung liên hệ
        public DateTime SentDate { get; set; } // Ngày gửi
    }

}