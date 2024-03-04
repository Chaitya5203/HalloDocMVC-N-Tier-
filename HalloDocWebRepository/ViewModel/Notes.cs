using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class Notes
    {
        public string AdminNotes { get; set; }
        public string phyNotes { get; set; }
        public int req_id { get; set; }
        public List<string> Transfernote { get; set; }
    }
}
