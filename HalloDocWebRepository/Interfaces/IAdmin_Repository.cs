using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HalloDocWebRepository.Interfaces
{
    public interface IAdmin_Repository
    {
        public int getcountofeachstate(int id);
        public IQueryable<AdminDashboardTableModel> getdataofdashboard(int id);
        public IQueryable<AdminDashboardTableModel> getdataofdashboardcheckvise(int id, int check);
        public Requestclient getdataofviewcase(int id);
        public HalloDocWebRepository.Data.Region getdataofregionvise(int regionid);
        public List<Physician> getphysician();
        public List<Physician> getphysicianbyregion(int id, int regionid);
        public List<Region> getregion();
        void setrequeststatuslogdata(Requeststatuslog statuslog);
        public Request getdataofrequest(int id);
        public Casetag getcasetagdata(int reasonid);
        void setrequestdata(Request request);

        void setblockrequestdata(Blockrequest blockrequest);
        public Requestnote getrequestnotebyid(int id);
        void Addreqnotetable(Requestnote reqnotes);
        void addreqnotetablewithnewnotw(Requestnote reqnotes);
        public Requestwisefile getRequestWiseFileList(int id,string filename);
        public List<Requestwisefile> getRequestWiseFileList(int id);
        public Requestwisefile getRequestWiseFile(int id);
        void updateRequestWiseFileTable(Requestwisefile file);
        void addrequestwisefiletablebyadmin(Requestwisefile reqclient);
        public Requestwisefile RequestwisefilesRepobyadmin(int id);
        public Requestwisefile getRequestWiseFileListByFileName(string fl,int id);
        public List<Healthprofessional> gethealthprofessionaldata();
        public List<Healthprofessionaltype> gethealthprofessionaltypedata();
        public List<Healthprofessional> gethealthprofessionaldatabyid(int hprof);
        Healthprofessional getBusinessDetailById(int hprof);
        void storeordertable(Orderdetail orderdetail1);
        void setrequestclientdata(Requestclient reqclient);
        void updateEncounterForm(EncounterForm model);
        public EncounterForm getEncounterTable(int id);
        Aspnetuser getAspnetuserByEmail(string? v);
        Admin getAdminByAspnetId(int id);
        Region getregionById(int? regionid);
        List<Request> getINlist();
        List<Requeststatuslog> getRequestStatusLoglist(int id);
        Physician getPhysicianById(int? physicianid);
        public Admin getadmindata(Admin admin);
        void saveadmindata(Admin model);
        void savetoken(TokenRegister tokenRegister);
        TokenRegister getTokenRegisterByToken(string token);
        Requestclient getRequestClientByEmail(string? email);
        TokenRegister updatetokenregister(int id);
        void updateandsavetokenregister(TokenRegister tokenRegister);
        List<Adminregion> getadminregionname(int id);
        User getUser(string email);
        void addRequestTable(Request request);
        void addRequestClienttable(Requestclient requestclient);
        object getadminregions(int adminid);
        void AddAdminReg(Adminregion ar);
        void RemoveAdminReg(Adminregion ar);
        void addphysiciantable(Physician model);
        Physician getphysiciandata(int id);
        List<Physicianregion> getphysicianregionname(int id);
        void updatePhysician(Physician physician);
        void AddPhysicianReg(Physicianregion pr);
        void RemovePhysicianReg(Physicianregion pr);
        List<Menu> getmenudataof();
        List<Menu> getMenuListWithCheck(int check);
        void saveRole(Role role);
        void saveRoleMenu(Rolemenu rolemenu);
        List<Role> getallrole();
        Role getdataofrole(int id);
        List<Rolemenu> getdataofrolemenu(int id);
    }
}