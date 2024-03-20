using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class FamilyFriendPatientRequest
    {
        [Required(ErrorMessage = "Family Member First Name is Required.")]
        public string f_first_name { get; set; } = null!;
        [Required(ErrorMessage = "Family Member Last Name is Required.")]
        public string f_last_name { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [RegularExpression(@"^\+[0-9\-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]

        public string f_phone_number { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string f_email { get; set; } = null!;
        [Required(ErrorMessage = "Patient's BirthDate is Required.")]
        public DateOnly dob { get; set; }
        public DateTime Createddate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Patient's First Name is Required.")]
        public string p_first_name { get; set; } = null!;
        [Required(ErrorMessage = "Patient's Last Name is Required.")]
        public string p_last_name { get; set; } = null!;
        [Required(ErrorMessage = "Patient's Email is Required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string p_email { get; set; } = null!;
        [Required(ErrorMessage = "Patient's Mobile Number is Required.")]
        [RegularExpression(@"^\+[0-9\-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string p_phonenumber { get; set; } = null!;
        [Required(ErrorMessage = "Street is Required.")]
        public string p_street { get; set; } = null!;
        [Required(ErrorMessage = "City is Required.")]
        public string p_city { get; set; } = null!;
        [Required(ErrorMessage = "State is Required.")]
        public string p_state { get; set; } = null!;
        [Required(ErrorMessage = "Zip Code is Required.")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Invalid Zip")]
        public string p_zip { get; set; } = null!;
        [Required(ErrorMessage = "Please Upload the Document")]
        public IFormFile? File { get; set; }
    }
}
