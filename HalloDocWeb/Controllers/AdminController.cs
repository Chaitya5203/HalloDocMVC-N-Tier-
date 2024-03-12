using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using HalloDocWebServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Net.Mail;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HalloDocWebService.Authentication;
using System.Drawing;

namespace HalloDocWeb.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly IAdmin_Service _service;
        public AdminController(IAdmin_Service service)
        {
            _service = service;
        }
        [CustomAuthorize("Admin")]
        public IActionResult Admindashboard(int id)
        {
            ViewBag.NewCount = _service.getcount(1);
            ViewBag.PendingCount = _service.getcount(2);
            ViewBag.ActiveCount = _service.getcount(3);
            ViewBag.ConcludeCount = _service.getcount(4);
            ViewBag.TocloseCount = _service.getcount(5);
            ViewBag.UnpaidCount = _service.getcount(6);
            //ViewBag.NewCount = _context.Requests.Where(r => r.Status == 1).Count();
            //ViewBag.PendingCount = _context.Requests.Where(r => r.Status == 2).Count();
            //ViewBag.ActiveCount = _context.Requests.Where(r => r.Status == 3).Count();
            //ViewBag.ConcludeCount = _context.Requests.Where(r => r.Status == 4).Count();
            //ViewBag.TocloseCount = _context.Requests.Where(r => r.Status == 5).Count();
            //ViewBag.UnpaidCount = _context.Requests.Where(r => r.Status == 6).Count();
            return View();
        }
        [HttpPost]
        public ActionResult New(int id, int check)
        {
            IQueryable data;
            data = _service.dashboardtabledata(id, check);
            if (id == 1)
                return PartialView("_New", data);
            if (id == 2)
                return PartialView("_Pending", data);
            if (id == 3)
                return PartialView("_Active", data);
            if (id == 4)
                return PartialView("_Conclude", data);
            if (id == 5)
                return PartialView("_Toclose", data);
            if (id == 6)
                return PartialView("_Unpaid", data);
            else
                return PartialView("_New", data);
        }
        public async Task<ActionResult> View_Case(int id)
        {
            return View(_service.getviewcasedataofpatient(id));
        }
        public IActionResult Adminlogin()
        {
            return View();
        }
       public IActionResult ReviewAgreement()
        {
            return View();
        }
        public IActionResult DeleteFile(int id)
        {
            _service.deleteFile(id);
            return RedirectToAction(nameof(Admindashboard));
        }
        public IActionResult DeleteAll(int id, [FromBody] string[] reqids)
        {
            _service.deleteallfilesbyadmin(reqids, id);
            return RedirectToAction(nameof(Admindashboard));
        }
        public IActionResult UploadFilebyadmin(int id, IFormFile fileToUpload)
        {
            if (fileToUpload != null && fileToUpload.Length > 0)
            {
                _service.addrequestwisefilebyadmin(id, fileToUpload);
                //_service.addfilerequestwise(id, fileToUpload);
                //_context.Requestwisefiles.Add(reqclient);
                //_context.SaveChanges();
                return RedirectToAction(nameof(Admindashboard));
            }
            else
            {
                // User did not select a file
                return RedirectToAction(nameof(Admindashboard));
            }
        }
        //download File 
        public async Task<IActionResult> DownloadFile(int id)
        {
            var bytes = _service.DownloadSingleFilebyadmin(id);
            var file = _service.RequestwisefilesSerbyadmin(id);
            //var filepath = "C:\\Users\\pce96\\source\\repos\\WebApplication2 - Copy\\WebApplication2\\wwwroot\\uploads\\" + Path.GetFileName(file.Filename);
            //var bytes = System.IO.File.ReadAllBytes(filepath);
            return File(bytes, "application/octet-stream", file.Filename);
        }
        public IActionResult DownloadAll([FromBody] string[] filenames)
        {
            MemoryStream ms = _service.DownloadAllServicebyadmin(filenames);
            return File(ms.ToArray(), "application/zip", "download.zip");
        }
        public IActionResult ViewUploadAdmin(int id)
        {
            ViewBag.id = id;
            viewuploadmin model = _service.viewUploadAdmin(id);
            return View(model);
        }
        public IActionResult Adminforgot()
        {
            return View();
        }
        public IActionResult Close_case(int id)
        {
            ViewBag.id = id;
            viewuploadmin model = _service.viewUploadAdmin(id);
            return View(model);
        }
        public IActionResult Closecase(int id, viewuploadmin n)
        {
            _service.closecasetounpaid(id, n);
            return RedirectToAction(nameof(Admindashboard));
        }

        public IActionResult Patientrequestadmin()
        {
            return View();
        }
        public ActionResult Ordersend(sendorder sendorder)
        {
            _service.insertordertable(sendorder);
            return RedirectToAction(nameof(Admindashboard));
        }
        public ActionResult Sendorder(int id ,int hprof=0,int hproftype= 0)
        {
            //var data = _service.getdataofsendorder(id);
            return View(_service.getdataofsendorder(id,hprof,hproftype));
        }
        public IActionResult Encounterform(int id)
        {
            ViewBag.id = id;
            Encounterformmodel model = _service.EncounterAdmin(id);
            return View(model);
        }
        public  ActionResult SaveEncounterForm(Encounterformmodel info)
        {
            _service.saveEncounterForm(info);
            return RedirectToAction(nameof(Admindashboard));
        }
        public async Task<IActionResult> Modalsofnew(int id)
        {
            var data = _service.opencancelmodel(id);
            TempData["reqid"] = id;
            return PartialView("_Modalsofnew", data);
            //var data = _context.Requestclients.FirstOrDefault(r => r.Requestid == id);
            //return PartialView("_Modalsofnew", data);
        }
        public async Task<IActionResult> Modalofsendagreement(int id)
        {
            var data = _service.sendAgreement(id);
            return PartialView("_Modalofsendagreement", data);
            //var data = _context.Requestclients.FirstOrDefault(r => r.Requestid == id);
            //return PartialView("_Modalsofnew", data);
        }
        public async Task<IActionResult> Modalofclear(int id)
        {                                                                               
            var data = _service.opencancelmodel(id);
            TempData["reqid"] = id;
            return PartialView("_Modalofclear", data);
            //var data = _context.Requestclients.FirstOrDefault(r => r.Requestid == id);
            //return PartialView("_Modalsofnew", data);
        }
        public async Task<IActionResult> Modaloftransfer(int id, int regionid)
        {
            TempData["reqid"] = id;
            return PartialView("_Modaloftransfer", _service.openassignmodel(id, regionid));
            //var data = _context.Requestclients.FirstOrDefault(r => r.Requestid == id);
            //return PartialView("_Modalsofnew", data);
        }
        public async Task<IActionResult> Modalofblock(int id)
        {
            var data = _service.opencancelmodel(id);
            //var data = _context.Requestclients.FirstOrDefault(r => r.Requestid == id);
            TempData["reqid"] = id;
            return PartialView("_Modalofblock", data);
        }
        public async Task<IActionResult> Modalofassign(int id, int regionid)
        {
            TempData["reqid"] = id;
            return PartialView("_Modalofassign", _service.openassignmodel(id, regionid));
        }
        [HttpPost]
        public ActionResult CancelConfirm(int id, int reasonid, string notes)
        {
            _service.insertrequeststatuslogtable(id, notes, reasonid);
            Casetag reason = _service.getcasetag(reasonid);
            Request request = _service.getrequestdata(id, reason.Name);
            return RedirectToAction(nameof(Admindashboard));
        }
        [HttpPost]
        public ActionResult Transferconfirm(int id, string notes, int physician, int regions)
        {
            _service.insertrequeststatuslogtablebytransfer(id,notes,physician);
            Request request = _service.getrequestdatatotransfercase(id, physician);
            return RedirectToAction(nameof(Admindashboard));
        }
        [HttpPost]
        public ActionResult blockConfirm(int id, string notes)
        {
            var data = _service.opencancelmodel(id);
            //var data = _context.Requestclients.FirstOrDefault(r => r.Requestid == id);
            _service.insertrequeststatuslogtableofblockcase(id, notes);
            _service.insertblockrequesttable(id, data.Email, data.Phonenumber, notes);
            Request request = _service.getrequestdatatoblockcase(id);
            //_context.Requests.Update(request);
            return RedirectToAction(nameof(Admindashboard));
        }
        [HttpPost]
        public ActionResult AssignConfirm(int id, string notes, int physician, string regions)
        {
            _service.insertrequeststatuslogtablebyassign(id, notes,physician);
            Request request = _service.getrequestdatatoassigncase(id,physician);
            return RedirectToAction(nameof(Admindashboard));
        }
        [HttpPost]
        public ActionResult Clearconfirm(int id)
        {
            _service.insertrequeststatuslogtableclearcase(id);
            Request request = _service.setclearcase(id);
            return RedirectToAction(nameof(Admindashboard));
        }
        [HttpPost]
        public ActionResult Sendagreementconfirm(int id)
        {
            _service.SendAgreementEmail(id);
            return RedirectToAction(nameof(Admindashboard));
        }
        public ActionResult View_Note(int id)
        {
            var note = _service.getrequestnotes(id);
            //var tranfernote =  _service.getrequeststatuslog(id);
            note.req_id = id;
            return View(note);
        }
        [HttpPost]
        public ActionResult SendMail(int id, string[] filenames)
        {
            _service.SendEmail(id, filenames);
            return RedirectToAction(nameof(Admindashboard));
        }
        [HttpPost]
        public ActionResult SaveNotes(Notes n, int id)
        {
            var user = _service.getrequestdataofnotes(id);
            _service.getreqnoteofsavenote(id, n, user.Email);
            return RedirectToAction("");
        }
    }
}  