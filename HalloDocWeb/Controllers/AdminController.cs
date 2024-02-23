using HalloDocWebServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDocWeb.Controllers
{
    public class AdminController : Controller
    {
        //private readonly IAdmin_Service _service;
        //public AdminController(IAdmin_Service service)
        //{
        //    _service = service;
        //}
        public IActionResult Admindashboard()
        {

            return View();
        }
         //[HttpPost]
         //public ActionResult New()
         //{
         //   var data = _service.patientinnewstate();
         //    //var data =from t1 in _context.Requests
         //    //          join t2 in _context.Requestclients on t1.Requestid equals t2.Requestid
         //    //          join t3 in _context.Requesttypes on t1.Requesttypeid equals t3.Requesttypeid
         //    //          select new
         //    //          {
         //    //              t1.Requesttypeid,
         //    //              t1.Firstname,
         //    //              t1.Lastname,
         //    //              t2.Intdate,
         //    //              t2.Strmonth,
         //    //              t2.Intyear,
         //    //              t1.Phonenumber,
         //    //              t2.Street,
         //    //              t2.City,
         //    //              t2.Notes,
         //    //              t1.Createddate,
         //    //              t3.Name
         //    //          };
         //   return PartialView("_New",data);
         //}
    }
}
