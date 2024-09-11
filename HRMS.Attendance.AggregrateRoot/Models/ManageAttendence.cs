using HRMS.Attendance.AggregrateRoot.Models;

public class ManageAttendence
{
    public int ID { get; set; }

    public int EmployeeID { get; set; }
    public virtual Employee? Employee { get; set; }

    public DateTime InTime { get; set; }
    public DateTime? OutTime { get; set; }

    public TimeSpan? TotalWorkingHours
    {
        get
        {
            return OutTime.HasValue ? OutTime.Value - InTime : null;
        }
    }

    public string Status { get; set; }

    // Check if employee is on leave for that day
    public bool IsOnLeave { get; set; }
    public DateTime AttendanceDate { get; set; }
}
