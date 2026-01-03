using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        // For Index (JOIN result)


        public string? DoctorName { get; set; } = "";
        public string? PatientName { get; set; } = "";
    }
}
