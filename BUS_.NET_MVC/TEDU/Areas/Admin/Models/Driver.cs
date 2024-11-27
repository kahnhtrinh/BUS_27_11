
namespace TEDU.Areas.Admin.Models
{
    public class Driver : Employee
    {
        public string LicenseNumber { get; set; }
        public int ExperienceYear { get; set; }
        public int BusRouteId { get; set; }
    }
}