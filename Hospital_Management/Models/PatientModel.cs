using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class PatientModel
    {
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Patient Name is required")]
        [StringLength(50, ErrorMessage = "Patient Name cannot exceed 50 characters")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(0, 80, ErrorMessage = "Age must be between 0 and 80")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Contact { get; set; }
    }

}
