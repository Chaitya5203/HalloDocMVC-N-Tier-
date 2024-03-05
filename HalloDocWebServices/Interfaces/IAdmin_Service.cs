﻿using HalloDocWebRepository.Data;
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
        public IQueryable dashboardtabledata(int id, int check);
        public void deleteallfilesbyadmin(string[] reqids, int id);
        public void deleteFile(int id);
        public MemoryStream DownloadAllServicebyadmin( string[] filenames);
        public byte[] DownloadSingleFilebyadmin(int id);
        public Casetag getcasetag(int reasonid);
        public int getcount(int id);
        public void getreqnoteofsavenote(int id,Notes n ,string email);
        public Request getrequestdata(int id,string name);
        public Request getrequestdataofnotes(int id);
        public Request getrequestdatatoassigncase(int id);
        public Request getrequestdatatoblockcase(int id);
        public Notes getrequestnotes(int id);
        //public Requeststatuslog getrequeststatuslog(int id);
        public requestclientvisedata getviewcasedataofpatient(int id);
        void insertblockrequesttable(int id, string? email, string? phonenumber, string notes);
        void insertrequeststatuslogtable(int id, string notes, int reasonid);
        void insertrequeststatuslogtablebyassign(int id, string notes);
        void insertrequeststatuslogtableofblockcase(int id, string notes);
        public CaseModels openassignmodel(int id, int regionid);
        public Requestclient opencancelmodel(int id);
        public Requestwisefile RequestwisefilesSerbyadmin(int id);
        void SendEmail(int id, string[] filename);
        public viewuploadmin viewUploadAdmin(int id);
    }
}
