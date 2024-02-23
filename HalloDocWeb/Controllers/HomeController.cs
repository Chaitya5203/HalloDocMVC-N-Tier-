using HalloDocWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HalloDocWebRepository.ViewModel;
using NuGet.Versioning;
using HalloDocWebServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HalloDocWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPatient_Service _service;

        public HomeController(IPatient_Service service)
        {
              _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult submit_request_screen()
        {
            return View();
        }
        public IActionResult patientlogin()
        {
            return View();
        }

        [Route("/Home/patient/{email}")]
        [Route("/Home/business/{email}")]
        [Route("/Home/familyfriend/{email}")]
        [Route("/Home/concierge/{email}")]

        [HttpGet]
        public IActionResult CheckEmailExists(string email)
        {
            var chk = _service.CheckEmail(email);
            return Json(new { exists = chk });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> patientlogin(login loginobj)
        {
            if (!ModelState.IsValid)
            {
                return View("../Home/patientlogin", loginobj);
            }
            bool isReg = _service.ValidateUser(loginobj);
            if (isReg)
            {
                var user = _service.getUser(loginobj.Email);

                HttpContext.Session.SetString("Usarname", user.Usarname);
                HttpContext.Session.SetString("UsarEmail", loginobj.Email);
                return RedirectToAction(nameof(patientdashboard), "Home");
            }
            else
            {
                return RedirectToAction(nameof(patientlogin), "Home");
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Updateprofile(Userdata info)
        {
            _service.updateProfile(info, HttpContext.Session.GetString("UsarEmail"));
            return RedirectToAction(nameof(patientdashboard), "Home");
        }
        public async  Task<IActionResult> patientdashboard()
        {
            var dictionary = _service.getDictionary(HttpContext.Session.GetString("Usarname"));
            ViewBag.RequestIdCounts = dictionary;
            var pro = _service.Dashboarddata(HttpContext.Session.GetString("UsarEmail"), HttpContext.Session.GetString("Usarname"));
            return View(pro);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
