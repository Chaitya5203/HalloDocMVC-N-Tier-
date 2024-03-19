using HalloDocWebRepository.Data;
using HalloDocWebRepository.Interfaces;
using HalloDocWebRepository.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;

using System.Collections;

using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Spreadsheet;

namespace HalloDocWebServices.Interfaces
{
    public class Admin_Service : IAdmin_Service
    {
        private readonly IAdmin_Repository _repository;
        public Admin_Service(IAdmin_Repository repository)
        {
            _repository = repository;
        }
        public void addrequestwisefilebyadmin(int id, IFormFile fileToUpload)
        {
            var uploads = Path.Combine("wwwroot", "uploads");
            var FileNameOnServer = Path.Combine(uploads, fileToUpload.FileName);
            using var stream = System.IO.File.Create(FileNameOnServer);
            fileToUpload.CopyTo(stream);
            Requestwisefile reqclient = new Requestwisefile
            {
                Requestid = id,
                Filename = fileToUpload.FileName,
                Createddate = DateTime.Now,
            };
            _repository.addrequestwisefiletablebyadmin(reqclient);
        }

        public void AgreementAccepted(int id)
        {
            Request req = _repository.getdataofrequest(id);
            _repository.updatetokenregister(id);
            TokenRegister tokenRegister = new TokenRegister();
            tokenRegister.IsDeleted = new BitArray(1, true);
            tokenRegister.IsVerified = new BitArray(1, true);
            _repository.updateandsavetokenregister(tokenRegister);

            if (req != null)
            {
                Requeststatuslog log = new();
                log.Requestid = req.Requestid;
                log.Createddate = DateTime.Now;
                log.Notes = "Agreement Aceepted!!!";
                log.Status = req.Status;
                _repository.setrequeststatuslogdata(log);
                req.Status = 4;
                _repository.setrequestdata(req);
            }
        }

        public bool AgreementCancel(int id, string notes)
        {
            Request req = _repository.getdataofrequest(id);
            
            TokenRegister tokenRegister = _repository.updatetokenregister(id);
            if(tokenRegister != null)
            {
                tokenRegister.IsDeleted = new BitArray(1, true);
                tokenRegister.IsVerified = new BitArray(1, true);
                _repository.updateandsavetokenregister(tokenRegister);
            
            
                if (req != null)
                {
                    Requeststatuslog log = new();
                    log.Requestid = req.Requestid;
                    log.Createddate = DateTime.Now;
                    log.Notes = notes;
                    log.Status = req.Status;
                    _repository.setrequeststatuslogdata(log);
                    req.Status = 7;
                    _repository.setrequestdata(req);
                }
                return true;
            }
            return false;
        }

