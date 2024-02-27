using HalloDocWebRepository.Data;
using HalloDocWebRepository.Interfaces;
using HalloDocWebRepository.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocWebRepository.Implementation
{
    public class Patient_Repository : IPatient_Repository
    {
        private readonly ApplicationContext _context;
        public Patient_Repository(ApplicationContext context )
        {
            _context = context;
        }
        public void addAspuserTable(Aspnetuser aspuser)
        {
            _context.Aspnetusers.Add(aspuser);
            _context.SaveChanges();
        }

        public void addbusinesstable(Business business)
        {
            _context.Businesses.Add(business);
            _context.SaveChanges();
        }

        public void addconciergetable(Concierge c)
        {
            _context.Concierges.Add(c);
            _context.SaveChanges();
        }

        public void addrequestbusinesstable(Requestbusiness requestbusiness)
        {
            _context.Requestbusinesses.Add(requestbusiness);    
            _context.SaveChanges(); 
        }

        public void addrequestclientdata(Requestclient requestclient)
        {
            _context.Requestclients.Add(requestclient);
            _context.SaveChanges(); 
        }

        public void addrequestconciergetable(Requestconcierge requestconcierge)
        {
            _context.Requestconcierges.Add(requestconcierge);
            _context.SaveChanges();
        }

        public void addrequesttable(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }
        public void addrequestwisefiletable(Requestwisefile addrequestfile)
        {
            _context.Requestwisefiles.Add(addrequestfile);
            _context.SaveChanges();
        }
        public void addUsertable(User user)
        {
            _context.Users.Add(user);   
            _context.SaveChanges();
        }
        public bool getAspuserByEmail(string email)
        {
            return _context.Aspnetusers.Any(u => u.Email == email);
        }
        public List<Request> getRequest(string username)
        {
            var user = _context.Users.FirstOrDefault(u=> u.Firstname == username);  
           return _context.Requests.Where(m => m.Userid == user.Userid).ToList();
        }
        public bool getUser(login aspnetuser)
        {
            return _context.Aspnetusers.Any( u=> u.Email == aspnetuser.Email && u.Passwordhash == aspnetuser.Passwordhash );
        }
        public Aspnetuser ProfileAspdata(string email)
        {
            return _context.Aspnetusers.FirstOrDefault(m => m.Email == email);
        }
        public User ProfileUserdata(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public Aspnetuser setpatientdata(Userdata info)
        {
            return _context.Aspnetusers.FirstOrDefault(m => m.Usarname == info.first_name);
        }

        public Aspnetuser setpatientdatabybusiness(BusinessPatientRequest info)
        {
            return _context.Aspnetusers.FirstOrDefault(m => m.Email == info.email); 
        }

        public Aspnetuser setpatientdatabyconcierge(ConciergePatientRequest info)
        {
            return _context.Aspnetusers.FirstOrDefault(m => m.Email == info.pemail);
        }

        public Aspnetuser setpatientdatabyfamilyfriend(FamilyFriendPatientRequest info)
        {
            return _context.Aspnetusers.FirstOrDefault(m => m.Email == info.p_email);
        }
        Aspnetuser IPatient_Repository.getAspnetusername(string usarname)
        {
            return _context.Aspnetusers.FirstOrDefault(u => u.Email == usarname);
        }
        User IPatient_Repository.getUser(string? email)
        {
            return _context.Users.FirstOrDefault(m => m.Email == email);
        }
        int IPatient_Repository.requestWiseFile(int requestid)
        {
            return _context.Requestwisefiles.Count(r => r.Requestid == requestid);
        }
        void IPatient_Repository.SaveDbChanges()
        {
            _context.SaveChanges();
        }
        void IPatient_Repository.UpdateAspnetuser(Aspnetuser upadateAsp)
        {
            _context.Aspnetusers.Update(upadateAsp);
        }
        void IPatient_Repository.UpdateUser(User upadateUser)
        {
            _context.Users.Update(upadateUser);
        }
    }
}
