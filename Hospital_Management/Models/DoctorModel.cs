using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class DoctorModel
    {
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Doctor Name is required")]
        [StringLength(50)]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Workplace is required")]
        public string WorkPlace { get; set; }

        [Required(ErrorMessage = "Experience is required")]
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years")]
        public int Experience { get; set; }
    }
}
