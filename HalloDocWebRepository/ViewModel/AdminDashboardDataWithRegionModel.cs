
using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class AdminDashboardDataWithRegionModel
    {
        public List<Physician> physicians { get; set; }
        public List<Region> regions { get; set; }
        public IQueryable<AdminDashboardTableModel> tabledata { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool PreviousPage { get; set; }
        public bool NextPage { get; set; }
        public string Email { get; set; }

    }
}
