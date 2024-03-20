using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class BusinessPatientRequest
    {
        [Required(ErrorMessage = "Please Enter your First Name.")]
        public string first_name { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter your Last Name.")]
        public string last_name { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter your Email.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string email { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter your Phone Number")]
        [RegularExpression(@"^\+[0-9\-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string phone { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter your Business Name.")]
        public string business_name { get; set; } = string.Empty;
        public string case_no { get; set; } = null!;
        public string symptoms { get; set; } = null!;
        [Required(ErrorMessage = "Patient's First Name is Required.")]
        public string p_first_name { get; set; } = null!;
        [Required(ErrorMessage = "Patient's Last Name is Required.")]
        public string p_last_name { get; set; } = null!;
        [Required(ErrorMessage = "Birth Date is Required.")]
        public DateTime dob { get; set; }
        [Required(ErrorMessage = "Patient's Email is Required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string p_email { get; set; } = null!;
        [Required(ErrorMessage = "Patient's Mobile Number is Required.")]
        [RegularExpression(@"^\+[0-9\-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string p_phone { get; set; } = null!;
        [Required(ErrorMessage = "Street is Required.")]
        public string p_street { get; set; } = null!;
        [Required(ErrorMessage = "City is Required.")]
        public string p_city { get; set; } = null!;
        [Required(ErrorMessage = "State is Required.")]
        public string p_state { get; set; } = null!;
        [Required(ErrorMessage = "Zip Code is Required.")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Invalid Zip")]
        public string p_zip_code { get; set; } = null!;
        public DateTime Createddate { get; set; } = DateTime.Now;
        public string? password { get; set; }
        public string? cpassword { get; set; }
    }
}
