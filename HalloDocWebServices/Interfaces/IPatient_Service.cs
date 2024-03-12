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
    public interface IPatient_Service
    {
        public void addfilerequestwise(int id, IFormFile fileToUpload);
        public void addnewuserdata(login info);
        public void addresetpassword(login info);
        public bool CheckEmail(string email);
        public profile Dashboarddata(string email,string username);
        public MemoryStream DownloadAllService(int id);
        public byte[]  DownloadSingleFile(int id);
        public Dictionary<int,int> getDictionary(string? v2);
        public List<Requestwisefile> getdownloadfilerequestwise(int id);
        public Aspnetuser getUser(string usarname);
        public void insertbybusiness(BusinessPatientRequest info);
        public void insertbyconcierge(ConciergePatientRequest info);
        public void insertbyfamilyfriend(FamilyFriendPatientRequest info);
        public void insertpatient(Userdata info);
        public void RequestMe(Userdata info,string email);
        public void Requestsomeoneelse(someoneelse info, string? v);
        public Requestwisefile RequestwisefilesSer(int id);
        public void updateProfile(Userdata info, string email);
        public bool ValidateUser(login aspnetuser);
    }
}
