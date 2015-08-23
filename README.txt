Name: Michaelsun Baluyos
Student number: s3110401

Files I Created:
/wdt-assignment/
> Program.cs			- Main program is here. Run from here.
> Factory.cs			- 1st design pattern. The factory pattern.
> CustomCouldntFindException.cs	- Custom Exception Handler

/wdt-assignment/controller
> IOption.cs			- Interface for the factory pattern and polymorphism.
> BaseSessionOption.cs		- Abstract class for common functionality.
> DisplayCineplexList.cs	- Displays the process for 1.1 option 1.
> SearchCineplexMovie.cs	- Displays the process for 1.1 option 2.
> EditDeleteBooking.cs		- Displays the process for 1.1 option 3.
> Exit.cs			- Displays Terminating... and beeps.

/wdt-assignment/model
> CineplexModel.cs		- Singleton model for cineplex.
> JsonModel.cs			- Json model to read, write, and
				  load data into the singleton models.
> MovieModel.cs			- Singleton model for movie.
> SessionModel.cs		- Singleton model for sessions.

/wdt-assignment/testc
> TestJsonModel.cs		- Test case to create data.json file and
				  load sample data. Also used to test
				  json.net functionality
> TestLinqSearch.cs		- Test case to test linq searches.


Important folder:
/wdt-assignment/bin/Debug
> data.json			- Test case json sample is created here.


Patterns Used:
> Factory pattern.
> Singleton pattern.


References Added:
> Microsoft.VisualStudio.QualityTools.UnitTestFramework
> Newtonsoft.Json


Source Control:
> github.com (private)		- https://github.com/michaelsunb/wdt-assignment