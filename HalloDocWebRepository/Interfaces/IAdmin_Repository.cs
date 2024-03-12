using HalloDocWebRepository.Data;
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
        public IQueryable getdataofdashboard(int id);
        public IQueryable getdataofdashboardcheckvise(int id, int check);
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
    }
}