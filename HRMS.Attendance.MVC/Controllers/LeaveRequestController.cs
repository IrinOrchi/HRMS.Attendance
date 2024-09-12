using HRMS.Attendance.DTOs;
using HRMS.Attendance.Handler.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class LeaveRequestController : Controller
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    // 1. View all pending leave requests
    public async Task<IActionResult> Index()
    {
        var pendingRequests = await _leaveRequestService.GetAllPendingRequests();
        return View(pendingRequests);
    }

    // 2. View all approved leave requests
    public async Task<IActionResult> ApprovedList()
    {
        var approvedRequests = await _leaveRequestService.GetAllApprovedRequests();
        return View(approvedRequests);
    }

    // 3. Approve a leave request
    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        await _leaveRequestService.ApproveLeaveRequest(id);
        return RedirectToAction("Index");
    }

    // 4. Delete a leave request
    public async Task<IActionResult> Delete(int id)
    {
        var leaveRequest = await _leaveRequestService.GetById(id);
        return View(leaveRequest);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leaveRequestService.DeleteLeaveRequest(id);
        return RedirectToAction("Index");
    }

    // 5. Edit an approved leave request (GET: shows the edit form)
    public async Task<IActionResult> Edit(int id)
    {
        var leaveRequest = await _leaveRequestService.GetById(id);
        return View(leaveRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(LeaveRequestDto leaveRequestDto)
    {
        if (ModelState.IsValid)
        {
            await _leaveRequestService.UpdateApprovedLeaveRequest(leaveRequestDto);
            return RedirectToAction("ApprovedList");
        }
        return View(leaveRequestDto);
    }

    // 6. View the details of a leave request
    public async Task<IActionResult> Details(int id)
    {
        var leaveRequest = await _leaveRequestService.GetById(id);
        return View(leaveRequest);
    }
}
