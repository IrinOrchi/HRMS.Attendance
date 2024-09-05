using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Attendance.AggregrateRoot.Models
{
    public class LeaveRequest
    {
        [Key]
        public int ID { get; set; }

        public int EmployeeID { get; set; } // Foreign key for Employee
        public virtual Employee? Employee { get; set; } // Navigation property

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

     
        public required string Reason { get; set; }

        public required string Status { get; set; } // "Pending" or "Approved"
    }
}
