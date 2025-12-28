using Dapper;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Hospital_Management.Controllers
{
    public class DoctorController : Controller
    {
        private readonly string _con;

        public DoctorController(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("dbcon");
        }

        public IActionResult Index()
        {
            using (var db = new SqlConnection(_con))
            {
                var doctors = db
                    .Query<DoctorModel>("SELECT * FROM Doctors")
                    .ToList();

                return View(doctors);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DoctorModel m)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    @"INSERT INTO Doctors
                      (DoctorName, Specialization, WorkPlace, Experience)
                      VALUES
                      (@DoctorName, @Specialization, @WorkPlace, @Experience)",
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
                    "DELETE FROM Doctors WHERE DoctorId = @id",
                    new { id }
                );
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            using (var db = new SqlConnection(_con))
            {
                var doctor = db.QueryFirstOrDefault<DoctorModel>(
                    "SELECT * FROM Doctors WHERE DoctorId = @id",
                    new { id }
                );

                if (doctor == null)
                {
                    return NotFound();
                }

                return View(doctor);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DoctorModel m)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    @"UPDATE Doctors SET
                DoctorName = @DoctorName,
                Specialization = @Specialization,
                WorkPlace = @WorkPlace,
                Experience = @Experience
              WHERE DoctorId = @DoctorId",
                    m
                );
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
