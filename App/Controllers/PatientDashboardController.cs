using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs;

namespace App.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientDashboardController : Controller
    {
        private AppointmentService appointmentService;
        private PatientService patientService;
        private DoctorService doctorService;
        private UserService userService;

        public PatientDashboardController(AppointmentService appointmentService, PatientService patientService, DoctorService doctorService, UserService userService)
        {
            this.appointmentService = appointmentService;
            this.patientService = patientService;
            this.doctorService = doctorService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            var patient = patientService.GetPatientByUserId(user.Id);
            ViewBag.PatientName = $"{patient.FirstName} {patient.LastName}";
            return View();
        }

        public IActionResult MyAppointments()
        {
            var user = userService.GetUserByEmail(User.Identity.Name);
            var patient = patientService.GetPatientByUserId(user.Id);
            var appointments = appointmentService.GetAppointmentsByPatientId(patient.Id);
            return View(appointments);
        }

        [HttpGet]
        public IActionResult BookAppointment()
        {
            ViewBag.Doctors = doctorService.GetAllDoctors();
            return View();
        }

        [HttpPost]
        public IActionResult BookAppointment(AppointmentDTO appointment)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUserByEmail(User.Identity.Name);
                var patient = patientService.GetPatientByUserId(user.Id);
                var doctor = doctorService.GetDoctorById(appointment.DoctorId);
                appointment.PatientId = patient.Id;
                appointment.DoctorId = doctor.Id;
                //appointment.Status = "Scheduled";
                //appointment.PaymentStatus = "Pending";
                appointment.FinalAmount = doctor.ConsultationFee;

                var result = appointmentService.CreateAppointment(appointment);
                if (result)
                {
                    TempData["Success"] = "Appointment booked successfully!";
                    return RedirectToAction("MyAppointments");
                }
            }
            ViewBag.Doctors = doctorService.GetAllDoctors();
            return View(appointment);
        }

        public IActionResult CancelAppointment(int id)
        {
            appointmentService.CancelAppointment(id);
            TempData["Success"] = "Appointment cancelled successfully!";
            return RedirectToAction("MyAppointments");
        }
    }
}