----------------------------------------
-- SQL Create SailClub tables (Opg 1) --
----------------------------------------
---- Lukas, Kasper, Maria, Frederik ----
----------------------------------------

-- Reset
DROP TABLE IF EXISTS Booking
DROP TABLE IF EXISTS SailClubMember
DROP TABLE IF EXISTS Boat
CREATE TABLE SailClubMember(
    MemberId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    FirstName NVARCHAR(30) NOT NULL,
    SurName NVARCHAR(20) NOT NULL,
    PhoneNumber VARCHAR(11) NOT NULL UNIQUE,
    MemberAddress NVARCHAR(50),
    City NVARCHAR(30),
    Mail NVARCHAR(100) NOT NULL UNIQUE,
    MemberType int DEFAULT 0 CHECK (MemberType >= 0),
    MemberRole int DEFAULT 0 CHECK (MemberRole >= 0), 
    MemberImage NVARCHAR(MAX)
)
CREATE TABLE Boat(
    BoatId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Model NVARCHAR(30),
    SailNumber NVARCHAR(10) NOT NULL UNIQUE,
    EngineInfo NVARCHAR(20),
    Draft FLOAT,
    Width FLOAT,
    BoatLength FLOAT,
    YearOfConstruction VARCHAR(4),
    BoatType int DEFAULT 0 CHECK (BoatType >= 0)  
)
CREATE TABLE Booking(
    BookingId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    SailCompleted BIT DEFAULT 0,
    Destination NVARCHAR(30),
    MemberId int NOT NULL,
    BoatId int NOT NULL,
    FOREIGN KEY (MemberId) REFERENCES SailClubMember (MemberId),
    FOREIGN KEY (BoatId) REFERENCES Boat (BoatId),
    CONSTRAINT CHK_Dates CHECK (EndDate >= StartDate)
)
DELETE FROM Booking
DELETE FROM SailClubMember
DELETE FROM Boat
DBCC CHECKIDENT ('SailClubMember', RESEED, 0)
DBCC CHECKIDENT ('Boat', RESEED, 0)
DBCC CHECKIDENT ('Booking', RESEED, 0)
INSERT INTO SailClubMember (FirstName, SurName, PhoneNumber, MemberAddress, City, Mail, MemberType, MemberRole, MemberImage)
VALUES ('Poul', 'Henriksen', '12345678', 'Gaden 129', 'Roskilde', 'Poul@gmail.com', 0, 0, NULL)
INSERT INTO Boat (Model, SailNumber, EngineInfo, Draft, Width, BoatLength, YearOfConstruction, BoatType)
VALUES ('Model', '16-335', 'Is very good', 2.2, 3.3, 2.2, '2026', 2)
INSERT INTO Booking (StartDate, EndDate, SailCompleted, Destination, MemberId, BoatId)
VALUES ('2026-03-12', '2026-04-02', 0, 'Karrebæksminde', 1, 1)