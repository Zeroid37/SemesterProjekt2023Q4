insert into ZipCity values ('9000', 'Aalborg')
insert into Address values ('Vejvej', '5', '9000')

insert into Person values('Smajo', 'Omanovic', 1, '123456789', GETDATE(), 'Smajo@Mail', 'password', 0, 'e')
insert into Person values('Kasper', 'Christiansen', 1, '0987654321', GETDATE(), 'Kasper@mail', 'drowssap', 1, 'u')
insert into Users values('420', 'Kasper@mail')
insert into EventOrganizer values('69', 'Smajo@mail')

insert into Hall values ('1');
insert into Hall values ('2');
insert into Hall values ('3');
insert into Hall values ('4');
insert into Hall values ('5');

insert into VenueEvent values ('1', 199.95, 'Pop', CAST(GETDATE() AS date), CAST(GETDATE()+5 AS date), '1', '69');
insert into VenueEvent values ('2', 199.95, 'Rock', CAST(GETDATE() AS date), CAST(GETDATE()+5 AS date), '2', '69');
insert into VenueEvent values ('3', 199.95, 'Dak', CAST(GETDATE() AS date), CAST(GETDATE()+5 AS date), '3', '69');
insert into VenueEvent values ('4', 199.95, 'Jazz', CAST(GETDATE() AS date), CAST(GETDATE()+5 AS date), '4', '69');

insert into Seat values('S1', 1, 1)
insert into Seat values('S2', 1, 1)
insert into Seat values('S3', 1, 1)
insert into Seat values('S4', 1, 1)
insert into Seat values('S5', 1, 1)
insert into Seat values('S6', 1, 1)

insert into Seat values('S7', 1, 2)
insert into Seat values('S8', 1, 2)
insert into Seat values('S9', 1, 2)
insert into Seat values('S10', 1, 2)
insert into Seat values('S11', 1, 2)
insert into Seat values('S12', 1, 2)

insert into Seat values('S13', 1, 3)
insert into Seat values('S14', 1, 3)
insert into Seat values('S15', 1, 3)
insert into Seat values('S16', 1, 3)
insert into Seat values('S17', 1, 3)
insert into Seat values('S18', 1, 3)

insert into Seat values('S19', 1, 4)
insert into Seat values('S20', 1, 4)
insert into Seat values('S21', 1, 4)
insert into Seat values('S22', 1, 4)
insert into Seat values('S23', 1, 4)
insert into Seat values('S24', 1, 4)

