using HalloDocWebRepository.Data;
using HalloDocWebRepository.Interfaces;
using HalloDocWebRepository.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HalloDocWebServices.Interfaces
{
    public class Admin_Service : IAdmin_Service
    {
        private readonly IAdmin_Repository _repository;
        public Admin_Service(IAdmin_Repository repository)
        {
            _repository = repository;
        }
        public IQueryable dashboardtabledata(int id, int check)
        {
            IQueryable data;
            if (check == 0)
            {
               data = _repository.getdataofdashboard(id);
            }
            else
            {
               data = _repository.getdataofdashboardcheckvise(id,check);
            }
            return data;
        }

        public void deleteFile(int id)
        {
            var file = _repository.getRequestWiseFile(id);
            file.Isdeleted = new BitArray(1, true);
            _repository.updateRequestWiseFileTable(file);
        }

        public Casetag getcasetag(int reasonid)
        {
            var reason = _repository.getcasetagdata(reasonid);
            return reason;
        }
        public int getcount(int id)
        {
            return _repository.getcountofeachstate(id);
        }

        public void getreqnoteofsavenote(int id,Notes n , string email)
        {
            var reqnotes = _repository.getrequestnotebyid(id);
            if (reqnotes != null)
            {
                reqnotes.Adminnotes = n.AdminNotes;
                reqnotes.Physiciannotes = reqnotes.Physiciannotes;
                reqnotes.Createdby = email;
                reqnotes.Modifiedby = email;
                reqnotes.Modifieddate = DateTime.Now;
                _repository.Addreqnotetable(reqnotes);
               
            }
            else
            {
                Requestnote addreq = new Requestnote
                {
                    Requestid = id,
                    Adminnotes = n.AdminNotes,
                    Createdby = email,
                    Createddate = DateTime.Now,
                };
                _repository.addreqnotetablewithnewnotw(addreq);   
            }
            
        }
        public Request getrequestdata(int id, string name)
        {
            var request= _repository.getdataofrequest(id);
            request.Status = 5;
            request.Casetag =name;
            _repository.setrequestdata(request);
            return request;
        }

        public Request getrequestdataofnotes(int id)
        {
            var request = _repository.getdataofrequest(id);
            return request;
        }

        public Request getrequestdatatoassigncase(int id)
        {
            var request = _repository.getdataofrequest(id);
            request.Status = 2;
            _repository.setrequestdata(request);
            return request;
        }

        public Request getrequestdatatoblockcase(int id)
        {
            var request = _repository.getdataofrequest(id);
            //Request request = _context.Requests.FirstOrDefault(r => r.Requestid == id);
            request.Status = 10;
            _repository.setrequestdata(request);
            return request;
        }

        public Notes getrequestnotes(int id)
        {
            var note = _repository.getrequestnotebyid(id);
            Notes notes = new();

           
            if (note != null)
            {
                if (note.Physiciannotes == null)
                    notes.phyNotes = "---";
               else
                    notes.phyNotes = note.Physiciannotes;
                if (note.Adminnotes == null)
                    notes.AdminNotes = "---";
                else
                    notes.AdminNotes = note.Adminnotes;
            }
            else
            {
                notes.phyNotes = notes.AdminNotes = "---";
            }
            notes.req_id = id;
            return notes;
        }

        //public Requeststatuslog getrequeststatuslog(int id)
        //{
        //    Requeststatuslog requeststatuslog = new();
        //}

        public requestclientvisedata getviewcasedataofpatient(int id)
        {
            var data = _repository.getdataofviewcase(id);
            var region = _repository.getdataofregionvise(data.Regionid);
            DateOnly date = DateOnly.Parse(DateTime.Parse(data.Intyear + data.Strmonth + data.Intdate).ToString("yyyy-MM-dd"));
            //var data = await _context.Requestclients.FirstOrDefaultAsync(m => m.Requestid == id);
            //var region = await _context.Regions.FirstOrDefaultAsync(m => m.Regionid == data.Regionid);
            requestclientvisedata model = new();
            model.FName = data.Firstname;
            model.LName = data.Lastname;
            model.DOB = date;
            model.Notes = data.Notes;
            model.Phonenumber = data.Phonenumber;
            model.Email = data.Email;
            model.RegionName = region.Name;
            model.Address = data.Street + " " + data.City + " " + data.State + " " + data.Zipcode;

            return model;
        }

        public void insertblockrequesttable(int id, string email, string phonenumber, string notes)
        {
            Blockrequest blockrequest = new Blockrequest
            {
                Requestid = id.ToString(),
                Email = email,
                Createddate = DateTime.Now,
                Reason = notes,
                Phonenumber = phonenumber,
            };
            _repository.setblockrequestdata(blockrequest);
        }

        public void insertrequeststatuslogtable(int id, string notes, int reasonid)
        {
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = 3,
                Createddate = DateTime.Now,
            };
            _repository.setrequeststatuslogdata(statuslog);
            //_context.Requeststatuslogs.Add(statuslog);
            //_context.Requests.Update(request);
            //_context.SaveChanges();
        }

        public void insertrequeststatuslogtablebyassign(int id, string notes)
        {
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = 2,
                Createddate = DateTime.Now,
            };
            _repository.setrequeststatuslogdata(statuslog);
        }

        public void insertrequeststatuslogtableofblockcase(int id, string notes)
        {
            Requeststatuslog statuslog = new Requeststatuslog
            {
                Requestid = id,
                Notes = notes,
                Status = 10,
                Createddate = DateTime.Now,
            };
            _repository.setrequeststatuslogdata(statuslog);
        }

        public CaseModels openassignmodel(int id, int regionid)
        {
            CaseModels model = new();
            if (regionid == 0)
            {
                var physician = _repository.getphysician();
                //var physician = _context.Physicians.ToList();
                model.physics = physician;
            }
            else
            {
                var physician = _repository.getphysicianbyregion(id, regionid);
                //var physician = _context.Physicians.Where(m => m.Regionid == regionid).ToList();
                model.physics = physician;
            }
            var region = _repository.getregion();
            //var region = _context.Regions.ToList();
            model.regions = region;
            return model;
        }
        public Requestclient opencancelmodel(int id)
        {
            return _repository.getdataofviewcase(id);
        }

        public viewuploadmin viewUploadAdmin(int id)
        {
            viewuploadmin model = new();
            model.patientData = _repository.getdataofviewcase(id);
            model.confirmationDetail = _repository.getdataofrequest(id);
            model.FileList = _repository.getRequestWiseFileList(id);
            return model;

        }
    }
}