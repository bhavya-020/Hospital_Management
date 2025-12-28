using Dapper;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Hospital_Management.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly string _con;

        public AppointmentController(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("dbcon");
        }

        // INDEX: List all appointments
        public IActionResult Index()
        {
            using (var db = new SqlConnection(_con))
            {
                var appointments = db.Query<AppointmentModel>(@"
                   SELECT A.AppointmentId,
                          D.DoctorName,
                          P.PatientName,
                          A.AppointmentDate,
                          A.AppointmentTime
                   FROM Appointments A
                   JOIN Doctors D ON A.DoctorId = D.DoctorId
                   JOIN Patients P ON A.PatientId = P.PatientId
                ").ToList();

                return View(appointments);
            }
        }

        // CREATE: Show form
        public IActionResult Create()
        {
            using (var db = new SqlConnection(_con))
            {
                ViewBag.Doctors = db.Query<DoctorModel>("SELECT * FROM Doctors").ToList();
                ViewBag.Patients = db.Query<PatientModel>("SELECT * FROM Patients").ToList();
            }

            return View();
        }

        // CREATE: Save form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppointmentModel m)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SqlConnection(_con))
                {
                    ViewBag.Doctors = db.Query<DoctorModel>("SELECT * FROM Doctors").ToList();
                    ViewBag.Patients = db.Query<PatientModel>("SELECT * FROM Patients").ToList();
                }
                return View(m);
            }

            using (var db = new SqlConnection(_con))
            {
                db.Execute(@"
                   INSERT INTO Appointments
                   (DoctorId, PatientId, AppointmentDate, AppointmentTime)
                   VALUES (@DoctorId, @PatientId, @AppointmentDate, @AppointmentTime)
                ", m);
            }

            return RedirectToAction(nameof(Index));
        }

        // EDIT: Show edit form
        public IActionResult Edit(int id)
{
    using var db = new SqlConnection(_con);

    ViewBag.Doctors = db.Query<DoctorModel>("SELECT * FROM Doctors").ToList();
    ViewBag.Patients = db.Query<PatientModel>("SELECT * FROM Patients").ToList();

    var appointment = db.QueryFirstOrDefault<AppointmentModel>(
        "SELECT * FROM Appointments WHERE AppointmentId = @id",
        new { id });

    if (appointment == null)
        return NotFound();

    return View(appointment);
}


        // EDIT: Save changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AppointmentModel m)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SqlConnection(_con))
                {
                    ViewBag.Doctors = db.Query<DoctorModel>("SELECT * FROM Doctors").ToList();
                    ViewBag.Patients = db.Query<PatientModel>("SELECT * FROM Patients").ToList();
                }
                return View(m);
            }

            using (var db = new SqlConnection(_con))
            {
                db.Execute(@"
                   UPDATE Appointments SET
                   DoctorId = @DoctorId,
                   PatientId = @PatientId,
                   AppointmentDate = @AppointmentDate,
                   AppointmentTime = @AppointmentTime
                   WHERE AppointmentId = @AppointmentId
                ", m);
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE: Delete appointment
        public IActionResult Delete(int id)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Execute("DELETE FROM Appointments WHERE AppointmentId = @id", new { id });
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
