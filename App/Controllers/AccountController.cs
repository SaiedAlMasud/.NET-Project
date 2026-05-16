using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using App.Models;
using DAL.Entities;
using DAL.Repos;

namespace App.Controllers
{
    public class AccountController : Controller
    {
        private UserRepo userRepo;
        private PatientRepo patientRepo;
        private DoctorRepo doctorRepo;

        public AccountController(UserRepo userRepo, PatientRepo patientRepo, DoctorRepo doctorRepo)
        {
            this.userRepo = userRepo;
            this.patientRepo = patientRepo;
            this.doctorRepo = doctorRepo;
        }

        [HttpGet]
        public IActionResult Register(string role = "Patient")
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model, string role)
        {
            if (role == "Patient")
            {
                ModelState.Remove("Specialization");
                ModelState.Remove("ConsultationFee");
            }

            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingUser = userRepo.GetByEmail(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already registered");
                    ViewBag.Role = role;
                    return View(model);
                }

                // Create User
                var user = new User
                {
                    Email = model.Email,
                    Password = model.Password,
                    Role = role
                };

                bool userCreated = userRepo.Create(user);

                if (userCreated)
                {
                    // Get the newly created User
                    var newUser = userRepo.GetByEmail(model.Email);
                    int userId = newUser.Id;

                    // Create Patient or Doctor record
                    if (role == "Patient")
                    {
                        var patient = new Patient
                        {
                            UserId = userId,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            DateOfBirth = model.DateOfBirth,
                            Gender = model.Gender
                        };
                        patientRepo.Create(patient);
                    }
                    else if (role == "Doctor")
                    {
                        var doctor = new Doctor
                        {
                            UserId = userId,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Specialization = model.Specialization,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            ConsultationFee = model.ConsultationFee
                        };
                        doctorRepo.Create(doctor);
                    }

                    // Auto login after registration
                    SignInUser(model.Email, role);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Role = role;
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userRepo.GetByEmail(model.Email);

                if (user != null && user.Password == model.Password)
                {
                    SignInUser(user.Email, user.Role, model.RememberMe);

                    if (user.Role == "Doctor")
                        return RedirectToAction("Index", "DoctorDashboard");
                    else
                        return RedirectToAction("Index", "PatientDashboard");
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

            return View(model);
        }

        private void SignInUser(string email, string role, bool rememberMe = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).Wait();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}