using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class login
    {
        [Required(ErrorMessage = "Please Enter First Name.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Password")]
        public string? Passwordhash { get; set; }
    }
}