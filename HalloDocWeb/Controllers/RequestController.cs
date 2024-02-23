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
        //Create Patient By It Self 
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Routing Of The Data and Check if email exist if not then add column password and confirm password 
        public async Task<IActionResult> SubmitRequestByPatient(Userdata info)
        {
            //var temp = 0;
            //var userobj = await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Usarname == info.first_name);
            if (!ModelState.IsValid)
            {
                return View("../Request/patient", info);
            }
            _service.insertpatient(info);
            return RedirectToAction(nameof(patientlogin), "Home");
            //return View();
        }
        /// Family Friend Data store on Family Friend Page 
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
    }
}