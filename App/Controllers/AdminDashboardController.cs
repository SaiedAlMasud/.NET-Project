using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs;

namespace App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private UserService userService;
        private PatientService patientService;
        private DoctorService doctorService;
        private AppointmentService appointmentService;
        private AdminService adminService;

        public AdminDashboardController(UserService userService, PatientService patientService, DoctorService doctorService, AppointmentService appointmentService, AdminService adminService)
        {
            this.userService = userService;
            this.patientService = patientService;
            this.doctorService = doctorService;
            this.appointmentService = appointmentService;
            this.adminService = adminService;
        }

        // View All Users
        public IActionResult Users()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            var admin = adminService.GetAdminByUserId(user.Id);
            ViewBag.AdminName = $"{admin.FirstName} {admin.LastName}";
            var users = userService.GetAllUsers();
            return View(users);
        }
    }
}