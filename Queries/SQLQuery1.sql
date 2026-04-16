CREATE TABLE SailClubMember(
	MemberId int NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(30),
	SurName NVARCHAR(20),
	PhoneNumber VARCHAR(11) NOT NULL,
	MemberAddress NVARCHAR(50),
	City NVARCHAR(30),
	Mail NVARCHAR(30) NOT NULL,
	MemberType int CHECK (MemberType >= 0),
	MemberRole int CHECK (MemberRole >= 0), 
	MemberImage NVARCHAR
);

CREATE TABLE ModelInfo(
	Model NVARCHAR(30) NOT NULL PRIMARY KEY,
	BoatType int CHECK (BoatType >= 0)
);

CREATE TABLE Boat(
	BoatId int NOT NULL PRIMARY KEY,
	Model NVARCHAR(30),
	SailNumber NVARCHAR(10) NOT NULL,
	EngineInfo NVARCHAR(20),
	Draft FLOAT,
	Width FLOAT,
	BoatLength FLOAT,
	YearOfConstruction VARCHAR(5),

	FOREIGN KEY (Model) REFERENCES ModelInfo (Model)
);

CREATE TABLE Booking(
	BookingId int NOT NULL PRIMARY KEY,
	StartDate DATE,
	EndDate DATE,
	SailCompleted BIT,
	Destination NVARCHAR(30),
	MemberId int,
	BoatId int,

	FOREIGN KEY (MemberId) REFERENCES SailClubMember (MemberId),
	FOREIGN KEY (BoatId) REFERENCES Boat (BoatId)
);

DROP TABLE Booking, Boat, ModelInfo, SailClubMember;