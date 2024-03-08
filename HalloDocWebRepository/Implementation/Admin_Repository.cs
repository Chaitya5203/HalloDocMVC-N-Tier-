using HalloDocWebRepository.Data;
using HalloDocWebRepository.Interfaces;
using HalloDocWebRepository.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace HalloDocWebRepository.Implementation
{
    public class Admin_Repository : IAdmin_Repository
    {
        private readonly ApplicationContext _context;
        public Admin_Repository(ApplicationContext context)
        {
            _context = context;
        }
        public int getcountofeachstate(int id)
        {
           return _context.Requests.Where(r => r.Status == id).Count();
        }
        public IQueryable getdataofdashboard(int id)
        {
            IQueryable data = from r in _context.Requests
                   join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                   join rt in _context.Requesttypes on r.Requesttypeid equals rt.Requesttypeid
                   join reg in _context.Regions on rc.Regionid equals reg.Regionid
                   where r.Status == id 
                   select new AdminDashboardTableModel
                   {
                       Name = rc.Firstname + ' ' + rc.Lastname,
                       Requestor = rt.Name + " , " + r.Firstname + ' ' + r.Lastname,
                       physician = "Dr.Agola",/* + phy.Firstname,*/
                       Dateofservice = r.Lastreservationdate,
                       Requesteddate = r.Createddate,
                       Phonenumber = rc.Phonenumber,
                       Email = r.Email,
                       Address = rc.Street + " , " + rc.City + " , " + rc.Street + " , " + rc.Zipcode,
                       Requestid = r.Requestid,
                       Notes = rc.Notes,
                       Requesttypeid = r.Requesttypeid,
                       RegionName = reg.Name,
                       RequestTypeName = rt.Name
                   };
            return data;
        }
        public IQueryable getdataofdashboardcheckvise(int id, int check)
        {
            IQueryable data = from r in _context.Requests
                   join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                   join rt in _context.Requesttypes on r.Requesttypeid equals rt.Requesttypeid
                   join reg in _context.Regions on rc.Regionid equals reg.Regionid
                   where r.Status == id && r.Requesttypeid == check
                   select new AdminDashboardTableModel
                   {
                       Name = rc.Firstname + ' ' + rc.Lastname,
                       Requestor = rt.Name + " , " + r.Firstname + ' ' + r.Lastname,
                       physician = "Dr.Agola",/* + phy.Firstname,*/
                       Dateofservice = r.Lastreservationdate,
                       Requesteddate = r.Createddate,
                       Phonenumber = rc.Phonenumber,
                       Email = r.Email,
                       Address = rc.Street + " , " + rc.City + " , " + rc.Street + " , " + rc.Zipcode,
                       Requestid = r.Requestid,
                       Notes = rc.Notes,
                       Requesttypeid = r.Requesttypeid,
                       RegionName = reg.Name,
                       RequestTypeName = rt.Name
                   };
            return data;
        }
        public Requestclient getdataofviewcase (int id)
        {
            return _context.Requestclients.FirstOrDefault(m => m.Requestid == id);
        }
        public Region getdataofregionvise(int regionid)
        {
            return _context.Regions.FirstOrDefault(m => m.Regionid == regionid);
        }
        public List<Physician> getphysician()
        {
            return _context.Physicians.ToList();
        }
        public List<Physician> getphysicianbyregion(int id, int regionid)
        {
            return _context.Physicians.Where(m => m.Regionid == regionid).ToList();
        }
        public List<Region> getregion()
        {
            return _context.Regions.ToList();
        }
        public void setrequeststatuslogdata(Requeststatuslog statuslog)
        {
            _context.Requeststatuslogs.Add(statuslog);
            _context.SaveChanges();
        }
        public Request getdataofrequest(int id)
        {
            return _context.Requests.FirstOrDefault(r => r.Requestid == id);
        }
        public Casetag getcasetagdata(int reasonid)
        {
            return _context.Casetags.FirstOrDefault(c => c.Casetagid == reasonid);
        }
        public void setrequestdata(Request request)
        {
            _context.Requests.Update(request);
            _context.SaveChanges ();
        }
        public void setblockrequestdata(Blockrequest blockrequest)
        {
            _context.Blockrequests.Add(blockrequest);
            _context.SaveChanges ();
        }
        public Requestnote getrequestnotebyid(int id)
        {
            return _context.Requestnotes.FirstOrDefault(m => m.Requestid == id);
        }
        public void Addreqnotetable(Requestnote reqnotes)
        {
            _context.Requestnotes.Update(reqnotes);
            _context.SaveChanges ();
        }
        public void addreqnotetablewithnewnotw(Requestnote addreq)
        {
            _context.Requestnotes.Add(addreq);
            _context.SaveChanges ();
        }
        public Requestwisefile getRequestWiseFileList(int id,string filename)
        {
            return _context.Requestwisefiles.FirstOrDefault(m => m.Requestid == id && m.Isdeleted == null && m.Filename == filename);
        }
        public List<Requestwisefile> getRequestWiseFileList(int id)
        {
            return _context.Requestwisefiles.Where(m => m.Requestid == id && m.Isdeleted == null ).ToList();
        }
        public Requestwisefile getRequestWiseFile(int id)
        {
            return _context.Requestwisefiles.FirstOrDefault(m => m.Requestwisefileid == id);
        }
        public void updateRequestWiseFileTable(Requestwisefile file)
        {
            _context.Requestwisefiles.Update(file);
            _context.SaveChanges ();    
        }

        public void addrequestwisefiletablebyadmin(Requestwisefile reqclient)
        {
            _context.Requestwisefiles.Add(reqclient);
            _context.SaveChanges();
        }

        public Requestwisefile RequestwisefilesRepobyadmin(int id)
        {
            return _context.Requestwisefiles.Find(id);
        }

        public Requestwisefile getRequestWiseFileListByFileName(string fl,int id)
        {
            return _context.Requestwisefiles.FirstOrDefault(m => m.Filename == fl && m.Requestid == id && m.Isdeleted == null);
        }

        public List<Healthprofessional> gethealthprofessionaldata()
        {
            return _context.Healthprofessionals.ToList();
        }

        public List<Healthprofessionaltype> gethealthprofessionaltypedata()
        {
            return _context.Healthprofessionaltypes.ToList();   
        }

        public List<Healthprofessional> gethealthprofessionaldatabyid(int hprof)
        {
            return _context.Healthprofessionals.Where(m=> m.Profession== hprof).ToList();
        }

        public Healthprofessional getBusinessDetailById(int hprof)
        {
            return _context.Healthprofessionals.FirstOrDefault(m => m.Vendorid == hprof);
        }

        public void storeordertable(Orderdetail orderdetail1)
        {
            _context.Orderdetails.Add(orderdetail1);
            _context.SaveChanges();
        }

        public void setrequestclientdata(Requestclient reqclient)
        {
            _context.Requestclients.Update(reqclient);
            _context.SaveChanges();
        }
    }
}