using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Attendance.AggregrateRoot.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Designation { get; set; }
        public required string Department { get; set; }
        public required string NID { get; set; }
        public string? FathersName { get; set; }
        public string? MothersName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string? Address { get; set; }
        public required string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
