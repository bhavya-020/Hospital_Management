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


















/////SSSSSSSSPPPPPPPPP
//ALTER PROC sp_Doctor_Insert
//@DoctorId int,
// @DoctorName NVARCHAR(100),
// @Specialization NVARCHAR(100),
// @WorkPlace NVARCHAR(100),
// @Experience INT
//AS
//INSERT INTO Doctors
//VALUES (@DoctorName, @Specialization, @WorkPlace, @Experience)



//select * From doctors

//sp_Doctor_Insert 'Test Doctor','General','Delhi',4;
//sp_Appointment_Insert 11,6 , '2025-12-22' , '02:56:22';

//EXEC sp_Appointment_Insert
//    'DR. Ashish',
//    'Patient22',
//    '2025-12-22',
//    '02:56:22';

//////////////////////////////////////////////////////
//ALTER PROC sp_Patient_Insert
// @PatientId int,
// @PatientName NVARCHAR(100),
// @Age INT,
// @Gender NVARCHAR(10),
// @Contact NVARCHAR(15)
//AS
//INSERT INTO Patients VALUES(@PatientName, @Age, @Gender, @Contact);



//CREATE PROC sp_Doctor_Delete
// @DoctorId INT
//AS
//DELETE FROM Doctors WHERE DoctorId=@DoctorId


/////////////////////////////////////////////////

//ALTER PROC sp_Appointment_Update
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


////sp_Appointment_Insert

//Alter PROC sp_Appointment_Insert
// @DoctorId INT,
// @PatientId INT,
// @AppointmentDate DATE,
// @AppointmentTime TIME,
// @AppointmentId int,
// @DoctorName NVARCHAR(100),
// @PatientName NVARCHAR(100)
//AS
//INSERT INTO Appointments
//VALUES(@DoctorId, @PatientId, @AppointmentDate, @AppointmentTime);


//alter PROC sp_Appointment_Update
// @DoctorId INT,
// @PatientId INT,
// @AppointmentDate DATE,
// @AppointmentTime TIME,
// @AppointmentId INT,
// @DoctorName NVARCHAR(100),
// @PatientName NVARCHAR(100)
//AS
//UPDATE Appointments SET
// DoctorId=@DoctorId,
// PatientId = @PatientId,
// AppointmentDate = @AppointmentDate,
// AppointmentTime = @AppointmentTime
//WHERE AppointmentId=@AppointmentId







/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///




/////SSSSSSSSPPPPPPPPP

//CREATE PROCEDURE sp_Appointment_Insert_With_Check
//(
//    @DoctorId INT,
//    @PatientId INT,
//    @AppointmentDate DATE,
//    @AppointmentTime TIME,
//    @Result INT OUTPUT
//)
//AS
//BEGIN
//    SET NOCOUNT ON;


//IF EXISTS(
//        SELECT 1
//        FROM Appointments
//        WHERE DoctorId = @DoctorId
//          AND AppointmentDate = @AppointmentDate
//          AND AppointmentTime = @AppointmentTime
//    )
//    BEGIN
//        SET @Result = -1; --Doctor busy
//        RETURN;
//END


//IF EXISTS (
//        SELECT 1
//        FROM Appointments
//        WHERE PatientId = @PatientId
//          AND AppointmentDate = @AppointmentDate
//          AND AppointmentTime = @AppointmentTime
//    )
//    BEGIN
//        SET @Result = -2; --Patient busy
//        RETURN;
//END

//-- ✅ Insert appointment
//    INSERT INTO Appointments
//    (DoctorId, PatientId, AppointmentDate, AppointmentTime)
//    VALUES
//    (@DoctorId, @PatientId, @AppointmentDate, @AppointmentTime);

//SET @Result = 1; --Success
//END






//CREATE PROCEDURE sp_Appointment_Update_With_Check
//(
//    @AppointmentId INT,
//    @DoctorId INT,
//    @PatientId INT,
//    @AppointmentDate DATE,
//    @AppointmentTime TIME,
//    @Result INT OUTPUT
//)
//AS
//BEGIN
//    SET NOCOUNT ON;

//--Doctor busy check(ignore current appointment)
//    IF EXISTS(
//        SELECT 1 FROM Appointments
//        WHERE DoctorId = @DoctorId
//          AND AppointmentDate = @AppointmentDate
//          AND AppointmentTime = @AppointmentTime
//          AND AppointmentId <> @AppointmentId
//    )
//    BEGIN
//        SET @Result = -1; --Doctor busy
//        RETURN;
//END

//-- Patient busy check (ignore current appointment)
//    IF EXISTS (
//        SELECT 1 FROM Appointments
//        WHERE PatientId = @PatientId
//          AND AppointmentDate = @AppointmentDate
//          AND AppointmentTime = @AppointmentTime
//          AND AppointmentId <> @AppointmentId
//    )
//    BEGIN
//        SET @Result = -2; --Patient busy
//        RETURN;
//END

//-- Update
//UPDATE Appointments
//    SET
//        DoctorId = @DoctorId,
//    PatientId = @PatientId,
//    AppointmentDate = @AppointmentDate,
//    AppointmentTime = @AppointmentTime
//    WHERE AppointmentId = @AppointmentId;

//SET @Result = 1; --Success
//END




/////////////////////////////////////////////



//CREATE PROCEDURE sp_Appointment_GetById
//(
//    @AppointmentId INT
//)
//AS
//BEGIN
//    SET NOCOUNT ON;

