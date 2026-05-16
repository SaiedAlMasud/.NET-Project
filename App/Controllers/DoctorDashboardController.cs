using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs;

namespace App.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorDashboardController : Controller
    {
        private AppointmentService appointmentService;
        private DoctorService doctorService;
        private UserService userService;
        private PatientService patientService;

        public DoctorDashboardController(AppointmentService appointmentService, DoctorService doctorService, UserService userService, PatientService patientService)
        {
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
            this.userService = userService;
            this.patientService = patientService;
        }

        public IActionResult Index()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            var doctor = doctorService.GetDoctorByUserId(user.Id);
            ViewBag.DoctorName = $"Dr. {doctor.FirstName} {doctor.LastName}";
            return View();
        }

        public IActionResult MyAppointments()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            var doctor = doctorService.GetDoctorByUserId(user.Id);
            var appointments = appointmentService.GetAppointmentsByDoctorId(doctor.Id);
            ViewBag.DoctorName = $"Dr. {doctor.FirstName} {doctor.LastName}";
            return View(appointments);
        }

        public IActionResult TodayAppointments()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            var doctor = doctorService.GetDoctorByUserId(user.Id);
            var appointments = appointmentService.GetTodayAppointmentsByDoctorId(doctor.Id);
            return View(appointments);
        }

        public IActionResult UpdateAppointmentStatus(int id, string status)
        {
            appointmentService.UpdateAppointmentStatus(id, status);
            TempData["Success"] = "Appointment status updated!";
            return RedirectToAction("MyAppointments");
        }

        public IActionResult Schedule()
        {
            return View();
        }
    }
}