        public void closecasetounpaid(int id , viewuploadmin n)
        {
            var request = _repository.getdataofrequest(id);
            var reqclient = _repository.getdataofviewcase(id);
            if (n.email != null)
            {
                request.Email = n.email;
                request.Phonenumber = n.phone;
                reqclient.Email = n.email;
                reqclient.Phonenumber = n.phone;
                _repository.setrequestclientdata(reqclient);
            }
            request.Status = 9;
            _repository.setrequestdata(request);
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Status = 9,
                Createddate = DateTime.Now,
            };
            _repository.setrequeststatuslogdata(statuslog);
        }
        public IQueryable<AdminDashboardTableModel> dashboardtabledata(int id, int check)
        {
            IQueryable<AdminDashboardTableModel> data;
            if (check == 0)
            {
               data = _repository.getdataofdashboard(id);
            }
            else
            {
               data = _repository.getdataofdashboardcheckvise(id,check);
            }
            return data;
        }
        public void deleteallfilesbyadmin(string[] reqids,int id)
        {
            List<Requestwisefile> file = new();
            foreach (var fl in reqids)
            {
                file.Add(_repository.getRequestWiseFileListByFileName(fl,id));  
            }
            foreach(var item in file.ToList())
            {
                item.Isdeleted = new BitArray(1, true);
                _repository.updateRequestWiseFileTable(item);
            }
        }
        public void deleteFile(int id)
        {
            var file = _repository.getRequestWiseFile(id);
            file.Isdeleted = new BitArray(1, true);
            _repository.updateRequestWiseFileTable(file);
        }
        public MemoryStream DownloadAllServicebyadmin(string[] filenames)
        {
            string repositoryPath = "D:\\ProjectMvc\\HalloDocWeb\\HalloDocWeb\\wwwroot\\uploads\\";
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipArchive zipArchive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (string filename in filenames)
                    {
                        string filePath = Path.Combine(repositoryPath, filename);
                        if (System.IO.File.Exists(filePath))
                        {
                            zipArchive.CreateEntryFromFile(filePath, filename);
                        }
                        else
                        {
                        }
                    }
                }
                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }
        }
        public byte[] DownloadSingleFilebyadmin(int id)
        {
            var file = _repository.RequestwisefilesRepobyadmin(id);
            var filepath = "D:\\ProjectMvc\\HalloDocWeb\\HalloDocWeb\\wwwroot\\uploads\\" + Path.GetFileName(file.Filename);
            var bytes = System.IO.File.ReadAllBytes(filepath);
            return bytes;
        }
        public Encounterformmodel EncounterAdmin(int id)
        {
            var patientData = _repository.getdataofviewcase(id);
            DateOnly date = DateOnly.Parse(DateTime.Parse(patientData.Intdate + patientData.Strmonth + patientData.Intyear).ToString("yyyy-MM-dd"));
            Encounterformmodel model = new();
            var info = _repository.getEncounterTable(id);  
            model.patientData = patientData;
            model.confirmationDetail = _repository.getdataofrequest(id);
            model.FileList = _repository.getRequestWiseFileList(id);
            model.DOB = date;
            model.Requestid = info.Requestid;
            model.Abd = info.Abd;
            model.Skin = info.Skin;
            model.Hr = info.Hr;
            model.O2 = info.O2;
            model.Rr = info.Rr;
            model.Cv = info.Cv;
            model.BpS = info.BpS;
            model.BpD = info.BpD;
            model.Temp = info.Temp;
            model.Allergies = info.Allergies;
            model.Chest = info.Chest;
            model.Date = info.Date;
            model.Diagnosis = info.Diagnosis;
            model.Extr = info.Extr;
            model.Heent = info.Heent;
            model.FollowUp = info.FollowUp;
            model.HistoryIllness = info.HistoryIllness;
            model.MedicalHistory = info.MedicalHistory;
            model.Medications = info.Medications;
            model.Procedures = info.Procedures;
            model.MedicationDispensed = info.MedicationDispensed;
            model.Neuro = info.Neuro;
            model.Pain = info.Pain;
            model.Other = info.Other;
            return model;
        }
        public Casetag getcasetag(int reasonid)
        {
            var reason = _repository.getcasetagdata(reasonid);
            return reason;
        }
        public int getcount(int id)
        {
            return _repository.getcountofeachstate(id);
        }
        public sendorder getdataofsendorder(int id,int hprof,int hporftype)
        {
            sendorder sendorder = new();
            if(hprof == 0)
            {
                Healthprofessional pr = new Healthprofessional
                {
                    Faxnumber = string.Empty,
                    Businesscontact = string.Empty,
                    Email = string.Empty
                };
                sendorder.businessDetail = pr;
            }
            else
                sendorder.businessDetail = _repository.getBusinessDetailById(hprof);
            if (hporftype == 0 && hprof == 0)
            {
                var hprofessional = _repository.gethealthprofessionaltypedata();
                sendorder.healthprofessional = _repository.gethealthprofessionaldata();
                sendorder.healthprofessionaltype = hprofessional;                
            }
            else if(hporftype != 0)
            {       
                sendorder.healthprofessionaltype = _repository.gethealthprofessionaltypedata();
                sendorder.healthprofessional = _repository.gethealthprofessionaldatabyid(hporftype);
            }else if(hporftype == 0 && hprof != 0)
            {
                
                sendorder.healthprofessionaltype = _repository.gethealthprofessionaltypedata();
                sendorder.healthprofessional = _repository.gethealthprofessionaldata();
            }
            sendorder.id = id;
            return sendorder;
        }
        public AdminProfileModel getMyProfileData(string? v)
        {
            var adminUser = _repository.getAspnetuserByEmail(v);
            AdminProfileModel model = new();
            model.adminuser = adminUser;
            var admin = _repository.getAdminByAspnetId(adminUser.Id);
            model.admin = admin;
            model.adminregion = _repository.getadminregionname(admin.Adminid);

            model.region = _repository.getregionById(model.admin.Regionid);
            model.regions = _repository.getregion();
            return model;
        }

        public List<Physician> getPhycision()
        {
            return _repository.getphysician();
        }

        public List<Region> getRegionList()
        {
            return _repository.getregion();
        }

        public void getreqnoteofsavenote(int id,Notes n , string email)
        {
            var reqnotes = _repository.getrequestnotebyid(id);
            if (reqnotes != null)
            {
                reqnotes.Adminnotes = n.AdminNotes;
                reqnotes.Physiciannotes = reqnotes.Physiciannotes;
                reqnotes.Createdby = email;
                reqnotes.Modifiedby = email;
                reqnotes.Modifieddate = DateTime.Now;
                _repository.Addreqnotetable(reqnotes);  
            }
            else
            {
                Requestnote addreq = new Requestnote
                {
                    Requestid = id,
                    Adminnotes = n.AdminNotes,
                    Createdby = email,
                    Createddate = DateTime.Now,
                };
                _repository.addreqnotetablewithnewnotw(addreq);   
            }   
        }


        public Request getrequestdata(int id, string name)
        {
            var request= _repository.getdataofrequest(id);
            request.Status = 5;
            request.Casetag =name;
            _repository.setrequestdata(request);
            return request;
        }
        public List<Request> GetRequestDataInList()
        {
            return _repository.getINlist();
        }
        public Request getrequestdataofnotes(int id)
        {
            var request = _repository.getdataofrequest(id);
            return request;
        }
        public Request getrequestdatatoassigncase(int id,int physician)
        {
            var request = _repository.getdataofrequest(id);
            request.Physicianid = physician;
            request.Status = 2;
            _repository.setrequestdata(request);
            return request;
        }
        public Request getrequestdatatoblockcase(int id)
        {
            var request = _repository.getdataofrequest(id);

            //Request request = _context.Requests.FirstOrDefault(r => r.Requestid == id);
            request.Status = 10;
            _repository.setrequestdata(request);
            return request;
        }
        public Request getrequestdatatotransfercase(int id, int physician)
        {
            var request = _repository.getdataofrequest(id);  
            request.Physicianid = physician;
            _repository.setrequestdata(request);
            return request;
        }
        public Notes getrequestnotes(int id)
        {
            var note = _repository.getrequestnotebyid(id);
            List<Requeststatuslog> reqstlog = _repository.getRequestStatusLoglist(id);
            List<string> item = new();
            Notes notes = new();
            if (note != null)
            {
                if (note.Physiciannotes == null)
                    notes.phyNotes = "---";
               else
                    notes.phyNotes = note.Physiciannotes;
                if (note.Adminnotes == null)
                    notes.AdminNotes = "---";
                else
                    notes.AdminNotes = note.Adminnotes;
            }
            else
            {
                notes.phyNotes = notes.AdminNotes = "---";
            }
            foreach (var a in reqstlog)
            {
                if (a.Transtoadmin != null)
                {
                    var phy = _repository.getPhysicianById(a.Physicianid);
                    string str = "Dr." + phy.Firstname + ' ' + phy.Lastname + " has transmitted case to Admin On " + a.Createddate.ToString("dd/MM/yyyy") + " at " + a.Createddate.ToString("HH: mm:ss: tt") + ": " + a.Notes;
                    item.Add(str);
                }
                else if (a.Status == 1 && a.Physicianid != null)
                {
                    var phy = _repository.getPhysicianById(a.Physicianid);
                    string str = "Admin Assigned Case To Dr." + phy.Firstname + ' ' + phy.Lastname + " On " + a.Createddate.ToString("dd/MM/yyyy") + " at " + a.Createddate.ToString("HH: mm:ss: tt") + ": " + a.Notes;
                    item.Add(str);

                }
                else if (a.Transtophysicianid != null)
                {
                    var phy1 = _repository.getPhysicianById(a.Transtophysicianid);
                    string str = "Case transfer To Dr." + phy1.Firstname + ' ' + phy1.Lastname + " On " + a.Createddate.ToString("dd / MM / yyyy") + " at " + a.Createddate.ToString("HH: mm: ss: tt") + ": " + a.Notes;
                    item.Add(str);
                }

            }
            notes.Transfernote = item;
            notes.req_id = id;
            return notes;
        }

        public Requestclient getReviewAgreementData(TokenRegister token)
        {
            
            
            var client = _repository.getRequestClientByEmail(token.Email);
            return client;

        }

        public TokenRegister getTokenRegidterDataByToken(string token)
        {
            TokenRegister tokenreg = _repository.getTokenRegisterByToken(token);
            return tokenreg;
        }

        public requestclientvisedata getviewcasedataofpatient(int id)
        {
            var data = _repository.getdataofviewcase(id);
            var region = _repository.getdataofregionvise(data.Regionid);
            DateOnly date = DateOnly.Parse(DateTime.Parse(data.Intyear + data.Strmonth + data.Intdate).ToString("yyyy-MM-dd"));
            //var data = await _context.Requestclients.FirstOrDefaultAsync(m => m.Requestid == id);
            //var region = await _context.Regions.FirstOrDefaultAsync(m => m.Regionid == data.Regionid);
            requestclientvisedata model = new();
            model.FName = data.Firstname;
            model.LName = data.Lastname;
            model.DOB = date;
            model.Notes = data.Notes;
            model.Phonenumber = data.Phonenumber;
            model.Email = data.Email;
            model.RegionName = region.Name;
            model.Address = data.Street + " " + data.City + " " + data.State + " " + data.Zipcode;
            return model;
        }
        public void insertblockrequesttable(int id, string email, string phonenumber, string notes)
        {
            Blockrequest blockrequest = new Blockrequest
            {
                Requestid = id.ToString(),
                Email = email,
                Createddate = DateTime.Now,
                Reason = notes,
                Phonenumber = phonenumber,
            };
            _repository.setblockrequestdata(blockrequest);
        }

        public void insertordertable(sendorder sendorder)
        {
            Orderdetail orderdetail1 = new Orderdetail
            {
                Requestid = sendorder.id,
                Vendorid =sendorder.SelectedVendorId,
                Prescription = sendorder.Detail,
                Faxnumber = sendorder.Fax,
                Email =sendorder.Email,
                Businesscontact = sendorder.Businesscontact,
                Createddate= DateTime.Now,

            };
            _repository.storeordertable(orderdetail1);
        }

        public void insertrequeststatuslogtable(int id, string notes, int reasonid)
        {
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = 3,
                Createddate = DateTime.Now,
            };
            _repository.setrequeststatuslogdata(statuslog);
            //_context.Requeststatuslogs.Add(statuslog);
            //_context.Requests.Update(request);
            //_context.SaveChanges();
        }
        public void insertrequeststatuslogtablebyassign(int id, string notes, int physician)
        {
            var req = _repository.getdataofrequest(id);
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = req.Status,
                Createddate = DateTime.Now,
                Physicianid = physician,
                Adminid=1, //admin change 
            };
            _repository.setrequeststatuslogdata(statuslog);
        }
        public void insertrequeststatuslogtablebytransfer(int id, string notes,int physician)
        {
            var req = _repository.getdataofrequest(id);
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = 2,
                Createddate = DateTime.Now,
                Physicianid=req.Physicianid,
                Transtophysicianid=physician,
                Adminid = 1, //admin change 
            };
            _repository.setrequeststatuslogdata(statuslog);
        }
        public void insertrequeststatuslogtableclearcase(int id)
        {
            var req = _repository.getdataofrequest(id);
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Status = req.Status,
                Createddate = DateTime.Now,
                Adminid = 1, //admin change 
            };
            _repository.setrequeststatuslogdata(statuslog) ;
        }
        public void insertrequeststatuslogtableofblockcase(int id, string notes)
        {
            var req = _repository.getdataofrequest(id);
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = req.Status,
                Createddate = DateTime.Now,
                Adminid = 1, //admin change 
            };
            _repository.setrequeststatuslogdata(statuslog);
        }
        public CaseModels openassignmodel(int id, int regionid)
        {
            CaseModels model = new();
            if (regionid == 0)
            {
                var physician = _repository.getphysician();
                //var physician = _context.Physicians.ToList();
                model.physics = physician;
            }
            else
            {
                var physician = _repository.getphysicianbyregion(id, regionid);
                //var physician = _context.Physicians.Where(m => m.Regionid == regionid).ToList();
                model.physics = physician;
            }
            var region = _repository.getregion();
            //var region = _context.Regions.ToList();
            model.regions = region;
            return model;
        }
        public Requestclient opencancelmodel(int id)
        {
            return _repository.getdataofviewcase(id);
        }
        public Requestwisefile RequestwisefilesSerbyadmin(int id)
        {
            return _repository.RequestwisefilesRepobyadmin(id);
        }
        public void saveEncounterForm(Encounterformmodel info)
        {
            var model = _repository.getEncounterTable(info.Requestid);
           
            model.Requestid = info.Requestid;
            model.Abd=info.Abd;
            model.Skin=info.Skin;
            model.Hr=info.Hr;
            model.O2=info.O2;
            model.Rr=info.Rr;
            model.Cv=info.Cv;
            model.BpS=info.BpS;
            model.BpD=info.BpD;
            model.Temp=info.Temp;
            model.Allergies =info.Allergies;
            model.Chest=info.Chest;
            model.Date=info.Date;
            model.Diagnosis=info.Diagnosis;
            model.Extr=info.Extr;
            model.Heent=info.Heent;
            model.FollowUp=info.FollowUp;
            model.HistoryIllness=info.HistoryIllness;
            model.MedicalHistory=info.MedicalHistory;
            model.Medications=info.Medications;
            model.Procedures=info.Procedures;
            model.MedicationDispensed=info.MedicationDispensed;
            model.TreatmentPlan=info.TreatmentPlan;
            model.Neuro=info.Neuro;
            model.Pain=info.Pain;
            model.Other=info.Other;
            _repository.updateEncounterForm(model);
        }
        public sendorder sendAgreement(int id)
        {
            var req = _repository.getdataofrequest(id);
            sendorder model = new sendorder
            {
                requestclient = _repository.getdataofviewcase(id),
                RequestType = req.Requesttypeid,
                id = id,
            };
            return model;
        }
        public void SendAgreementEmail(int id)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var token = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            Request request = _repository.getdataofrequest(id);
            var receiver = "binalmalaviya2002@gmail.com";
            var subject = "Documents of Request " + request.Confirmationnumber?.ToUpper();
            var message = "https://localhost:44327/Admin/ReviewAgreement?token="+token;
            var mailMessage = new MailMessage(from: "chaityamehta522003@gmail.com", to: receiver, subject, message);
            var mail = "chaityamehta522003@gmail.com";
            var password = "iwbc edlf rgpt oucs";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };
            client.SendMailAsync(mailMessage);
            TokenRegister tokenRegister = new TokenRegister();
            tokenRegister.Email = request.Email;
            tokenRegister.Requestid = id;
            tokenRegister.TokenValue = token;
            _repository.savetoken(tokenRegister);
        }
        public void SendEmail(int id, string[] filenames)
        {
            List<Requestwisefile> files = new();
            foreach(var filename in filenames)
            {
                files.Add(_repository.getRequestWiseFileList(id,filename));
            }
            Request request = _repository.getdataofrequest(id);
            var receiver = "binalmalaviya2002@gmail.com";
            var subject = "Documents of Request " + request.Confirmationnumber?.ToUpper();
            var message = "Find the Files uploaded for your request in below:";
            var mailMessage = new MailMessage(from: "chaityamehta522003@gmail.com", to: receiver, subject, message);
            foreach (var file in files)
            {
                var filePath = "D:\\ProjectMvc\\HalloDocWeb\\HalloDocWeb\\wwwroot\\uploads\\" + file.Filename;
                if (File.Exists(filePath))
                {
                    byte[] fileContent;
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        fileContent = new byte[fileStream.Length];
                        fileStream.Read(fileContent, 0, (int)fileStream.Length);
                    }
                    var attachment = new Attachment(new MemoryStream(fileContent), file.Filename);
                    mailMessage.Attachments.Add(attachment);
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            var mail = "chaityamehta522003@gmail.com";
            var password = "iwbc edlf rgpt oucs";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };
            client.SendMailAsync(mailMessage);
        }
        public Request setclearcase(int id)
        {
            var request = _repository.getdataofrequest(id);
            request.Status = 10;
            _repository.setrequestdata(request);
            return request;
        }
        public void updateadminaddress(AdminProfileModel info)
        {
            var model = _repository.getadmindata(info.admin);
            //var model1 = _repository.getregionById(model.Regionid);
            model.Address1 = info.admin.Address1;
            model.Address2 = info.admin.Address2;
            model.City = info.admin.City;
            model.Zip = info.admin.Zip;
            model.Altphone = info.admin.Altphone;
            model.Modifieddate = DateTime.Now;
            //model1.Name = info.region.Name;
            _repository.saveadmindata(model);
            //_repository.saveadmindata(model1);          
        }
        public void updateadminform(AdminProfileModel info)
        {
            var model = _repository.getadmindata(info.admin);
            model.Firstname = info.admin.Firstname;
            model.Lastname = info.admin.Lastname;
            model.Email = info.admin.Email;
            model.Mobile = info.admin.Mobile;
            model.Modifieddate = DateTime.Now;
            _repository.saveadmindata(model);
        }
        public viewuploadmin viewUploadAdmin(int id)
        {
            var patientData = _repository.getdataofviewcase(id);
            DateOnly date = DateOnly.Parse(DateTime.Parse(patientData.Intdate + patientData.Strmonth + patientData.Intyear).ToString("yyyy-MM-dd"));
            viewuploadmin model = new();
            model.patientData = patientData;
            model.confirmationDetail = _repository.getdataofrequest(id);
            model.FileList = _repository.getRequestWiseFileList(id);
            model.DOB = date;
            return model;
        }

        //HalloDocWebRepository.ViewModel.Notes IAdmin_Service.getrequestnotes(int id)
        //{
        //    throw new NotImplementedException();
        //}   
    }
}