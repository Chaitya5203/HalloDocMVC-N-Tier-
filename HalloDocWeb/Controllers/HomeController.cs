using HalloDocWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HalloDocWebRepository.ViewModel;
using NuGet.Versioning;
using HalloDocWebServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using HalloDocWebRepository.Data;
using System.Collections;
using System.IO.Compression;

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
        public async Task<IActionResult> document(int id)
        {

            //HttpContext.Session.SetInt32("req_id", id);

            ViewBag.Id = id;

            //HttpContext.Session.SetString("req_id", id.ToString());
            return View( _service.getdownloadfilerequestwise(id));

            //return _context.Requestwisefiles != null ? View(_context.Requestwisefiles.Where(m => m.Requestid == id).ToList()) : Problem("vchgvytfvtv");
        }
        public IActionResult SubmitRequestOnMe()
        {
            return View();
        }
        public IActionResult SubmitRequestSomeone()
        {
            return View();
        }
        // Request on me page When Dashboard Is Open and request is Created 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitRequestOnMe(Userdata info)
        {
            
            if (!ModelState.IsValid)
            {
                return View("../Home/SubmitRequestOnMe", info);
            }
            _service.RequestMe(info, HttpContext.Session.GetString("UsarEmail"));
            return RedirectToAction(nameof(patientdashboard), "Home");
        }
        // Request on someoneelse page When Dashboard Is Open and request is Created 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitRequestSomeone(someoneelse info)
        {
            if (!ModelState.IsValid)
            {
                return View("../Home/SubmitRequestSomeone", info);
            }
            _service.Requestsomeoneelse(info, HttpContext.Session.GetString("UsarEmail"));
            return RedirectToAction(nameof(patientdashboard), "Home");
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
        [HttpPost]
        public IActionResult UploadFile(int id, IFormFile fileToUpload)
        {
            if (fileToUpload != null && fileToUpload.Length > 0)
            {
                _service.addfilerequestwise(id,fileToUpload);
                //_context.Requestwisefiles.Add(reqclient);
                //_context.SaveChanges();

                return RedirectToAction(nameof(patientdashboard), "Home");
            }
            else
            {
                // User did not select a file
                return RedirectToAction(nameof(patientdashboard), "Home");
            }
        }
        
        //download File 
        public async Task<IActionResult> DownloadFile(int id)
        {
            var bytes = _service.DownloadSingleFile(id);
            var file = _service.RequestwisefilesSer(id);
            //var filepath = "C:\\Users\\pce96\\source\\repos\\WebApplication2 - Copy\\WebApplication2\\wwwroot\\uploads\\" + Path.GetFileName(file.Filename);
            //var bytes = System.IO.File.ReadAllBytes(filepath);
            return File(bytes, "application/octet-stream", file.Filename);
        }
        public IActionResult DownloadAll(int id)
        {
            MemoryStream ms =  _service.DownloadAllService(id);
            return File(ms.ToArray(), "application/zip", "download.zip");
        }
    }
}