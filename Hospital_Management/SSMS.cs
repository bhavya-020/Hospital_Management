//using Microsoft.AspNetCore.Http.HttpResults;
//using System.Runtime.Intrinsics.X86;

//create database HospitalDB;

//use HospitalDB;

//CREATE TABLE Doctors(
//    DoctorId INT IDENTITY PRIMARY KEY,
//    DoctorName NVARCHAR(100),
//    Specialization NVARCHAR(100),
//    WorkPlace NVARCHAR(100),
//    Experience INT
//);

//CREATE TABLE Patients(
//    PatientId INT IDENTITY PRIMARY KEY,
//    PatientName NVARCHAR(100),
//    Age INT,
//    Gender NVARCHAR(10),
//    Contact NVARCHAR(15)
//);

//CREATE TABLE Appointments(
//    AppointmentId INT IDENTITY PRIMARY KEY,
//    DoctorId INT,
//    PatientId INT,
//    AppointmentDate DATE,
//    AppointmentTime TIME,
//    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId),
//    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
//);

//select* from Doctors;
//select* from Appointments;
//select* from Patients;

//DELETE FROM Doctors WHERE DoctorId = 8;

/////////////////////////////////////////////////////////////
///SSSSSSSSPPPPPPPPP


//CREATE PROC sp_Doctor_Insert
// @DoctorName NVARCHAR(100),
// @Specialization NVARCHAR(100),
// @WorkPlace NVARCHAR(100),
// @Experience INT
//AS
//INSERT INTO Doctors
//VALUES (@DoctorName, @Specialization, @WorkPlace, @Experience)


//CREATE PROC sp_Doctor_GetAll
//AS
//SELECT * FROM Doctors


//CREATE PROC sp_Doctor_Update
// @DoctorId INT,
// @DoctorName NVARCHAR(100),
// @Specialization NVARCHAR(100),
// @WorkPlace NVARCHAR(100),
// @Experience INT
//AS
//UPDATE Doctors SET
// DoctorName=@DoctorName,
// Specialization = @Specialization,
// WorkPlace = @WorkPlace,
// Experience = @Experience
//WHERE DoctorId=@DoctorId

//CREATE PROC sp_Doctor_Delete
// @DoctorId INT
//AS
//DELETE FROM Doctors WHERE DoctorId=@DoctorId


////////////////////////////////////////////////////////////

//CREATE PROC sp_Patient_Insert
// @PatientName NVARCHAR(100),
// @Age INT,
// @Gender NVARCHAR(10),
// @Contact NVARCHAR(15)
//AS
//INSERT INTO Patients VALUES(@PatientName, @Age, @Gender, @Contact)


//CREATE PROC sp_Patient_GetAll
//AS SELECT * FROM Patients

//CREATE PROC sp_Patient_Update
// @PatientId INT,
// @PatientName NVARCHAR(100),
// @Age INT,
// @Gender NVARCHAR(10),
// @Contact NVARCHAR(15)
//AS
//UPDATE Patients SET
// PatientName=@PatientName,
// Age = @Age,
// Gender = @Gender,
// Contact = @Contact
//WHERE PatientId=@PatientId


//CREATE PROC sp_Patient_Delete
// @PatientId INT
//AS DELETE FROM Patients WHERE PatientId=@PatientId


/////////////////////////////////////////////////////////////

//CREATE PROC sp_Appointment_Insert
// @DoctorId INT,
// @PatientId INT,
// @AppointmentDate DATE,
// @AppointmentTime TIME
//AS
//INSERT INTO Appointments
//VALUES(@DoctorId, @PatientId, @AppointmentDate, @AppointmentTime)

//CREATE PROC sp_Appointment_GetAll
//AS
//SELECT A.AppointmentId, D.DoctorName, P.PatientName,
//A.AppointmentDate, A.AppointmentTime
//FROM Appointments A
//JOIN Doctors D ON A.DoctorId=D.DoctorId
//JOIN Patients P ON A.PatientId=P.PatientId


//CREATE PROC sp_Appointment_Update
// @AppointmentId INT,
// @DoctorId INT,
// @PatientId INT,
// @AppointmentDate DATE,
// @AppointmentTime TIME
//AS
//UPDATE Appointments SET
// DoctorId=@DoctorId,
// PatientId = @PatientId,
// AppointmentDate = @AppointmentDate,
// AppointmentTime = @AppointmentTime
//WHERE AppointmentId=@AppointmentId

//CREATE PROC sp_Appointment_Delete
// @AppointmentId INT
//AS
//DELETE FROM Appointments WHERE AppointmentId=@AppointmentId

