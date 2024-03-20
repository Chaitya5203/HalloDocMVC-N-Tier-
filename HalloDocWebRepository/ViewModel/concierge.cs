using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class ConciergePatientRequest
    {
        [Required(ErrorMessage = "Concierge First Name is Required.")]
        public string cname { get; set; } = null!;
        [Required(ErrorMessage = "Concierge Last Name is required.")]
        public string clname { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string cemail { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [RegularExpression(@"^\+[0-9\-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]

        public string cphone { get; set; } = null!;
        [Required(ErrorMessage = "Street is required.")]
        public string cstreet { get; set; } = null!;
        [Required(ErrorMessage = "city is required.")]
        public string ccity { get; set; } = null!;
        [Required(ErrorMessage = "state is required.")]
        public string cstate { get; set; } = null!;
        [Required(ErrorMessage = "zip code is required.")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Invalid Zip")]
        public string czip { get; set; } = null!;
        public int Regionid { get; set; } = 1;
        public DateTime Createddate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Patient's First Name is Required.")]
        public string first_name { get; set; } = null!;
        [Required(ErrorMessage = "Patient's last name is required.")]
        public string last_name { get; set; } = null!;
        [Required(ErrorMessage = "Patient's email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string pemail { get; set; } = null!;
        [Required(ErrorMessage = "Patient's Mobile Number is Required.")]
        [RegularExpression(@"^\+[0-9\-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]

        public string Phonenumber { get; set; } = null!;
        [Required(ErrorMessage = "Street is required.")]
        public string? Street { get; set; }

        public DateOnly dob { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }    
        public string? p_zip_code { get; set; }
    }
}
