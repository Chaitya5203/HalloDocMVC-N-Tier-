﻿using HalloDocWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HalloDocWebRepository.ViewModel;
using NuGet.Versioning;
using HalloDocWebServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using HalloDocWebRepository.Data;
using System.Collections;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using HalloDocWebService.Authentication;
namespace HalloDocWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPatient_Service _service;
        private readonly IJwt_Service _jwt_Service;
        public HomeController(IPatient_Service service, IJwt_Service jwtservice)
        {
            _service = service;
            _jwt_Service = jwtservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateAccount(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }
        public IActionResult submit_request_screen()
        {
            return View();
        }
        [CustomAuthorize("Login")]
        public IActionResult patientlogin()
        {
            return View();
        }
        public async Task<IActionResult> document(int id)
        {
            ViewBag.Id = id;
            return View(_service.getdownloadfilerequestwise(id));
        }
        public IActionResult patientforgot()
        {
            return View();
        }
        public IActionResult SubmitRequestOnMe()
        {
            return View();
        }
        public IActionResult SubmitRequestSomeone()
        {
            return View();
        }
        public IActionResult ResetPassword(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitRequestSomeone(someoneelse info)
        {
             if (!ModelState.IsValid)
            {
                return View("../Home/SubmitRequestSomeone", info);
            }
            _service.Requestsomeoneelse(info, HttpContext.Request.Cookies["UsarEmail"]);
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
                var jwttoken = _jwt_Service.GetJWTToken(user);
                Response.Cookies.Append("jwt", jwttoken, new CookieOptions { MaxAge = TimeSpan.FromDays(1) });
                Response.Cookies.Append("UsarEmail", loginobj.Email, new CookieOptions { MaxAge = TimeSpan.FromDays(1) });
                Response.Cookies.Append("Usarname", user.Usarname, new CookieOptions { MaxAge = TimeSpan.FromDays(1) });
                if (user.Role == "3")
                {
                    return RedirectToAction(nameof(AdminController.Admindashboard), "Admin");
                }
                else if (user.Role == "1")
                {
                    TempData["toastMsg"] = "Login Successfully!!!!";
                    return RedirectToAction(nameof(patientdashboard), "Home");
                }
                else
                {
                    return RedirectToAction(nameof(patientlogin), "Home");
                }
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
            _service.updateProfile(info, HttpContext.Request.Cookies["UsarEmail"]);
            return RedirectToAction(nameof(patientdashboard), "Home");
        }
        [CustomAuthorize("Patient")]
        public async Task<IActionResult> patientdashboard()
        {
            var req = HttpContext.Request;
            var username = req.Cookies["Usarname"];
            var useremail = req.Cookies["UsarEmail"];
            var dictionary = _service.getDictionary(username);
            ViewBag.RequestIdCounts = dictionary;
            var pro = _service.Dashboarddata(useremail, username);
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
                _service.addfilerequestwise(id, fileToUpload);
                return RedirectToAction(nameof(document), new {id = id});
            }
            else
            {
                return RedirectToAction(nameof(document), new { id = id });
            }
        }
        public async Task<IActionResult> DownloadFile(int id)
        {
            var bytes = _service.DownloadSingleFile(id);
            var file = _service.RequestwisefilesSer(id);
            return File(bytes, "application/octet-stream", file.Filename);
        }
        public IActionResult Patientprofile()
        {
            var pro = _service.Dashboarddata(HttpContext.Request.Cookies["UsarEmail"], HttpContext.Request.Cookies["Usarname"]);
            return View(pro);
        }
        public IActionResult DownloadAll(int id)
        {
            MemoryStream ms = _service.DownloadAllService(id);
            return File(ms.ToArray(), "application/zip", "download.zip");
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Usarname");
            Response.Cookies.Delete("UsarEmail");
            Response.Cookies.Delete("jwt");
            return View(nameof(Index));
        }
    }
}