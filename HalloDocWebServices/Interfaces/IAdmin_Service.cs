using DocumentFormat.OpenXml.Spreadsheet;
using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HalloDocWebServices.Interfaces
{
    public interface IAdmin_Service
    {
        void addphysiciandata(PhysicianProfile phy);
        void addrequestwisefilebyadmin(int id, IFormFile fileToUpload);
        void AgreementAccepted(int id);
        bool AgreementCancel(int id, string notes);
        public void closecasetounpaid(int id, viewuploadmin n);
        public IQueryable<AdminDashboardTableModel> dashboardtabledata(int id, int check);
        //public IQueryable<UserAccess> getaspnetuserdata();
        public void deleteallfilesbyadmin(string[] reqids, int id);
        public void deleteFile(int id);
        public MemoryStream DownloadAllServicebyadmin( string[] filenames);
        public byte[] DownloadSingleFilebyadmin(int id);
        public Encounterformmodel EncounterAdmin(int id);
        void generateRole(string roleName, string[] selectedRoles, int check,string email);
        public Casetag getcasetag(int reasonid);
        public int getcount(int id);
        public sendorder getdataofsendorder(int id,int hprof,int hproftype);
        RoleModel GetMenuData(int check,string rolename);
        AdminProfileModel getMyProfileData(string? v);
        List<Physician> getPhycision();
        PhysicianProfile getphysicianprofiledata(int id);
        List<Region> getRegionList();
        public void getreqnoteofsavenote(int id,Notes n ,string email);
        public Request getrequestdata(int id,string name);
        List<Request> GetRequestDataInList();
        public Request getrequestdataofnotes(int id);
        public Request getrequestdatatoassigncase(int id,int physician);
        public Request getrequestdatatoblockcase(int id);
        public Request getrequestdatatotransfercase(int id,int physician);
        public Notes getrequestnotes(int id);
        public Requestclient getReviewAgreementData(TokenRegister token);
        RoleModel getrolewisedataofrole(int id);
        TokenRegister getTokenRegidterDataByToken(string token);
        public List<Role> getrolewisedata();
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
        void saveadminrequest(Userdata1 info, string? v);
        void saveEncounterForm(Encounterformmodel info);
        public sendorder sendAgreement(int id);
        void SendAgreementEmail(int id);
        void SendEmail(int id, string[] filename);
        void SendLink(string email);
        public Request setclearcase(int id);
        void updateadminaddress(AdminProfileModel info);
        void updateadminform(AdminProfileModel info);
        void updatephysicianbilling(PhysicianProfile phy);
        void updatephysicianprofile(PhysicianProfile phy);
        public viewuploadmin viewUploadAdmin(int id);
        void updateroleof(RoleModel roleModel);
        void deleterole(int id);
        AdminProfileModel getAdminRoleData();
        void CreateAdminAccount(AdminProfileModel model,string email);
        void saveadminpassword(AdminProfileModel info);
        UserAccess getaspnetuserdata();

    }
}
