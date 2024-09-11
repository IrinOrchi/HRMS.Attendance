using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Attendance.DTOs
{
    public class LeaveRequestDto
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; } // From Employee
        public string EmployeeDepartment { get; set; } // From Employee
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } // "Pending" or "Approved"
    }


}
