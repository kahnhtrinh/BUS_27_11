using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Staff : Employee
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
