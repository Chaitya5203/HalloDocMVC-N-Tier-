using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HalloDocWebRepository.Data;
using System.Linq;
namespace HalloDocWebRepository.ViewModel
{
    public class profile    
    {
        public List<Request>? Request { get; set; }
        public User? User { get; set; }    
        public DateOnly? DOB { get; set; }
    }
}
