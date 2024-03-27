using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using HalloDocWebServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HalloDocWebService.Authentication;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2010.Excel;
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
            AdminDashboardDataWithRegionModel model = new();
            model.TotalPages = 1;
            model.CurrentPage = 1;
            model.PreviousPage = 1 > 1;
            model.NextPage = 1 < model.TotalPages;
            model.regions = _service.getRegionList();
            ViewBag.NewCount = _service.getcount(1);
            ViewBag.PendingCount = _service.getcount(2);
            ViewBag.ActiveCount = _service.getcount(3);
            ViewBag.ConcludeCount = _service.getcount(4);
            ViewBag.TocloseCount = _service.getcount(5);
            ViewBag.UnpaidCount = _service.getcount(6);
            return View(model);
        }
        [HttpPost]
        public ActionResult New(int id, int check , string searchValue, int searchRegion,int pagesize=3, int  pagenumber =1 )
        {
            IQueryable<AdminDashboardTableModel> tabledata1;
            AdminDashboardDataWithRegionModel model = new();
            tabledata1 = _service.dashboardtabledata(id, check);
            model.physicians = _service.getPhycision();
            model.regions = _service.getRegionList();
            if (searchValue != null)
            {
                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    tabledata1 = tabledata1.Where(x => x.Name.ToLower().Contains(searchValue.ToLower()));
                }
            }
            if (searchRegion != 0)
            {
                tabledata1 = tabledata1.Where(x => x.RegionID == searchRegion);
            }
            model.tabledata = tabledata1;
            var count = tabledata1.Count();
            if (count > 0)
            {
                tabledata1 = tabledata1.Skip((pagenumber - 1) * 5).Take(5);
                model.tabledata = tabledata1;
                model.TotalPages = (int)Math.Ceiling((double)count / 5);
                model.CurrentPage = pagenumber;
                model.PreviousPage = pagenumber > 1;
                model.NextPage = pagenumber < model.TotalPages;
            }
            if (id == 1)
                return PartialView("_New", model);
            if (id == 2)
                return PartialView("_Pending", model);
            if (id == 3)
                return PartialView("_Active", model);
            if (id == 4)
                return PartialView("_Conclude", model);
            if (id == 5)
                return PartialView("_Toclose", model);
            if (id == 6)
                return PartialView("_Unpaid", model);
            else
                return PartialView("_New", model);
        }
        public async Task<ActionResult> View_Case(int id)
        {
            return View(_service.getviewcasedataofpatient(id));
        }
        public IActionResult Adminlogin()
        {
            return View();
        }
        public IActionResult CreatePhysicianAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePhysicianAccount(PhysicianProfile phy)
        {
            if (ModelState.IsValid)
            {
                _service.addphysiciandata(phy);
                return RedirectToAction(nameof(Admindashboard));
            }
            return RedirectToAction(nameof(Admindashboard));
        }
        public IActionResult EditPhysicianAccount(int id)
        {
            return View(_service.getphysicianprofiledata(id));
        }
        public IActionResult SavePhysicianInfo(PhysicianProfile phy)
        {
            _service.updatephysicianprofile(phy);
            return RedirectToAction("EditPhysicianAccount", new { id = phy.PhysicianId }) ;
        }
        public IActionResult SavePhysicianBillingInfo(PhysicianProfile phy)
        {
            _service.updatephysicianbilling(phy);
            return RedirectToAction("EditPhysicianAccount", new { id = phy.PhysicianId });
        }
        public IActionResult AdminProviderLocation()
        {
            return View();
        }
        public IActionResult AdminMyProfile()
        {
            return View(_service.getMyProfileData(HttpContext.Request.Cookies["UsarEmail"]));
        }
        public IActionResult AdminPartner()
        {
            return View();
        }
        public IActionResult AdminProvider()
        {
            PhysicianProfile model = new();
            model.physician = _service.getPhycision();
            return View(model);
        }
        public IActionResult CreateRole(int check)
        {
            return View(_service.GetMenuData(check));
        }
        public IActionResult EditRole(int id)
        {
            return View(_service.getrolewisedataofrole(id));
        }
        public IActionResult DeleteRole(int id)
        {
            _service.deleterole(id);
            return RedirectToAction(nameof(AdminAccess));
        }
        public IActionResult GenerateRole(string RoleName, string[] selectedRoles, int check)
        {
            _service.generateRole(RoleName, selectedRoles, check, HttpContext.Request.Cookies["UsarEmail"]);
            return RedirectToAction(nameof(AdminAccess));
        }
        public IActionResult AdminAccess()
        {
            return View(_service.getrolewisedata());
        }
        public IActionResult UpdateRole(RoleModel roleModel)
        {
            _service.updateroleof(roleModel);
            return RedirectToAction(nameof(AdminAccess));
        }
        public IActionResult AdminRecord()
        {
            return View();
        }
        public IActionResult ReviewAgreement(string token )
        {
            TokenRegister tokenReg = _service.getTokenRegidterDataByToken(token);
            if(tokenReg == null)
                return Problem("Invalid Request");
            var model = _service.getReviewAgreementData(tokenReg);
            return View(model);
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
                return RedirectToAction(nameof(Admindashboard));
            }
            else
            {
                return RedirectToAction(nameof(Admindashboard));
            }
        }
        public async Task<IActionResult> DownloadFile(int id)
        {
            var bytes = _service.DownloadSingleFilebyadmin(id);
            var file = _service.RequestwisefilesSerbyadmin(id);
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
        public IActionResult SendLink(AdminDashboardDataWithRegionModel info)
        {
            _service.SendLink(info.Email);
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
            return View(_service.getdataofsendorder(id,hprof,hproftype));
        }
        public IActionResult Encounterform(int id)
        {
            ViewBag.id = id;
            Encounterformmodel model = _service.EncounterAdmin(id);
            return View(model);
        }
        public ActionResult SaveEncounterForm(Encounterformmodel info)
        {
            _service.saveEncounterForm(info);
            return RedirectToAction(nameof(Admindashboard));
        }
        public ActionResult UpdateAdminProfile(AdminProfileModel info)
        {
            _service.updateadminform(info);
            return RedirectToAction(nameof(Admindashboard));
        }
        public ActionResult UpdateAddressInformationOfAdmin(AdminProfileModel info)
        {
            _service.updateadminaddress(info);
            return RedirectToAction(nameof(Admindashboard));
        }
        public async Task<IActionResult> Modalsofnew(int id)
        {
            var data = _service.opencancelmodel(id);
            TempData["reqid"] = id;
            return PartialView("_Modalsofnew", data);
        }
        public async Task<IActionResult> Modalofsendagreement(int id)
        {
            var data = _service.sendAgreement(id);
            return PartialView("_Modalofsendagreement", data);
        }
        public async Task<IActionResult> Modalofclear(int id)
        {                                                                               
            var data = _service.opencancelmodel(id);
            TempData["reqid"] = id;
            return PartialView("_Modalofclear", data);
        }
        public async Task<IActionResult> Modaloftransfer(int id, int regionid)
        {
            TempData["reqid"] = id;
            return PartialView("_Modaloftransfer", _service.openassignmodel(id, regionid));
        }
        public async Task<IActionResult> Modalofblock(int id)
        {
            var data = _service.opencancelmodel(id);
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
            _service.insertrequeststatuslogtableofblockcase(id, notes);
            _service.insertblockrequesttable(id, data.Email, data.Phonenumber, notes);
            Request request = _service.getrequestdatatoblockcase(id);
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
        [HttpPost]
        public ActionResult ReviewAgreement(int Requestid, string notes)
        {
            bool isValidToken =  _service.AgreementCancel(Requestid, notes);
            if (isValidToken == false)
                return Problem("Invalid Request");
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
        public ActionResult View_Note(int id)
        {
            var note = _service.getrequestnotes(id);
            note.req_id = id;
            return View(note);
        }
        public ActionResult AgreementAccept(int id)
        {
            _service.AgreementAccepted(id);
            return RedirectToAction(nameof(HomeController.Index),"Home");
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
        public IActionResult ExportAll()
        {
            try
            {
                List<Request> data = GetTableData();
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Data");
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Date Of Birth";
                worksheet.Cell(1, 3).Value = "Requestor";
                worksheet.Cell(1, 4).Value = "Physician Name";
                worksheet.Cell(1, 5).Value = "Date of Service";
                worksheet.Cell(1, 6).Value = "Requested Date";
                worksheet.Cell(1, 7).Value = "Phone Number";
                worksheet.Cell(1, 8).Value = "Address";
                worksheet.Cell(1, 9).Value = "Notes";
                int row = 2;
                foreach (var item in data)
                {
                    var statusClass = "";
                    var dos = "";
                    var notes = "";
                    if (item.Requesttypeid == 1)
                    {
                        statusClass = "patient";
                    }
                    else if (item.Requesttypeid == 4)
                    {
                        statusClass = "business";
                    }
                    else if (item.Requesttypeid == 2)
                    {
                        statusClass = "family";
                    }
                    else
                    {
                        statusClass = "concierge";
                    }
                    foreach (var stat in item.Requeststatuslogs)
                    {
                        if (stat.Status == 2)
                        {
                            dos = stat.Createddate.ToString("MMMM dd,yyyy");
                            notes = stat.Notes ?? "";
                        }
                    }
                    worksheet.Cell(row, 1).Value = item.Requestclients?.FirstOrDefault()?.Firstname + item.Requestclients?.FirstOrDefault()?.Lastname;
                    worksheet.Cell(row, 3).Value = statusClass.Substring(0, 1).ToUpper() + statusClass.Substring(1).ToLower() + item.Firstname + item.Lastname;
                    worksheet.Cell(row, 4).Value = ("Dr." );
                    worksheet.Cell(row, 5).Value = dos;
                    worksheet.Cell(row, 6).Value = item.Createddate.ToString("MMMM dd,yyyy");
                    worksheet.Cell(row, 7).Value = item.Requestclients?.FirstOrDefault()?.Phonenumber + "(Patient)" + (item.Requesttypeid != 4 ? item.Phonenumber + statusClass.Substring(0, 1).ToUpper() + statusClass.Substring(1).ToLower() : "");
                    worksheet.Cell(row, 8).Value = (item.Requestclients?.FirstOrDefault()?.Address == null ? item.Requestclients?.FirstOrDefault()?.Address + item.Requestclients?.FirstOrDefault()?.Street + item.Requestclients?.FirstOrDefault()?.City + item.Requestclients?.FirstOrDefault()?.State + item.Requestclients?.FirstOrDefault()?.Zipcode : item.Requestclients?.FirstOrDefault()?.Street + item.Requestclients?.FirstOrDefault()?.City + item.Requestclients?.FirstOrDefault()?.State + item.Requestclients?.FirstOrDefault()?.Zipcode);
                    worksheet.Cell(row, 9).Value = item.Requestclients?.FirstOrDefault()?.Notes;
                    row++;
                }
                worksheet.Columns().AdjustToContents();
                var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
        public List<Request> GetTableData()
        {
            List<Request> data = new List<Request>();
            data = _service.GetRequestDataInList();
            return data;
        }
        public ActionResult Export(int id, int check, string searchValue, int searchRegion, int pagesize = 3, int pagenumber = 1)
        {
            try
            {
                IQueryable<AdminDashboardTableModel> tabledata1;
                tabledata1 = _service.dashboardtabledata(id, check);
                if (searchValue != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        tabledata1 = tabledata1.Where(x => x.Name.ToLower().Contains(searchValue.ToLower()));
                    }
                }
                if (searchRegion != 0)
                {
                    tabledata1 = tabledata1.Where(x => x.RegionID == searchRegion);
                }
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Data");
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Date Of Birth";
                worksheet.Cell(1, 3).Value = "Requestor";
                worksheet.Cell(1, 4).Value = "Physician Name";
                worksheet.Cell(1, 5).Value = "Date of Service";
                worksheet.Cell(1, 6).Value = "Requested Date";
                worksheet.Cell(1, 7).Value = "Phone Number";
                worksheet.Cell(1, 8).Value = "Address";
                worksheet.Cell(1, 9).Value = "Notes";
                int row = 2;
                foreach (var item in tabledata1)
                {
                    var statusClass = "";
                    var dos = "";
                    var notes = "";
                    if (item.Requesttypeid == 1)
                    {
                        statusClass = "patient";
                    }
                    else if (item.Requesttypeid == 4)
                    {
                        statusClass = "business";
                    }
                    else if (item.Requesttypeid == 2)
                    {
                        statusClass = "family";
                    }
                    else
                    {
                        statusClass = "concierge";
                    }
                    worksheet.Cell(row, 1).Value = item.Name;
                    worksheet.Cell(row, 2).Value = item.DOB;
                    worksheet.Cell(row, 3).Value = item.Requestor;
                    worksheet.Cell(row, 4).Value = item.physician;
                    worksheet.Cell(row, 5).Value = item.Dateofservice;
                    worksheet.Cell(row, 6).Value = item.Requesteddate;
                    worksheet.Cell(row, 7).Value = item.Phonenumber;
                    worksheet.Cell(row, 8).Value = item.Address;
                    worksheet.Cell(row, 9).Value = item.Notes;
                    row++;
                }
                worksheet.Columns().AdjustToContents();
                var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Patientrequestadmin(Userdata1 info)
        {
            if (ModelState.IsValid)
            {
                _service.saveadminrequest(info, HttpContext.Request.Cookies["UsarEnail"]);
                return RedirectToAction(nameof(Admindashboard));
            }
            return View();
            }
        }
    }
