using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class PatientModel
    {
        public int PatientId { get; set; }


        [Required(ErrorMessage = "Patient Name is required")]
        [StringLength(30, ErrorMessage = "Patient Name cannot exceed 30 characters")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Doctor Name must contain only alphabets")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(0, 100, ErrorMessage = "Age must be between 1 and 100")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Contact is required")]
        //[StringLength(10, ErrorMessage = "Number must be between 0 and 10")]
        [RegularExpression(@"^[6-9]\d{9}$",
            ErrorMessage = "Contact number must be 10 digits and start with 6, 7, 8, or 9")]
        public string Contact { get; set; }
    }

}
