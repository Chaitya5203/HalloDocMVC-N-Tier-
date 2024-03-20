
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace HalloDocWebRepository.ViewModel
{
    public class Userdata1 
    {
        [Required(ErrorMessage = "First Name is Required.")]
        public string first_name { get; set; } = null!;
        [Required(ErrorMessage = "Last Name is Required.")]
        public string? last_name { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string email { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [RegularExpression(@"^\+[0-9\-\(\)\/\.]{6,15}[0-9]$", ErrorMessage = "Please enter a valid phone number with country code.")]
        public string phonenumber { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        public string? street { get; set; }
        [Required(ErrorMessage = "Birth Date is Required.")]
        public DateOnly dob { get; set; }
        [Required(ErrorMessage = "room Number is required.")]
        public string? room { get; set; }
        [Required(ErrorMessage = "City is Required.")]
        public string? city { get; set; }
        [Required(ErrorMessage = "State is Required.")]
        public string? state { get; set; }
        [Required(ErrorMessage = "Zip Code is Required.")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Invalid Zip")]
        public string zipcode { get; set; }
        [Required(ErrorMessage = "create date is required.")]
        public DateTime Createddate { get; set; } = DateTime.Now;
    }
}