using HalloDocWebRepository.Data;
using Microsoft.AspNetCore.Http;
namespace HalloDocWebRepository.ViewModel
{
    public class viewuploadmin
    {
        public List<Requestwisefile> FileList { get; set; }
        public Requestclient patientData { get; set; }
        public Request confirmationDetail { get; set; }
        public IFormFile fileToUpload { get; set; }
    }
}