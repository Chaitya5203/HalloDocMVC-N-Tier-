using HalloDocWebRepository.Data;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace HalloDocWebRepository.ViewModel
{
    public class Encounterformmodel
    {
        public List<Requestwisefile> FileList { get; set; }
        public Requestclient patientData { get; set; }
        public Request confirmationDetail { get; set; }
        public IFormFile fileToUpload { get; set; }
        public DateOnly? DOB { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public int Requestid { get; set; }
        public BitArray? IsFinalized { get; set; }
        public string? HistoryIllness { get; set; }
        public string? MedicalHistory { get; set; }
        public DateTime? Date { get; set; }
        public string? Medications { get; set; }
        public string? Allergies { get; set; }
        public decimal? Temp { get; set; }
        public decimal? Hr { get; set; }
        public decimal? Rr { get; set; }
        public int? BpS { get; set; }
        public int? BpD { get; set; }
        public decimal? O2 { get; set; }
        public string? Pain { get; set; }
        public string? Heent { get; set; }
        public string? Cv { get; set; }
        public string? Chest { get; set; }
        public string? Abd { get; set; }
        public string? Extr { get; set; }
        public string? Skin { get; set; }
        public string? Neuro { get; set; }
        public string? Other { get; set; }
        public string? Diagnosis { get; set; }
        public string? TreatmentPlan { get; set; }
        public string? MedicationDispensed { get; set; }
        public string? Procedures { get; set; }
        public string? FollowUp { get; set; }
    }
}