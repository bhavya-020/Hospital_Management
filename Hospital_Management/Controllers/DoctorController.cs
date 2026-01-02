using Dapper;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            var model = new DoctorModel
            {
                SpecializationList = GetSpecializations()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoctorModel m)
        {
            if (!ModelState.IsValid)
            {
                    m.SpecializationList = GetSpecializations(); // VERY IMPORTANT

                return View(m); // Return form with validation errors
            }

            //if (m.Specialization == "Other")
            //{
            //    if (string.IsNullOrWhiteSpace(m.OtherSpecialization))
            //    {
            //        ModelState.AddModelError("OtherSpecialization", "Please enter specialization");
            //        m.SpecializationList = GetSpecializations();
            //        return View(m);
            //    }

            //    m.Specialization = m.OtherSpecialization;
            //}

            try
            {
                using (var db = new SqlConnection(_con))
                {
                    var param = new DynamicParameters();
                    param.Add("@DoctorName", m.DoctorName);
                    param.Add("@Specialization", m.Specialization);
                    param.Add("@WorkPlace", m.WorkPlace);
                    param.Add("@Experience", m.Experience);

                    db.Execute(
                        "sp_Doctor_Insert",
                        param,
                        commandType: CommandType.StoredProcedure
                    );
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                m.SpecializationList = GetSpecializations();

                return View(m);
            }
        }


        private List<SelectListItem> GetSpecializations()
        {
            return new List<SelectListItem>
    {
        new SelectListItem { Text = "Cardiologist", Value = "Cardiologist" },
        new SelectListItem { Text = "Dermatologist", Value = "Dermatologist" },
        new SelectListItem { Text = "Neurologist", Value = "Neurologist" },
        new SelectListItem { Text = "Orthopedic", Value = "Orthopedic" },
        new SelectListItem { Text = "Pediatrician", Value = "Pediatrician" },
        new SelectListItem { Text = "Other", Value = "Other" }

    };
        }

        public IActionResult Delete(int id)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Execute(
                    "sp_Doctor_Delete",
                    new { DoctorId = id },
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
                doctor.SpecializationList = GetSpecializations();

                return View(doctor);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DoctorModel m)
        {

            if (!ModelState.IsValid)
            {
                m.SpecializationList = GetSpecializations();
                return View(m);
            }


            //if (m.Specialization == "Other")
            //{
            //    if (string.IsNullOrWhiteSpace(m.OtherSpecialization))
            //    {
            //        ModelState.AddModelError("OtherSpecialization", "Please enter specialization");
            //        m.SpecializationList = GetSpecializations();
            //        return View(m);
            //    }

            //    m.Specialization = m.OtherSpecialization;
            //}


            try
            {
                using var db = new SqlConnection(_con);

                var param = new DynamicParameters();
                param.Add("@DoctorId", m.DoctorId);
                param.Add("@DoctorName", m.DoctorName);
                param.Add("@Specialization", m.Specialization);
                param.Add("@WorkPlace", m.WorkPlace);
                param.Add("@Experience", m.Experience);

                db.Execute(
                    "sp_Doctor_Update",
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                m.SpecializationList = GetSpecializations();

                return View(m);
            }

        }
    }
}
