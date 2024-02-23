using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocWebServices.Interfaces
{
    public interface IPatient_Service
    {
        public bool CheckEmail(string email);
        public profile Dashboarddata(string email,string username);
        public Dictionary<int,int> getDictionary(string? v2);
        public Aspnetuser getUser(string usarname);
        public void insertbybusiness(BusinessPatientRequest info);
        public void insertbyconcierge(ConciergePatientRequest info);
        public void insertbyfamilyfriend(FamilyFriendPatientRequest info);
        public void insertpatient(Userdata info);
        public void updateProfile(Userdata info, string email);
        public bool ValidateUser(login aspnetuser);
    }
}
