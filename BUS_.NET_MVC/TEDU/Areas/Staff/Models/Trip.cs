using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace TEDU.Areas.Staff.Models
{
    public class Trip
    {
        
        public int TripId { get; set; } // Mã chuyến
        public int RouteId { get; set; } // Mã tuyến (liên kết tới Route)
        public RouteInfo RouteInfo { get; set; } // Tham chiếu đến tuyến xe liên quan
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureTime { get; set; } // Ngày giờ khởi hành
        public string DriverName { get; set; } // Tên tài xế
        public string VehicleNumber { get; set; } // Biển số xe
        public decimal TicketPrice { get; set; } // Giá vé
        public string Status { get; set; } // Trạng thái (Đang chờ, Đã khởi hành, Hoàn thành, Hủy bỏ)
    }

}