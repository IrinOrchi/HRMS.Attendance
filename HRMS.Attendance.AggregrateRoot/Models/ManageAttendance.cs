using HRMS.Attendance.DTOs;

public class ManageAttendance
{
    public int ID { get; set; }
    public int EmployeeID { get; set; }
    public virtual Employee? Employee { get; set; }
    public DateTime InTime { get; set; }
    public DateTime? OutTime { get; set; }
    public TimeSpan? TotalWorkingHours => OutTime.HasValue ? OutTime.Value - InTime : null;
    public string Status { get; set; }
    public bool IsOnLeave { get; set; }
    public DateTime AttendanceDate { get; set; }

    // Mapping method to DTO
    public AttendanceDto ToDto()
    {
        return new AttendanceDto
        {
            ID = this.ID,
            EmployeeID = this.EmployeeID,
            EmployeeName = this.Employee?.Name,
            Department = this.Employee?.Department,
            InTime = this.InTime,
            OutTime = this.OutTime,
            TotalWorkingHours = this.TotalWorkingHours,
            Status = this.Status,
            AttendanceDate = this.AttendanceDate
        };
    }

    // Static factory method to create new attendance instance
    public static ManageAttendance CreateAttendance(Employee employee)
    {
        return new ManageAttendance
        {
            EmployeeID = employee.ID,
            InTime = DateTime.Now,
            Status = "Present",
            AttendanceDate = DateTime.Now,
            IsOnLeave = false,
            Employee = employee
        };
    }
}
