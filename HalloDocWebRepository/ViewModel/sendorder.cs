using System.ComponentModel.DataAnnotations;
using HalloDocWebRepository.Data;
//using HalloDocWebRepository.ViewModel;

namespace HalloDocWebRepository.ViewModel
{
    public class sendorder
    {
        public List<Healthprofessional> healthprofessional { get; set; }
        public List<Healthprofessionaltype> healthprofessionaltype { get; set; }
        public Healthprofessional businessDetail { get; set; }
        
        public Requestclient requestclient { get; set; }
        public int RequestType { get; set; }
        public string Detail { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Businesscontact { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int SelectedVendorId { get; set; }
        public int refill { get; set; }
        public int id { get; set; }
    }
}