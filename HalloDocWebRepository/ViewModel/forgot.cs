using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class forgot
    {
        [Required(ErrorMessage = "Please Enter First Name.")]
        public string email { get; set; } = null!;
    }
}
