

//CREATE DATABASE HospitalDB;
//GO
//USE HospitalDB;

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
