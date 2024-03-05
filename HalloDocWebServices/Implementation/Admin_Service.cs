using HalloDocWebRepository.Data;
using HalloDocWebRepository.Interfaces;
using HalloDocWebRepository.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NuGet.Protocol.Core.Types;
using System.Net.Mail;
using System.Net;

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

        public IQueryable dashboardtabledata(int id, int check)
        {
            IQueryable data;
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
        public Casetag getcasetag(int reasonid)
        {
            var reason = _repository.getcasetagdata(reasonid);
            return reason;
        }
        public int getcount(int id)
        {
            return _repository.getcountofeachstate(id);
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
        public Request getrequestdataofnotes(int id)
        {
            var request = _repository.getdataofrequest(id);
            return request;
        }
        public Request getrequestdatatoassigncase(int id)
        {
            var request = _repository.getdataofrequest(id);
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
        public Notes getrequestnotes(int id)
        {
            var note = _repository.getrequestnotebyid(id);
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
            notes.req_id = id;
            return notes;
        }

        //public Requeststatuslog getrequeststatuslog(int id)
        //{
        //    Requeststatuslog requeststatuslog = new();
        //}

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
        public void insertrequeststatuslogtablebyassign(int id, string notes)
        {
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = 2,
                Createddate = DateTime.Now,
            };
            _repository.setrequeststatuslogdata(statuslog);
        }
        public void insertrequeststatuslogtableofblockcase(int id, string notes)
        {
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = 10,
                Createddate = DateTime.Now,
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
        public viewuploadmin viewUploadAdmin(int id)
        {
            viewuploadmin model = new();
            model.patientData = _repository.getdataofviewcase(id);
            model.confirmationDetail = _repository.getdataofrequest(id);
            model.FileList = _repository.getRequestWiseFileList(id);
            return model;
        }
    }
}