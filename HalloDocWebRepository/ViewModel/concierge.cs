using System.ComponentModel.DataAnnotations;

namespace HalloDocWebRepository.ViewModel
{
    public class ConciergePatientRequest
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string cname { get; set; } = null!;
        [Required(ErrorMessage = "Last Name is required.")]
        public string clname { get; set; } = null!;
        [Required(ErrorMessage = "Email is required.")]
        public string cemail { get; set; } = null!;
        [Required(ErrorMessage = "Phone Number is required.")]
        public string cphone { get; set; } = null!;
        [Required(ErrorMessage = "Street is required.")]
        public string cstreet { get; set; } = null!;
        [Required(ErrorMessage = "city is required.")]
        public string ccity { get; set; } = null!;
        [Required(ErrorMessage = "state name is required.")]
        public string cstate { get; set; } = null!;
        [Required(ErrorMessage = "zip code is required.")]
        public string czip { get; set; } = null!;
        [Required(ErrorMessage = "Zip Code is required.")]
        public int Regionid { get; set; } = 1;
        public DateTime Createddate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Patient First name is required.")]
        public string first_name { get; set; } = null!;
        [Required(ErrorMessage = "patient last name is required.")]
        public string last_name { get; set; } = null!;
        [Required(ErrorMessage = "patient email name is required.")]
        public string pemail { get; set; } = null!;
        [Required(ErrorMessage = "patient phone number name is required.")]
        public string Phonenumber { get; set; } = null!;
        public string? Street { get; set; }
        public DateOnly dob { get; set; }
        public string? City { get; set; } 
        public string? State { get; set; } 
        public string? Zip { get; set; }    
        public string? p_zip_code { get; set; }
    }
}
