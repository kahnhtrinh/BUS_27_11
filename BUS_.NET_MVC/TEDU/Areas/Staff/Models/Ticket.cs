using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Staff.Models
{
    public class Ticket
    {
        public int TicketId { get; set; } // Mã vé
        public int TripId { get; set; } // Mã chuyến (liên kết tới Trip)
        public Trip Trip { get; set; } // Tham chiếu đến chuyến xe liên quan

        public string PassengerName { get; set; } // Tên hành khách
        public string PassengerPhone { get; set; } // Số điện thoại hành khách
        public string PassengerEmail { get; set; } // Email
        public string SeatNumber { get; set; } // Số ghế
        public bool IsPaid { get; set; } // Trạng thái thanh toán (true = đã thanh toán)
        public DateTime BookingDate { get; set; } // Ngày đặt vé
    }

   

}