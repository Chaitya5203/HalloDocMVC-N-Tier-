using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HalloDocWebServices.Interfaces
{
    public interface IAdmin_Service
    {
        void addrequestwisefilebyadmin(int id, IFormFile fileToUpload);
        void AgreementAccepted(int id);
        bool AgreementCancel(int id, string notes);
        public void closecasetounpaid(int id, viewuploadmin n);
        public IQueryable dashboardtabledata(int id, int check);
        public void deleteallfilesbyadmin(string[] reqids, int id);
        public void deleteFile(int id);
        public MemoryStream DownloadAllServicebyadmin( string[] filenames);
        public byte[] DownloadSingleFilebyadmin(int id);
        public Encounterformmodel EncounterAdmin(int id);
        public Casetag getcasetag(int reasonid);
        public int getcount(int id);
        public sendorder getdataofsendorder(int id,int hprof,int hproftype);
        AdminProfileModel getMyProfileData(string? v);
        public void getreqnoteofsavenote(int id,Notes n ,string email);
        public Request getrequestdata(int id,string name);
        List<Request> GetRequestDataInList();
        public Request getrequestdataofnotes(int id);
        public Request getrequestdatatoassigncase(int id,int physician);
        public Request getrequestdatatoblockcase(int id);
        public Request getrequestdatatotransfercase(int id,int physician);
        public Notes getrequestnotes(int id);
        public Requestclient getReviewAgreementData(TokenRegister token);
        TokenRegister getTokenRegidterDataByToken(string token);

        //public Requeststatuslog getrequeststatuslog(int id);
        public requestclientvisedata getviewcasedataofpatient(int id);
        void insertblockrequesttable(int id, string? email, string? phonenumber, string notes);
        void insertordertable(sendorder sendorder );
        void insertrequeststatuslogtable(int id, string notes, int reasonid);
        void insertrequeststatuslogtablebyassign(int id, string notes,int physician);
        void insertrequeststatuslogtablebytransfer(int id, string notes,int physician);
        void insertrequeststatuslogtableclearcase(int id);
        void insertrequeststatuslogtableofblockcase(int id, string notes);
        public CaseModels openassignmodel(int id, int regionid);
        public Requestclient opencancelmodel(int id);
        public Requestwisefile RequestwisefilesSerbyadmin(int id);
        void saveEncounterForm(Encounterformmodel info);
        public sendorder sendAgreement(int id);
        void SendAgreementEmail(int id);
        void SendEmail(int id, string[] filename);
        public Request setclearcase(int id);
        void updateadminaddress(AdminProfileModel info);
        void updateadminform(AdminProfileModel info);
        public viewuploadmin viewUploadAdmin(int id);
    }
}
