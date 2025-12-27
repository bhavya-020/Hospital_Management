namespace Hospital_Management.Models
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        public string DoctorName { get; set; }
        public string PatientName { get; set; }
    }

}
