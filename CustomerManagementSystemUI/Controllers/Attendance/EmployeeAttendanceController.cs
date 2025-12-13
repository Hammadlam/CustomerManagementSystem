using CustomerManagementSystemUI.UIModels;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemUI.Controllers.Attendance
{
    public class EmployeeAttendanceController : Controller
    {
        //public IActionResult EmployeeAttendance(AttendanceVM model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    return RedirectToAction("EmployeeAttendance");
        //}
      public IActionResult EmployeeAttendance()
        {
            return View();
        }
    }
}
