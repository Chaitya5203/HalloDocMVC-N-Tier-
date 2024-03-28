using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using HalloDocWebServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net.Mail;
using System.Net;
namespace HalloDocWeb.Controllers
{
    public class RequestController : Controller
    {
        private readonly IPatient_Service _service;
        public RequestController(IPatient_Service service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult patient()
        {
            return View();
        }
        public IActionResult patientlogin()
        {
            return View();
        }
        public IActionResult familyfriend()
        {
            return View();
        }
        public IActionResult business()
        {
            return View();
        }
        public IActionResult concierge()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitRequestByPatient(Userdata info)
        {
            if (!ModelState.IsValid)
            {
                return View("../Request/patient", info);
            }
            _service.insertpatient(info);
            return RedirectToAction(nameof(patientlogin), "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePatientByFamilyFriend(FamilyFriendPatientRequest info)
        {
            if (!ModelState.IsValid)
            {
                return View("../Request/familyfriend", info);
            }
            _service.insertbyfamilyfriend(info);
            return RedirectToAction(nameof(patientlogin), "Home");
        }
        [HttpPost]
        public async Task<IActionResult> SavePatientData(login info)
        {
            if (!ModelState.IsValid)
            {
                return View("../Home/CreateAccount", info);
            }
             _service.addnewuserdata(info);
            return RedirectToAction(nameof(patientlogin), "Home");
        }
        [HttpPost]
        public async Task<IActionResult> SaveResetPassword(login info)
        {
            if (!ModelState.IsValid)
            {
                return View("../Home/ResetPassword", info);
            }
            _service.addresetpassword(info);
            return RedirectToAction(nameof(patientlogin), "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePatientByBusiness(BusinessPatientRequest info)
        {
            if (!ModelState.IsValid)
            {
                return View("../Request/business", info);
            }
            _service.insertbybusiness(info);
            return RedirectToAction(nameof(patientlogin), "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePatientByConcierge(ConciergePatientRequest info)
        {
            if (!ModelState.IsValid)
            {
                return View("../Request/concierge", info);
            }
            _service.insertbyconcierge(info);
            return RedirectToAction(nameof(patientlogin), "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sendforgotlink(forgot info)
        {
            var mail = "tatva.dotnet.binalmalaviya@outlook.com";
            var password = "binal@2002";
            var receiver = info.email;
            var subject = "Reset Password";
            var message = "Reset Your Password: https://localhost:7234/Home/ResetPassword/?Email=" + receiver;
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };
            client.SendMailAsync(new MailMessage(from: mail, to: receiver, subject, message));
            return RedirectToAction(nameof(patientlogin), "Home");
        }
    }
}