
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace HalloDocWebRepository.ViewModel
{
    public class Userdata
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string first_name { get; set; } = null!;
        [Required(ErrorMessage = "Last Name is required.")]
        public string? last_name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Number is required.")]
        public string? phonenumber { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        public string? street { get; set; }
        [Required(ErrorMessage = "dob is required.")]
        public DateOnly dob { get; set; }
        [Required(ErrorMessage = "room Number is required.")]
        public string? room { get; set; }
        [Required(ErrorMessage = "city name is required.")]
        public string? city { get; set; }
        [Required(ErrorMessage = "state name is required.")]
        public string? state { get; set; }
        [Required(ErrorMessage = "zipcode is required.")]
        public string? zipcode { get; set; }
        [Required(ErrorMessage = "create date is required.")]
        public DateTime Createddate { get; set; } = DateTime.Now;
        public string? password { get; set; }
        public string? cpassword { get; set; }
        public IFormFile? File { get; set; }
    }
}