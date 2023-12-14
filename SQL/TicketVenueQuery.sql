--use master;
--if exists (select * from sys.databases where name='ticketVenue')
--	drop database ticketVenue;
--go

--create database ticketVenue;
--go
use ticketVenue;
-- create tables
create table ZipCity(
	zip varchar(16) not null,
	city varchar(20) not null,
	primary key (zip)
);

create table Address(
	id int IDENTITY(1,1) not null,
	street varchar(16) not null,
	houseNo varchar(10) not null,
	zip_FK varchar(16) not null,
	primary key (id),
	foreign key (zip_FK) references ZipCity(zip)
);

create table Person(
	id int IDENTITY (1,1) not null,
	firstName varchar(12) not null,
	lastName varchar(20) not null,
	addressId_FK int not null,
	phone varchar(16) not null,	
	dateOfBirth dateTime not null,
	email nvarchar(256) unique not null,
	aspNetUsersId_FK nvarchar(450),
	isAdmin bit not null,
	type char not null,
	primary key (email),
	foreign key (addressId_FK) references Address(id),
	foreign key (aspNetUsersId_FK) references AspNetUsers(Id)
);

create table Users(
	userId varchar(16) unique not null,
	email_FK nvarchar(256) not null,
	primary key (userId),
	foreign key (email_FK) references Person (email)
);

create table EventOrganizer(
	organizerId varchar(16) unique not null,
	email_FK nvarchar(256) not null,
	primary key (organizerId),
	foreign key (email_FK) references Person (email)
);

create table Hall(
	hallNumber varchar(5) not null,
	primary key(hallNumber)
);

create table VenueEvent(
	id int IDENTITY(1,1) unique not null,
	venueEvent_ID varchar(20) unique not null,
	price float not null,
	eventName varchar(40) not null,
	startDate datetime not null,
	endDate datetime not null,
	hallNumber_FK varchar(5) not null,
	organizerId_FK varchar(16),
	primary key (id),
	foreign key (hallNumber_FK)references Hall(hallNumber),
	foreign key (organizerId_FK) references EventOrganizer(organizerId)
);

create table Seat(
	seatNumber varchar(20) not null,
	isInOrder bit not null,
	hallNumber_FK varchar(5) not null,
	primary key (seatNumber),
	unique (seatNumber),
	foreign key (hallNumber_FK)references Hall(hallNumber)
);

create table Ticket(
	id int IDENTITY(1,1) not null,
	ticket_ID varchar(20) unique not null,
	startDate datetime not null,
	endDate datetime not null,
	venueEventID_FK int not null,
	userID_FK varchar(16) not null,
	seatNumber_FK varchar(20) not null,
	primary key (id),
	unique (id),
	foreign key (venueEventID_FK) references VenueEvent (id),
	foreign key (userID_FK) references Users (userId),
	foreign key (seatNumber_FK) references Seat (seatNumber)
);