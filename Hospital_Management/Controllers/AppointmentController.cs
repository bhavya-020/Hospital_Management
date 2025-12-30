using Dapper;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

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
                var appointments = db
                    .Query<AppointmentModel>("sp_Appointment_GetAll",
                   commandType: CommandType.StoredProcedure
                   )
                   .ToList();



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
                db.Execute(@"sp_Appointment_Insert", m,
                   commandType: CommandType.StoredProcedure
                   );
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
                db.Execute(
                    "sp_Appointment_Update",
                    m
                   , commandType: CommandType.StoredProcedure);
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE: Delete appointment
        public IActionResult Delete(int id)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Execute(@"sp_Appointment_Delete", 
                    new { AppointmentId = id },
                    commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
