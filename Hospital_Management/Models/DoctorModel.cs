using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class DoctorModel
    {
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Doctor Name is required")]
        [StringLength(30, ErrorMessage = "Doctor Name cannot exceed 30 characters")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Doctor Name must contain only alphabets")]

        public string DoctorName { get; set; }


        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(30, ErrorMessage = "Specialization cannot exceed 30 characters")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Doctor Name must contain only alphabets")]
        public string Specialization { get; set; }


        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only alphabets allowed")]
        //public string? OtherSpecialization { get; set; }


        [Required(ErrorMessage = "Workplace is required")]
        [StringLength(30, ErrorMessage = "Workplace cannot exceed 30 characters")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Workplace must contain only alphabets")]

        public string WorkPlace { get; set; }


        [Required(ErrorMessage = "Experience is required")]
        [Range(0, 70, ErrorMessage = "Experience must be between 0 and 70 years")]
        public int Experience { get; set; }
        public List<SelectListItem>? SpecializationList { get; set; }

    }
}
