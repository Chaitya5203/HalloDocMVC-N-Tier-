using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class FamilyFriendPatientRequest
    {
        [Required(ErrorMessage = "Family / Friend  First Name is required.")]
        public string f_first_name { get; set; } = null!;
        [Required(ErrorMessage = "Family / Friend Last Name is required.")]
        public string f_last_name { get; set; } = null!;
        [Required(ErrorMessage = "Phone Number is required.")]
        public string f_phone_number { get; set; } = null!;
        [Required(ErrorMessage = "Email is required.")]
        public string f_email { get; set; } = null!;
        public DateOnly dob { get; set; }
        public DateTime Createddate { get; set; }= DateTime.Now;
        [Required(ErrorMessage = "patient First Name is required.")]
        public string p_first_name { get; set; } = null!;
        [Required(ErrorMessage = "patient Last Name is required.")]
        public string p_last_name { get; set; } = null!;
        [Required(ErrorMessage = "Email is required.")]
        public string p_email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Number is required.")]
        public string p_phonenumber { get; set; } = null!;
        [Required(ErrorMessage = "street name is required.")]
        public string p_street { get; set; } = null!;
        [Required(ErrorMessage = "city name is required.")]
        public string p_city { get; set; } = null!;
        [Required(ErrorMessage = "state name is required.")]
        public string p_state { get; set; } = null!;
        [Required(ErrorMessage = "zip code is required.")]
        public string p_zip { get; set; } = null!;
        public IFormFile? File { get; set; }
    }
}
