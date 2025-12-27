using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

public class AppointmentController : Controller
{
    string con = ConfigurationManager.ConnectionStrings["dbcon"].ToString();

    public ActionResult Index()
    {
        using (SqlConnection db = new SqlConnection(con))
        {
            string q = @"select A.AppointmentId,D.DoctorName,
                         P.PatientName,A.AppointmentDate,A.AppointmentTime
                         from Appointments A
                         join Doctors D on A.DoctorId=D.DoctorId
                         join Patients P on A.PatientId=P.PatientId";
            return View(db.Query<AppointmentModel>(q).ToList());
        }
    }

    public ActionResult Create()
    {
        using (SqlConnection db = new SqlConnection(con))
        {
            ViewBag.Doctors = db.Query<DoctorModel>("select * from Doctors");
            ViewBag.Patients = db.Query<PatientModel>("select * from Patients");
        }
        return View();
    }

    [HttpPost]
    public ActionResult Create(AppointmentModel m)
    {
        using (SqlConnection db = new SqlConnection(con))
        {
            db.Execute(@"insert into Appointments
            values(@DoctorId,@PatientId,@AppointmentDate,@AppointmentTime)", m);
        }
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
        using (SqlConnection db = new SqlConnection(con))
        {
            ViewBag.Doctors = db.Query<DoctorModel>("select * from Doctors");
            ViewBag.Patients = db.Query<PatientModel>("select * from Patients");

            return View(db.QueryFirstOrDefault<AppointmentModel>(
             "select * from Appointments where AppointmentId=@id", new { id }));
        }
    }

    [HttpPost]
    public ActionResult Edit(AppointmentModel m)
    {
        using (SqlConnection db = new SqlConnection(con))
        {
            db.Execute(@"update Appointments set
   DoctorId=@DoctorId,
   PatientId=@PatientId,
   AppointmentDate=@AppointmentDate,
   AppointmentTime=@AppointmentTime
   where AppointmentId=@AppointmentId", m);
        }
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        using (SqlConnection db = new SqlConnection(con))
        {
            db.Execute("delete from Appointments where AppointmentId=@id", new { id });
        }
        return RedirectToAction("Index");
    }

}
