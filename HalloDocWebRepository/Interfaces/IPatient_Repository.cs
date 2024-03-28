using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
namespace HalloDocWebRepository.Interfaces
{
    public interface IPatient_Repository
    {
        void addAspuserTable(Aspnetuser aspuser);
        void addbusinesstable(Business business);
        void addconciergetable(Concierge c);
        void addrequestbusinesstable(Requestbusiness requestbusiness);
        void addrequestclientdata(Requestclient requestclient);
        void addrequestconciergetable(Requestconcierge requestconcierge);
        void addrequesttable(Request request);
        void addrequestwisefiletable(Requestwisefile addrequestfile);
        void addUsertable(User user);
        public Aspnetuser checkemailofreset(string email);
        public Requestwisefile downloadRequesrWiseFile(int id);
        public Aspnetuser getAspnetusername(string usarname);
        public bool getAspuserByEmail(string email);
        public List<Request> getRequest(string username);
        public Request getRequestById(int requestid);
        public Requestclient getrequestclientdatabyemail(string email);
        public List<Requestwisefile> getReqWiseFileById(int id);
        public bool getUser(login aspnetuser);
        public User getUser(string? email);
        public Aspnetuser ProfileAspdata(string email);
        public User ProfileUserdata(string email);
        public int requestWiseFile(int requestid);
        public Requestwisefile RequestwisefilesRepo(int id);
        public void SaveDbChanges();
        public Aspnetuser setpatientdata(Userdata info);
        public Aspnetuser setpatientdatabybusiness(BusinessPatientRequest info);
        public Aspnetuser setpatientdatabyconcierge(ConciergePatientRequest info);
        public Aspnetuser setpatientdatabyfamilyfriend(FamilyFriendPatientRequest info);
        public void UpdateAspnetuser(Aspnetuser upadateAsp);
        void updateasptable(Aspnetuser asp);
        void updaterequesttable(Request request);
        public void UpdateUser(User upadateUser);
    }
}
