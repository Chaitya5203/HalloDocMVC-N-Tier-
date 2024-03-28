using HalloDocWebRepository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocWebRepository.ViewModel
{
    public class UserAccess
    {
        public List<Aspnetuser> Aspnetuser { get; set; }
        public List<Admin> admins { get; set; }
        public List<Physician> physicsian { get; set; }
        public short? adminstatus { get; set; }
        public short? physicianstatus { get; set; }
        public string Usarname { get; set; }
        public string? Phonenumber { get; set; }
        public string? role { get; set; }
    }
}
