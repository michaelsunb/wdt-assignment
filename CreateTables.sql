create table Cineplex
(
	CineplexID int not null identity primary key,
	Location nvarchar(max) not null,
	ShortDescription nvarchar(max) not null,
	LongDescription nvarchar(max) not null,
	ImageUrl nvarchar(max) null
);

create table Enquiry
(
	EnquiryID int not null identity primary key,
	Email nvarchar(max) not null,
	Message nvarchar(max) not null,
    [Status] [int] not NULL
);

create table MovieComingSoon
(
	MovieComingSoonID int not null identity primary key,
	Title nvarchar(max) not null,
	ShortDescription nvarchar(max) not null,
	LongDescription nvarchar(max) not null,
	ImageUrl nvarchar(max) null
);

create table Movie
(
	MovieID int not null identity primary key,
	Title nvarchar(max) not null,
	ShortDescription nvarchar(max) not null,
	LongDescription nvarchar(max) not null,
	ImageUrl nvarchar(max) null,
	Price money not null,
    [Status] [int] not NULL
);

create table CineplexMovie
(
	CineplexMovieID int not null identity primary key,
	CineplexID int not null foreign key references Cineplex (CineplexID),
	MovieID int not null foreign key references Movie (MovieID)
);

create table Seating
(
	CineplexMovieID int not null foreign key references CineplexMovie (CineplexMovieID),
	primary key (CineplexMovieID),
	SeatRow nvarchar(max) not null,
	SeatColumn nvarchar(max) not null,
	[extra] [nchar](10) NULL
);
