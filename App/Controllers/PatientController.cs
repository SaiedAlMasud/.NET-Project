using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs;

namespace App.Controllers
{
    public class PatientController : Controller
    {
        PatientService patientService;
        public PatientController(PatientService patientService)
        {
            this.patientService = patientService;
        }
        public IActionResult Index()
        {
            var data = patientService.Get();
            return View(data);
        }
        public IActionResult Details(int id)
        {
            var data = patientService.Get(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PatientDTO p)
        {
            if (ModelState.IsValid)
            {
                var res = patientService.Create(p);
                if(res == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(p);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = patientService.Get(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(PatientDTO p)
        {
            if (ModelState.IsValid)
            {
                var res = patientService.Update(p);
                if (res == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(p);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = patientService.Get(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(int id, string Decision)
        {
            if (Decision.Equals("Yes"))
            {
                patientService.Delete(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
