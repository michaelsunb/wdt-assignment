declare @stKilda int;
insert into Cineplex (Location, ShortDescription, LongDescription, ImageUrl, [Status]) 
values ('St Kilda', 'Short description...', 'Long description...', 'StKilda.png', 1);
set @stKilda = SCOPE_IDENTITY();

declare @fitzroy int;
insert into Cineplex (Location, ShortDescription, LongDescription, ImageUrl, [Status]) 
values ('Fitzroy', 'Short description...', 'Long description...', 'Fitzroy.png', 1);
set @fitzroy = SCOPE_IDENTITY();

declare @melbourneCBD int;
insert into Cineplex (Location, ShortDescription, LongDescription, ImageUrl, [Status]) 
values ('Melbourne CBD', 'Short description...', 'Long description...', 'MelbourneCBD.png', 1);
set @melbourneCBD = SCOPE_IDENTITY();

declare @sunshine int;
insert into Cineplex (Location, ShortDescription, LongDescription, ImageUrl, [Status]) 
values ('Sunshine', 'Short description...', 'Long description...', 'Sunshine.png', 1);
set @sunshine = SCOPE_IDENTITY();

declare @lilydale int;
insert into Cineplex (Location, ShortDescription, LongDescription, ImageUrl, [Status]) 
values ('Lilydale', 'Short description...', 'Long description...', 'Lilydale.png', 1);
set @lilydale = SCOPE_IDENTITY();

insert into MovieComingSoon (Title, ShortDescription, LongDescription, ImageUrl)
values ('ASP.NET MVC 101', 'Short description...', 'Long description...', 'MovieComingSoon.png');

insert into MovieComingSoon (Title, ShortDescription, LongDescription, ImageUrl)
values ('WebForms Legacy', 'Short description...', 'Long description...', 'MovieComingSoon.png');

declare @theMatrix int;
insert into Movie (Title, ShortDescription, LongDescription, ImageUrl, Price, Status)
values ('The Matrix', 'Short description...', 'Long description...', 'TheMatrix.jpg', 10.00, 1);
set @theMatrix = SCOPE_IDENTITY();

declare @theMatrixReloaded int;
insert into Movie (Title, ShortDescription, LongDescription, ImageUrl, Price, Status)
values ('The Matrix Reloaded', 'Short description...', 'Long description...', 'TheMatrixReloaded.jpg', 15.00, 1);
set @theMatrixReloaded = SCOPE_IDENTITY();

declare @theMatrixRevolution int;
insert into Movie (Title, ShortDescription, LongDescription, ImageUrl, Price, Status)
values ('The Matrix Revolution', 'Short description...', 'Long description...', 'TheMatrixRevolution.jpg', 20.00, 1);
set @theMatrixRevolution = SCOPE_IDENTITY();

insert into CineplexMovie (CineplexID, MovieID) 
values (@stKilda, @theMatrix);

insert into CineplexMovie (CineplexID, MovieID) 
values (@stKilda, @theMatrixReloaded);

insert into CineplexMovie (CineplexID, MovieID) 
values (@stKilda, @theMatrixRevolution);

insert into CineplexMovie (CineplexID, MovieID) 
values (@fitzroy, @theMatrix);

insert into CineplexMovie (CineplexID, MovieID) 
values (@fitzroy, @theMatrixReloaded);

insert into CineplexMovie (CineplexID, MovieID) 
values (@melbourneCBD, @theMatrixRevolution);
