using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE_CRUD.Models
{
    public class Employee
    {
        
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string EmailID { get; set; }
    }
}
