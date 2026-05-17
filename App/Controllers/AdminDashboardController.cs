using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs;
using App.Models;

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
            var currentAdmin = adminService.GetAdminByUserId(user.Id);
            ViewBag.AdminName = $"{currentAdmin.FirstName} {currentAdmin.LastName}";
            var users = userService.GetAllUsers();
            return View(users);
        }
        // View User Details
        public IActionResult UserDetails(int id)
        {
            var userlogged = userService.GetUserByEmail(User.Identity.Name);
            var adminlogged = adminService.GetAdminByUserId(userlogged.Id);
            ViewBag.AdminName = $"{adminlogged.FirstName} {adminlogged.LastName}";
            var user = userService.GetUserById(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Users");
            }

            ViewBag.PatientInfo = null;
            ViewBag.DoctorInfo = null;
            ViewBag.AdminInfo = null;

            if (user.Role == "Patient")
            {
                var patient = patientService.GetPatientByUserId(user.Id);
                ViewBag.PatientInfo = patient;
            }
            else if (user.Role == "Doctor")
            {
                var doctor = doctorService.GetDoctorByUserId(user.Id);
                ViewBag.DoctorInfo = doctor;
            }
            else if (user.Role == "Admin")
            {
                // renamed to avoid shadowing any 'admin' declared in an outer scope
                var adminInfos = adminService.GetAdminByUserId(user.Id);
                ViewBag.AdminInfo = adminInfos;
            }

            return View(user);
        }

        // Add New User - GET (with role parameter)
        [HttpGet]
        public IActionResult AddUser(string role = "Patient")
        {
            ViewBag.Role = role;
            return View();
        }

        // Add New User - POST
        [HttpPost]
        public IActionResult AddUser(RegisterViewModel model, string Role)
        {
            if (Role != "Doctor")
            {
                ModelState.Remove("Specialization");
                ModelState.Remove("ConsultationFee");
            }

            if (ModelState.IsValid)
            {
                if (userService.EmailExists(model.Email))
                {
                    ModelState.AddModelError("Email", "Email already registered");
                    ViewBag.Role = Role;
                    return View(model);
                }

                // Create user
                var registerDto = new UserRegisterDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Role = Role
                };

                bool userCreated = userService.CreateUser(registerDto);

                if (userCreated)
                {
                    var user = userService.GetUserByEmail(model.Email);

                    if (Role == "Patient")
                    {
                        var patient = new PatientRegisterDTO
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            DateOfBirth = model.DateOfBirth,
                            Gender = model.Gender
                        };
                        patientService.CreatePatient(patient, user.Id);
                    }
                    else if (Role == "Doctor")
                    {
                        var doctor = new DoctorRegisterDTO
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Specialization = model.Specialization,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            ConsultationFee = model.ConsultationFee
                        };
                        doctorService.CreateDoctor(doctor, user.Id);
                    }
                    else if (Role == "Admin")
                    {
                        var admin = new AdminRegisterDTO
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            DateOfBirth = model.DateOfBirth,
                            Gender = model.Gender
                        };
                        adminService.CreateAdmin(admin, user.Id);
                    }

                    TempData["Success"] = "User added successfully!";
                    return RedirectToAction("Users");
                }
            }

            ViewBag.Role = Role;
            return View(model);
        }
        // Reset Password - GET
        [HttpGet]
        public IActionResult ResetPassword(int id)
        {
            var user = userService.GetUserById(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Users");
            }

            ViewBag.UserEmail = user.Email;
            ViewBag.UserId = user.Id;
            return View();
        }

        // Reset Password - POST
        [HttpPost]
        public IActionResult ResetPassword(int id, string newPassword, string confirmPassword)
        {
            var user = userService.GetUserById(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Users");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                TempData["Error"] = "Password is required.";
                return RedirectToAction("ResetPassword", new { id = id });
            }

            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "Passwords do not match.";
                return RedirectToAction("ResetPassword", new { id = id });
            }

            if (newPassword.Length < 6)
            {
                TempData["Error"] = "Password must be at least 6 characters.";
                return RedirectToAction("ResetPassword", new { id = id });
            }

            var result = userService.ResetPassword(id, newPassword);

            if (result)
            {
                TempData["Success"] = $"Password reset successfully for {user.Email}!";
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Error"] = "Failed to reset password.";
                return RedirectToAction("ResetPassword", new { id = id });
            }
        }
        // Delete User
        public IActionResult DeleteUser(int id)
        {
            var user = userService.GetUserById(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Users");
            }

            // Don't allow deleting your own account
            var currentUser = userService.GetUserByEmail(User.Identity.Name);
            if (currentUser.Id == id)
            {
                TempData["Error"] = "You cannot delete your own account.";
                return RedirectToAction("Users");
            }

            var result = userService.DeleteUser(id);

            if (result)
            {
                TempData["Success"] = $"User {user.Email} has been deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to delete user.";
            }

            return RedirectToAction("Users");
        }
    }
}