//SELECT
//        A.AppointmentId,
//        A.DoctorId,
//        D.DoctorName,
//        A.PatientId,
//        P.PatientName,
//        A.AppointmentDate,
//        A.AppointmentTime
//    FROM Appointments A
//    INNER JOIN Doctors D ON A.DoctorId = D.DoctorId
//    INNER JOIN Patients P ON A.PatientId = P.PatientId
//    WHERE A.AppointmentId = @AppointmentId;
//END


///////////////////////////////////////////


//CREATE PROCEDURE sp_Appointment_GetAll_Filtered
//    @Search VARCHAR(100) = NULL,
//    @Status  VARCHAR(20)  = NULL,
//    @Page    INT,
//    @PageSize INT
//AS
//BEGIN
//    SET NOCOUNT ON;

//DECLARE @Today DATE = CAST(GETDATE() AS DATE);

//; WITH Filtered AS (
//        SELECT
//            a.AppointmentId,
//        a.DoctorId,
//        d.DoctorName,
//        a.PatientId,
//        p.PatientName,
//        a.AppointmentDate,
//        a.AppointmentTime
//        FROM Appointments a
//        INNER JOIN Doctors d ON a.DoctorId = d.DoctorId
//        INNER JOIN Patients p ON a.PatientId = p.PatientId
//        WHERE
//            (@Search      IS NULL
//             OR d.DoctorName LIKE '%' + @Search + '%'
//             OR p.PatientName LIKE '%' + @Search + '%')
//          AND (
//            @Status IS NULL
//            OR (@Status = 'today'     AND a.AppointmentDate = @Today)
//            OR (@Status = 'upcoming'  AND a.AppointmentDate >  @Today)
//            OR (@Status = 'completed' AND a.AppointmentDate <  @Today)
//          )
//    ),
//Paged AS (
//        SELECT *,
//           ROW_NUMBER() OVER (ORDER BY AppointmentDate, AppointmentTime) AS RowNum
//        FROM Filtered
//    )
//    SELECT
//          AppointmentId,
//      DoctorId,
//      DoctorName,
//      PatientId,
//      PatientName,
//      AppointmentDate,
//      AppointmentTime
//    FROM Paged
//    WHERE RowNum BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize;

//SELECT COUNT(*) AS TotalRecords FROM Filtered;
//END;


/* 

CREATE PROCEDURE sp_Appointment_Filter
(
    @Search     NVARCHAR(100) = NULL,
    @DoctorId   INT = NULL,
    @PatientId  INT = NULL,
    @FromDate   DATE = NULL,
    @ToDate     DATE = NULL,
    @Status     NVARCHAR(20) = NULL,
    @Page       INT = 1,
    @PageSize   INT = 10
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);

    SELECT
        A.AppointmentId,
        A.DoctorId,
        D.DoctorName,
        A.PatientId,
        P.PatientName,
        A.AppointmentDate,
        A.AppointmentTime
    FROM Appointments A
    JOIN Doctors D ON A.DoctorId = D.DoctorId
    JOIN Patients P ON A.PatientId = P.PatientId
    WHERE
        -- 🔍 SEARCH
        (
            @Search IS NULL OR
            D.DoctorName LIKE '%' + @Search + '%' OR
            P.PatientName LIKE '%' + @Search + '%'
        )

        -- 👨‍⚕️ DOCTOR FILTER
        AND (@DoctorId IS NULL OR A.DoctorId = @DoctorId)

        -- 🧑 PATIENT FILTER
        AND (@PatientId IS NULL OR A.PatientId = @PatientId)

        -- 📅 DATE RANGE
        AND (@FromDate IS NULL OR A.AppointmentDate >= @FromDate)
        AND (@ToDate IS NULL OR A.AppointmentDate <= @ToDate)

        -- 📌 STATUS FILTER
        AND (
            @Status IS NULL OR
            (@Status = 'today' AND A.AppointmentDate = @Today) OR
            (@Status = 'upcoming' AND A.AppointmentDate > @Today) OR
            (@Status = 'completed' AND A.AppointmentDate < @Today)
        )
    ORDER BY A.AppointmentDate DESC
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- 🔢 TOTAL COUNT (for pagination)
    SELECT COUNT(1)
    FROM Appointments A
    JOIN Doctors D ON A.DoctorId = D.DoctorId
    JOIN Patients P ON A.PatientId = P.PatientId
    WHERE
        (
            @Search IS NULL OR
            D.DoctorName LIKE '%' + @Search + '%' OR
            P.PatientName LIKE '%' + @Search + '%'
        )
        AND (@DoctorId IS NULL OR A.DoctorId = @DoctorId)
        AND (@PatientId IS NULL OR A.PatientId = @PatientId)
        AND (@FromDate IS NULL OR A.AppointmentDate >= @FromDate)
        AND (@ToDate IS NULL OR A.AppointmentDate <= @ToDate)
        AND (
            @Status IS NULL OR
            (@Status = 'today' AND A.AppointmentDate = @Today) OR
            (@Status = 'upcoming' AND A.AppointmentDate > @Today) OR
            (@Status = 'completed' AND A.AppointmentDate < @Today)
        );
END
 


---------------------------------------------------------------------------------------


 ALTER PROCEDURE sp_Doctor_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        DoctorId,
        DoctorName
    FROM Doctors
    ORDER BY DoctorName;
END

ALTER PROCEDURE sp_Patient_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        PatientId,
        PatientName
    FROM Patients
    ORDER BY PatientName;
END



 */