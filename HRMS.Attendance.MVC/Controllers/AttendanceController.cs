using Microsoft.AspNetCore.Mvc;



public class AttendanceController : Controller
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    public async Task<IActionResult> Index()
    {
        var attendances = await _attendanceService.GetAllAttendances();
        return View(attendances);
    }

    public async Task<IActionResult> MarkAttendance(int employeeId)
    {
        await _attendanceService.MarkAttendance(employeeId);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EmployeeList()
    {
        var employees = await _attendanceService.GetAllEmployees();
        return View(employees);
    }
}
