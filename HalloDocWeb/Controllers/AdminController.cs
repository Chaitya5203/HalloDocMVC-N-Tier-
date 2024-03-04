using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using HalloDocWebServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HalloDocWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin_Service _service;
        public AdminController(IAdmin_Service service)
        {
            _service = service;
        }
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
            data =_service.dashboardtabledata(id,check);
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
        public IActionResult DeleteFile(int id)
        {
            _service.deleteFile(id);
            
            return RedirectToAction(nameof(Admindashboard));
        }
        public IActionResult ViewUploadAdmin(int id)
        {

            viewuploadmin model = _service.viewUploadAdmin(id);
            return View(model);
        }
        public IActionResult Adminforgot()
        {
            return View();
        }
        
        public IActionResult Sendorder()
        {
            return View();
        }
        public async Task<IActionResult> Modalsofnew(int id)
        {
            var data = _service.opencancelmodel(id);
            TempData["reqid"] = id;
            return PartialView("_Modalsofnew", data);
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
            Request request = _service.getrequestdata(id,reason.Name);
            return RedirectToAction(nameof(Admindashboard));
        }
        [HttpPost]
        public ActionResult blockConfirm(int id, string notes)
        {
            var data = _service.opencancelmodel(id);
            //var data = _context.Requestclients.FirstOrDefault(r => r.Requestid == id);
            _service.insertrequeststatuslogtableofblockcase(id, notes);
            _service.insertblockrequesttable(id,data.Email,data.Phonenumber,notes);
            Request request = _service.getrequestdatatoblockcase(id);
            //_context.Requests.Update(request);
            return RedirectToAction(nameof(Admindashboard));
        }
        [HttpPost]
        public ActionResult AssignConfirm(int id, string notes, string physician, string region)
        {
            _service.insertrequeststatuslogtablebyassign(id, notes);
            Request request = _service.getrequestdatatoassigncase(id);
            return RedirectToAction(nameof(Admindashboard));
        }
        public ActionResult View_Note(int id)
        {
            var note =  _service.getrequestnotes(id);
            //var tranfernote =  _service.getrequeststatuslog(id);
            note.req_id = id;
            return View(note);
        }
        [HttpPost]
        public ActionResult SaveNotes(Notes n, int id)
        {
            var user = _service.getrequestdataofnotes(id);
            _service.getreqnoteofsavenote(id,n,user.Email);
            return RedirectToAction(nameof(Admindashboard));
        }
    }
}