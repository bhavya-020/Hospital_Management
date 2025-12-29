using Dapper;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


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
                    .Query<DoctorModel>("sp_Doctor_GetAll",
                    commandType: CommandType.StoredProcedure
                    )
                    .ToList();

                return View(doctors);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoctorModel m)
        {
            if (!ModelState.IsValid)
            {
                return View(m); // Return form with validation errors
            }

            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    @"sp_Doctor_Insert",
                    m,
                    commandType: CommandType.StoredProcedure
                );
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    "sp_Doctor_Delete",
                    new { id },
                    commandType: CommandType.StoredProcedure
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

            if (!ModelState.IsValid)
                return View(m);

            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    "sp_Doctor_Update",
                    m
                , commandType: CommandType.StoredProcedure);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
