using Dapper;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Hospital_Management.Controllers
{
    public class PatientController : Controller
    {
        private readonly string _con;

        // ✔ Modern configuration
        public PatientController(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("dbcon");
        }

        public IActionResult Index()
        {
            using (var db = new SqlConnection(_con))
            {
                var patients = db
                    .Query<PatientModel>("SELECT * FROM Patients")
                    .ToList();

                return View(patients);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientModel m)
        {

            if (!ModelState.IsValid)
                return View(m);

            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    @"INSERT INTO Patients
                      (PatientName, Age, Gender, Contact)
                      VALUES
                      (@PatientName, @Age, @Gender, @Contact)",
                    m
                );
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            using (var db = new SqlConnection(_con))
            {
                var patient = db.QueryFirstOrDefault<PatientModel>(
                    "SELECT * FROM Patients WHERE PatientId = @id",
                    new { id }
                );

                if (patient == null)
                {
                    return NotFound();
                }

                return View(patient);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientModel m)
        {
            if (!ModelState.IsValid)
                return View(m);

            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    @"UPDATE Patients SET
                        PatientName = @PatientName,
                        Age = @Age,
                        Gender = @Gender,
                        Contact = @Contact
                      WHERE PatientId = @PatientId",
                    m
                );
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    "DELETE FROM Patients WHERE PatientId = @id",
                    new { id }
                );
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
