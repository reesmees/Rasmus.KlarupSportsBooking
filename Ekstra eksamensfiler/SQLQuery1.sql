CREATE TABLE [E-mails] (
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	[E-mailAddress] varchar(50) NOT NULL
);

CREATE TABLE Addresses (
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	StreetName varchar(100) NOT NULL,
	HouseNumber int NOT NULL,
	[Floor] int NOT NULL,
	ZipCode int NOT NULL,
	City varchar(100) NOT NULL
);
GO

CREATE TABLE Unions (
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	[E-mailID] int FOREIGN KEY REFERENCES [E-mails](ID) NOT NULL,
	AddressID int FOREIGN KEY REFERENCES [Addresses](ID) NOT NULL,
	UnionName varchar(255) NOT NULL
);
GO

CREATE TABLE UnionLeaders(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	UnionID int FOREIGN KEY REFERENCES Unions(ID) NOT NULL,
	[Name] varchar(100) NOT NULL,
	PhoneNumber varchar(20) NOT NULL
);

CREATE TABLE Activities(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	ActivityName varchar(100) NOT NULL,
	HallUsage varchar(50) NOT NULL
);

CREATE TABLE Administrators(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	[E-mailID] int FOREIGN KEY REFERENCES [E-mails](ID) NOT NULL,
	[Name] varchar(100) NOT NULL,
	[Password] varchar(100) NOT NULL
);
GO

CREATE TABLE Reservations(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	UnionID int FOREIGN KEY REFERENCES Unions(ID) NOT NULL,
	ActivityID int FOREIGN KEY REFERENCES Activities(ID) NOT NULL,
	[Date] date NOT NULL,
	ReservationLength int NOT NULL,
	IsHandled bit NOT NULL
);

CREATE TABLE RecurringReservations(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	UnionID int FOREIGN KEY REFERENCES Unions(ID) NOT NULL,
	ActivityID int FOREIGN KEY REFERENCES Activities(ID) NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	[Weekday] varchar(50) NOT NULL,
	StartTime time NOT NULL,
	EndTime time NOT NULL,
	IsHandled bit NOT NULL
);
GO

CREATE TABLE Booking(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	AdministratorID int FOREIGN KEY REFERENCES Administrators(ID) NOT NULL,
	ReservationID int FOREIGN KEY REFERENCES Reservations(ID) NOT NULL,
	StartTime time NOT NULL
);

CREATE TABLE RecurringBooking(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	AdministratorID int FOREIGN KEY REFERENCES Administrators(ID) NOT NULL,
	RecurringReservationID int FOREIGN KEY REFERENCES RecurringReservations(ID) NOT NULL
);
GO