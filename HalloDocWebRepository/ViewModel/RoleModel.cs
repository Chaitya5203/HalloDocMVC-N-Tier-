using System.ComponentModel.DataAnnotations;
using HalloDocWebRepository.Data;
namespace HalloDocWebRepository.ViewModel
{
    public class RoleModel
    {
        public string? RoleName {  get; set; }
        public List<int> RoleIds { get; set; }  
        public List<Rolemenu> rolemenus { get; set; }
        public List<Menu> menu {  get; set; }
        public int? SelectedRole { get; set; }
        public int RoleId { get; set; }
        public List<int> SelectedReg {  get; set; }
    }
}