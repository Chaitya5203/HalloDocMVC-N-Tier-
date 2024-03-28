using HalloDocWebRepository.Data;
using HalloDocWebRepository.Interfaces;
using HalloDocWebRepository.ViewModel;
using HalloDocWebServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace HalloDocWebServices.Implementation
{
    public class Patient_Service : IPatient_Service
    {
        private readonly IPatient_Repository _repository;
        public Patient_Service(IPatient_Repository repository)
        {
               _repository = repository;
        }
        public void addfilerequestwise(int id, IFormFile fileToUpload)
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
            _repository.addrequestwisefiletable(reqclient);
        }
        public void addnewuserdata(login info)
        {
            var req = _repository.getrequestclientdatabyemail(info.Email);
            var request = _repository.getRequestById(req.Requestid);
            Aspnetuser aspuser = new Aspnetuser
            {
                Usarname = req.Email,
                Passwordhash = info.Passwordhash,
                Email = info.Email,
                Phonenumber = req.Phonenumber,
                Role = "1",
            };
            _repository.addAspuserTable(aspuser);
            User user = new()
            {
                Firstname = req.Firstname,
                Lastname = req.Lastname,
                Email = req.Email,
                Zip = req.Zipcode,
                Mobile = req.Phonenumber,
                Street = req.Street,
                City = req.City,
                State = req.State,
                Aspnetuserid = aspuser.Id,
                Intdate = req.Intdate,
                Intyear = req.Intyear,
                Strmonth = req.Strmonth,
                Createdby = request.Email,
                Createddate = DateTime.Now,
            };
            _repository.addUsertable(user);
            request.Userid = user.Userid;
            _repository.updaterequesttable(request);
        }
        public void addresetpassword(login info)
        {
            var asp = _repository.checkemailofreset(info.Email);
            asp.Passwordhash = info.Passwordhash;
            _repository.updateasptable(asp);
        }
        public bool CheckEmail(string email)
        {
            return _repository.getAspuserByEmail(email);
        }
        public profile Dashboarddata(string? email, string username)
        {
            var userData = _repository.getUser(email);
            var requestData = _repository.getRequest(username);
            DateOnly date = DateOnly.Parse(DateTime.Parse(userData.Intdate + userData.Strmonth + userData.Intyear).ToString("dd-MM-yyyy"));
            profile profile = new();
            profile.Request = requestData;
            profile.User = userData;
            profile.DOB = date;
            return profile;
        }
        public MemoryStream DownloadAllService(int id)
        {
            var filesRow = _repository.getReqWiseFileById(id);
            MemoryStream ms = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                filesRow.ForEach(file =>
                {
                    var path = "D:\\ProjectMvc\\HalloDocWeb\\HalloDocWeb\\wwwroot\\uploads\\" + Path.GetFileName(file.Filename);
                    ZipArchiveEntry zipEntry = zip.CreateEntry(file.Filename);
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (Stream zipEntryStream = zipEntry.Open())
                    {
                        fs.CopyTo(zipEntryStream);
                    }
                });
            return ms;
        }
        public byte[] DownloadSingleFile(int id)
        {
            var file = _repository.RequestwisefilesRepo(id);
            var filepath = "D:\\ProjectMvc\\HalloDocWeb\\HalloDocWeb\\wwwroot\\uploads\\" + Path.GetFileName(file.Filename);
            var bytes = System.IO.File.ReadAllBytes(filepath);
            return bytes;
        }
        public List<Requestwisefile> getdownloadfilerequestwise(int id)
        {
            return _repository.getReqWiseFileById(id);
        }
        public void insertbybusiness(BusinessPatientRequest info)
        {
            Aspnetuser aspuser = _repository.setpatientdatabybusiness(info);
            int Year = info.dob.Year;
            int Date = info.dob.Day;
            System.Globalization.DateTimeFormatInfo dateformat = new System.Globalization.DateTimeFormatInfo();
            var Month = dateformat.GetMonthName(info.dob.Month).ToString();
            if (aspuser == null)
            {
                var receiver = info.p_email;
                var subject = "Create Account"; 
                var message = "Tap on link for Create Account: https://localhost:7234/Home/CreateAccount/?Email=" + receiver;
                var mail = "chaityamehta522003@gmail.com";
                var password = "iwbc edlf rgpt oucs";
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, password)
                };
                client.SendMailAsync(new MailMessage(from: mail, to: receiver, subject, message));
            }
            if (aspuser == null)
            {
                Aspnetuser aspuser1 = new Aspnetuser
                {
                    Usarname = info.p_first_name,
                    Passwordhash = info.p_last_name,
                    Email = info.p_email,
                    Phonenumber = info.p_phone,
                };
                aspuser = aspuser1;
                _repository.addAspuserTable(aspuser);
            }
            User user = new User
            {
                Firstname = info.p_first_name,
                Lastname = info.p_last_name,
                Email = info.p_email,
                Zip = info.p_zip_code,
                Mobile = info.p_phone,
                Street = info.p_street,
                City = info.p_city,
                State = info.p_state,
                Aspnetuserid = aspuser.Id,
                Intdate = Date,
                Intyear = Year,
                Strmonth = Month,
                Createdby = info.Createddate.ToShortDateString(),
                Createddate = info.Createddate
            };
            _repository.addUsertable(user);
            Business business = new Business()
            {
                Name = info.first_name + ' ' + info.last_name,
                Phonenumber = info.phone,
                Createdby = info.Createddate.ToShortDateString(),
                Createddate = info.Createddate,
            };
            _repository.addbusinesstable(business);
            Request request = new Request
            {
                Requesttypeid = 1,
                Userid = user.Userid,
                Isurgentemailsent = new BitArray(1, false),
                Status = 1,
                Firstname = info.first_name,
                Lastname = info.last_name,
                Email = info.email,
                Phonenumber = info.phone,
                Createddate = info.Createddate,
            };
            _repository.addrequesttable(request);
            Requestclient requestclient = new Requestclient
            {
                Requestid = request.Requestid,
                Firstname = info.p_first_name,
                Lastname = info.p_last_name,
                Email = info.p_email,
                Phonenumber = info.p_phone,
                Regionid = 1,
                Street = info.p_street,
                Intdate = Date,
                Intyear = Year,
                Strmonth = Month,
                City = info.p_city,
                Zipcode = info.p_zip_code
            };
            _repository.addrequestclientdata(requestclient);
            Requestbusiness requestbusiness = new Requestbusiness
            {
                Requestid = request.Requestid,
                Businessid = business.Businessid,
            };
            _repository.addrequestbusinesstable(requestbusiness);
        }
        public void insertbyconcierge(ConciergePatientRequest info)
        {
            Aspnetuser aspuser = _repository.setpatientdatabyconcierge(info);
            if (aspuser == null)
            {
                var receiver = info.pemail;
                var subject = "Create Account";
                var message = "Tap on link for Create Account: https://localhost:7234/Home/CreateAccount/?Email=" + receiver;
                var mail = "chaityamehta522003@gmail.com";
                var password = "iwbc edlf rgpt oucs";
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, password)
                };
                client.SendMailAsync(new MailMessage(from: mail, to: receiver, subject, message));
            }
            int Year = info.dob.Year;
            int Date = info.dob.Day;
            System.Globalization.DateTimeFormatInfo dateformat = new System.Globalization.DateTimeFormatInfo();
            var Month = dateformat.GetMonthName(info.dob.Month).ToString();
            Concierge c = new Concierge
            {
                Conciergename = info.cname,
                Regionid = 1,
                Zipcode = info.czip,
                Street = info.cstreet,
                City = info.ccity,
                State = info.cstate,
                Createddate = info.Createddate
            };
            _repository.addconciergetable(c);
            Request request = new Request
            {
                Requesttypeid = 4,
                Isurgentemailsent = new BitArray(1, false),
                Status = 1,
                Firstname = info.cname,
                Lastname = info.clname,
                Email = info.cemail,
                Phonenumber = info.cphone,
                Createddate = info.Createddate,
            };
            _repository.addrequesttable(request);
            Requestclient requestclient = new Requestclient
            {
                Requestid = request.Requestid,
                Firstname = info.first_name,
                Lastname = info.last_name,
                Intdate = Date,
                Intyear = Year,
                Strmonth = Month,
                Email = info.pemail,
                Phonenumber = info.Phonenumber,
                Regionid = 1,
                Street = info.Street,
                City = info.City,
                Zipcode = info.p_zip_code
            };
            _repository.addrequestclientdata(requestclient);
            Requestconcierge requestconcierge = new Requestconcierge
            {
                Requestid = request.Requestid,
                Conciergeid = c.Conciergeid
            };
            _repository.addrequestconciergetable(requestconcierge);
        }
        public void insertbyfamilyfriend(FamilyFriendPatientRequest info)
        {
            Aspnetuser aspuser = _repository.setpatientdatabyfamilyfriend(info);
            int Year = info.dob.Year;
            int Date = info.dob.Day;
            System.Globalization.DateTimeFormatInfo dateformat = new System.Globalization.DateTimeFormatInfo();
            var Month = dateformat.GetMonthName(info.dob.Month).ToString();
            if (aspuser == null)
            {
                var receiver = info.p_email;
                var subject = "Create Account";
                var message = "Tap on link for Create Account: https://localhost:7234/Home/CreateAccount/?Email=" + receiver;
                var mail = "chaityamehta522003@gmail.com";
                var password = "iwbc edlf rgpt oucs";
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, password)
                };
                client.SendMailAsync(new MailMessage(from: mail, to: receiver, subject, message));
            }
            Request request = new Request
            {
                Requesttypeid = 3,
                Isurgentemailsent = new BitArray(1, false),
                Status = 1,
                Firstname = info.f_first_name,
                Lastname = info.f_last_name,
                Email = info.f_email,
                Phonenumber = info.f_phone_number,
                Createddate = info.Createddate,
            };
            _repository.addrequesttable(request);
            Requestclient requestclient = new Requestclient
            {
                Requestid = request.Requestid,
                Firstname = info.p_first_name,
                Lastname = info.p_last_name,
                Email = info.f_email,
                Intdate = Date,
                Intyear = Year,
                Strmonth = Month,
                Phonenumber = info.p_phonenumber,
                Regionid = 1,
                Street = info.p_street,
                City = info.p_city,
                Zipcode = info.p_zip
            };
            _repository.addrequestclientdata(requestclient);
            var file = info.File;
            var uniqueFileName = Path.GetFileName(file.FileName);
            var uploads = "D:\\ProjectMvc\\HalloDocWeb\\HalloDocWeb\\wwwroot\\uploads\\";
            var filePath = Path.Combine(uploads, uniqueFileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            var addrequestfile = new Requestwisefile
            {
                Createddate = DateTime.Now,
                Filename = uniqueFileName,
                Requestid = request.Requestid
            };
            _repository.addrequestwisefiletable(addrequestfile);
        }
        public void insertpatient(Userdata info)
        {
            var userobj = _repository.setpatientdata(info);
            var temp = 0;
            int Year = info.dob.Year;
            int Date = info.dob.Day;
            System.Globalization.DateTimeFormatInfo dateformat = new System.Globalization.DateTimeFormatInfo();
            var Month = dateformat.GetMonthName(info.dob.Month).ToString();
            if (userobj == null)
            {
                Aspnetuser aspuser = new Aspnetuser
                {
                    Usarname = info.first_name,
                    Passwordhash = info.last_name,
                    Email = info.email,
                    Createddate = info.Createddate
                };
                _repository.addAspuserTable(aspuser);
                temp = aspuser.Id;
            }
            User user = new User
            {
                Firstname = info.first_name,
                Lastname = info.last_name,
                Email = info.email,
                Mobile = info.phonenumber,
                Street = info.street,
                City = info.city,
                State = info.state,
                Aspnetuserid = temp,
                Intdate = Date,
                Intyear = Year,
                Strmonth = Month,
                Zip = info.zipcode,
                Createdby = info.Createddate.ToShortDateString(),
                Createddate = info.Createddate
            };
            _repository.addUsertable(user);
            Request request = new Request
            {
                Requesttypeid = 2,
                Userid = user.Userid,
                Isurgentemailsent = new BitArray(1, false),
                Status = 1,
                Firstname = info.first_name,
                Lastname = info.last_name,
                Email = info.email,
                Phonenumber = info.phonenumber,
                Createddate = info.Createddate,
            };
            _repository.addrequesttable(request);
            Requestclient requestclient = new Requestclient
            {
                Requestid = request.Requestid,
                Firstname = info.first_name,
                Lastname = info.last_name,
                Email = info.email,
                Phonenumber = info.phonenumber,
                Regionid = 1,
                Street = info.street,
                City = info.city,
                Intdate = info.dob.Day,
                Intyear = info.dob.Year,
                Strmonth = info.dob.Month.ToString(),
                Zipcode = info.zipcode
            };
            _repository.addrequestclientdata(requestclient);
            var file = info.File;
            var uniqueFileName = Path.GetFileName(file.FileName);
            var uploads = "D:\\ProjectMvc\\HalloDocWeb\\HalloDocWeb\\wwwroot\\uploads\\";
            var filePath = Path.Combine(uploads, uniqueFileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            var addrequestfile = new Requestwisefile
            {
                Createddate = DateTime.Now,
                Filename = uniqueFileName,
                Requestid = request.Requestid
            };
            _repository.addrequestwisefiletable(addrequestfile);
        }
        public void RequestMe(Userdata info,string email)
        {
            var user = _repository.getUser(email);
            Request request = new Request
            {
                Requesttypeid = 2,
                Userid = user.Userid,
                Isurgentemailsent = new BitArray(1, false),
                Status = 1,
                Firstname = info.first_name,
                Lastname = info.last_name,
                Email = info.email,
                Phonenumber = info.phonenumber,
                Createddate = info.Createddate,
            };
            _repository.addrequesttable(request);
            Requestclient requestclient = new Requestclient
            {
                Requestid = request.Requestid,
                Firstname = info.first_name,
                Lastname = info.last_name,
                Email = info.email,
                Phonenumber = info.phonenumber,
                Regionid = 1,
                Street = info.street,
                City = info.city,
                Zipcode = info.zipcode
            };
            _repository.addrequestclientdata(requestclient);
            var file = info.File;
            var uniqueFileName = Path.GetFileName(file.FileName);
            var uploads = Path.Combine("wwwroot", "uploads");
            var filePath = Path.Combine(uploads, uniqueFileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            var addrequestfile = new Requestwisefile
            {
                Createddate = DateTime.Now,
                Filename = uniqueFileName,
                Requestid = request.Requestid
            };
            _repository.addrequestwisefiletable(addrequestfile);
        }
        public void Requestsomeoneelse(someoneelse info, string email)
        {
            var user = _repository.getUser(email);
            Request request = new Request
            {
                Requesttypeid = 3,
                Userid = user.Userid,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Phonenumber = user.Mobile,
                Email = user.Email,
                Status = 1,
                Relationname = info.relation,
                Createddate = DateTime.Now,
                Isurgentemailsent = new BitArray(1, false)
            };
            _repository.addrequesttable(request);
            Requestclient reqclient = new Requestclient
            {
                Requestid = request.Requestid,
                Firstname = info.p_first_name,
                Lastname = info.p_last_name,
                Phonenumber = info.p_phonenumber,
                Email = info.p_email,
                Location = info.p_street + "," + info.p_city + "," + info.p_state + " ," + info.p_zip,
                Regionid = 1
            };
            _repository.addrequestclientdata(reqclient);
        }
        public Requestwisefile RequestwisefilesSer(int id)
        {
            return _repository.RequestwisefilesRepo(id);     
        }
        public void updateProfile(Userdata info,string email)
        {
            var upadateAsp = _repository.ProfileAspdata(email);
            var upadateUser = _repository.ProfileUserdata(email);
            upadateAsp.Usarname = info.first_name;
            upadateAsp.Passwordhash = info.last_name;
            upadateAsp.Email = info.email;
            upadateAsp.Createddate = info.Createddate;
            upadateUser.Firstname = info.first_name;
            upadateUser.Lastname = info.last_name;
            upadateUser.Lastname = info.last_name;
            upadateUser.Email = info.email;
            upadateUser.Mobile = info.phonenumber;
            upadateUser.Street = info.street;
            upadateUser.City = info.city;
            upadateUser.State = info.state;
            upadateUser.Createddate = info.Createddate;
            _repository.UpdateAspnetuser(upadateAsp);
            _repository.UpdateUser(upadateUser);
            _repository.SaveDbChanges();
        }
        Dictionary<int, int> IPatient_Service.getDictionary( string? v2)
        {
            var requestData = _repository.getRequest(v2);
            Dictionary<int, int> requestIdCounts = new Dictionary<int, int>();
            foreach (var request in requestData)
            {
                int count = _repository.requestWiseFile(request.Requestid);
                requestIdCounts.Add(request.Requestid, count);
            }
            return requestIdCounts;
        }
        Aspnetuser IPatient_Service.getUser(string usarname)
        {
            return _repository.getAspnetusername(usarname);
        }
        bool IPatient_Service.ValidateUser(login aspnetuser)
        {
            bool check = _repository.getUser(aspnetuser);
            if (check) {return true;}
            else { return false;}   
        }
    }
}
