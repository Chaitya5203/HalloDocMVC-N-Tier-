using System.ComponentModel.DataAnnotations;
using HalloDocWebRepository.Data;
//using HalloDocWebRepository.ViewModel;

namespace HalloDocWebRepository.ViewModel
{
    public class sendorder
    {
        public Healthprofessional healthprofessional { get; set; }
        public List<Region> regions { get; set; }
        public List<Healthprofessionaltype> healthprofessionaltype { get; set; }
    }
}