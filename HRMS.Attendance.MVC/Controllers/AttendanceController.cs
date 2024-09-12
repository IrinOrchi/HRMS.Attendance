using HRMS.Attendance.DTOs;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRMS.Attendance.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // GET: Attendance/EmployeeList
        public async Task<IActionResult> EmployeeList()
        {
            var employees = await _attendanceService.GetEmployeeListForMarkAttendance();
            return View(employees);
        }

        // POST: Attendance/MarkAttendance
        [HttpPost]
        public async Task<IActionResult> MarkAttendance(int employeeId)
        {
            await _attendanceService.MarkAttendance(employeeId);
            return RedirectToAction(nameof(AttendanceListWithInTime));
        }

        // GET: Attendance/AttendanceListWithInTime
        public async Task<IActionResult> AttendanceListWithInTime()
        {
            var attendances = await _attendanceService.GetAttendanceListWithInTime();
            return View(attendances);
        }

        // POST: Attendance/ExitAttendance
        [HttpPost]
        public async Task<IActionResult> ExitAttendance(int attendanceId)
        {
            await _attendanceService.ExitAttendance(attendanceId);
            return RedirectToAction(nameof(FinalAttendanceList));
        }

        // GET: Attendance/FinalAttendanceList
        public async Task<IActionResult> FinalAttendanceList()
        {
            var attendances = await _attendanceService.GetFinalAttendanceList();
            return View(attendances);
        }
    }
}
