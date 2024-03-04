using System.ComponentModel.DataAnnotations;
using HalloDocWebRepository.Data;
//using HalloDocWebRepository.ViewModel;

namespace HalloDocWebRepository.ViewModel
{
    public class CaseModels
    {
        public Requestclient Requestclient { get; set; }
        public List<Region> regions { get; set; }
        public List<Physician> physics { get; set; }
    }
}