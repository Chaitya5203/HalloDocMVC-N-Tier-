namespace HalloDocWebRepository.ViewModel
{
    public class BusinessPatientRequest
    {
        public string first_name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string phone { get; set; } = null!;
        public string business_name { get; set; } = string.Empty;
        public string case_no { get; set; } = null!;
        public string symptoms { get; set; } = null!;
        public string p_first_name { get; set; } = null!;
        public string p_last_name { get; set; } = null!;
        public DateTime dob { get; set; } 
        public string p_email { get; set; } = null!;
        public string p_phone { get; set; } = null!;
        public string p_street { get; set; } = null!;
        public string p_city { get; set; } = null!;
        public string p_state { get; set; } = null!;
        public string p_zip_code { get; set; } = null!;
        public DateTime Createddate { get; set; } = DateTime.Now;
        public string? password { get; set; }
        public string? cpassword { get; set; }
    }
}
