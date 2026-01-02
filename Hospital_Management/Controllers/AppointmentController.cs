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

        // ===================== INDEX =====================
        public IActionResult Index(string status, string search, int page = 1)
        {
            int pageSize = 5;

            using var db = new SqlConnection(_con);

            var data = db.Query<AppointmentModel>(
                "sp_Appointment_GetAll",
                commandType: CommandType.StoredProcedure
            ).ToList();

            // SEARCH
            if (!string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(x =>
                    x.DoctorName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    x.PatientName.Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // STATUS FILTER
            var today = DateTime.Today;
            data = status switch
            {
                "today" => data.Where(x => x.AppointmentDate.Date == today).ToList(),
                "upcoming" => data.Where(x => x.AppointmentDate.Date > today).ToList(),
                "completed" => data.Where(x => x.AppointmentDate.Date < today).ToList(),
                _ => data
            };

            // PAGINATION
            int totalRecords = data.Count;
            ViewBag.TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.Status = status;
            ViewBag.Search = search;

            data = data
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(data);
        }

        // ===================== CREATE (GET) =====================
        public IActionResult Create()
        {
            using var db = new SqlConnection(_con);

            ViewBag.Doctors = db.Query<DoctorModel>(
                "sp_Doctor_GetAll",
                commandType: CommandType.StoredProcedure
            ).ToList();

            ViewBag.Patients = db.Query<PatientModel>(
                "sp_Patient_GetAll",
                commandType: CommandType.StoredProcedure
            ).ToList();

            return View();
        }

        // ===================== CREATE (POST) =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppointmentModel m)
        {
            using var db = new SqlConnection(_con);

            var parameters = new DynamicParameters();
            parameters.Add("@DoctorId", m.DoctorId);
            parameters.Add("@PatientId", m.PatientId);
            parameters.Add("@AppointmentDate", m.AppointmentDate);
            parameters.Add("@AppointmentTime", m.AppointmentTime);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            db.Execute(
                "sp_Appointment_Insert_With_Check",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            int result = parameters.Get<int>("@Result");

            if (result == -1)
                ModelState.AddModelError("", "This doctor is already booked.");
            else if (result == -2)
                ModelState.AddModelError("", "This patient already has an appointment.");

            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = db.Query<DoctorModel>(
                    "sp_Doctor_GetAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                ViewBag.Patients = db.Query<PatientModel>(
                    "sp_Patient_GetAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return View(m);
            }

            return RedirectToAction(nameof(Index));
        }

        // ===================== EDIT (GET) =====================
        public IActionResult Edit(int id)
        {
            using var db = new SqlConnection(_con);

            ViewBag.Doctors = db.Query<DoctorModel>(
                "sp_Doctor_GetAll",
                commandType: CommandType.StoredProcedure
            ).ToList();

            ViewBag.Patients = db.Query<PatientModel>(
                "sp_Patient_GetAll",
                commandType: CommandType.StoredProcedure
            ).ToList();

            var appointment = db.QueryFirstOrDefault<AppointmentModel>(
                "sp_Appointment_GetById",
                new { AppointmentId = id },
                commandType: CommandType.StoredProcedure
            );

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // ===================== EDIT (POST) =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AppointmentModel m)
        {
            if (!ModelState.IsValid)
            {
                using var db = new SqlConnection(_con);

                ViewBag.Doctors = db.Query<DoctorModel>(
                    "sp_Doctor_GetAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                ViewBag.Patients = db.Query<PatientModel>(
                    "sp_Patient_GetAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return View(m);
            }

            using var db2 = new SqlConnection(_con);

            var parameters = new DynamicParameters();
            parameters.Add("@AppointmentId", m.AppointmentId);
            parameters.Add("@DoctorId", m.DoctorId);
            parameters.Add("@PatientId", m.PatientId);
            parameters.Add("@AppointmentDate", m.AppointmentDate);
            parameters.Add("@AppointmentTime", m.AppointmentTime);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            db2.Execute(
                "sp_Appointment_Update_With_Check",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            int result = parameters.Get<int>("@Result");

            if (result == -1)
                ModelState.AddModelError("", "This doctor is already booked.");
            else if (result == -2)
                ModelState.AddModelError("", "This patient already has an appointment.");

            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = db2.Query<DoctorModel>(
                    "sp_Doctor_GetAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                ViewBag.Patients = db2.Query<PatientModel>(
                    "sp_Patient_GetAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return View(m);
            }

            return RedirectToAction(nameof(Index));
        }

        // ===================== DELETE =====================
        public IActionResult Delete(int id)
        {
            using var db = new SqlConnection(_con);

            db.Execute(
                "sp_Appointment_Delete",
                new { AppointmentId = id },
                commandType: CommandType.StoredProcedure
            );

            return RedirectToAction(nameof(Index));
        }
    }
}
