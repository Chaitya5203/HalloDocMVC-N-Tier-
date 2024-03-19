using HalloDocWebRepository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HalloDocWebRepository.ViewModel
{
    public class AdminProfileModel
    {
        public List<Adminregion> adminregion {  get; set; }
        public Admin admin { get; set; }
        public Aspnetuser adminuser { get; set; }   
        public Region region { get; set; }
        public List<Region> regions { get; set; }
    }
}