using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdt_assignment.model;

namespace wdt_assignment.Option
{
    class SearchCineplexMovie : IOption
    {
        public string GetOption()
        {
            return "Search by Cineplex OR movie ";
        }
        public void Selected()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Search by Cineplex");
            Console.WriteLine("2 - Search by Movie");
            int index = 0;
            try
            {
                index = Program.EnterOption();
                if (index == 1)
                {
                    SearchByCineplex();
                    return;
                }
                if (index == 2)
                {
                    SearchByMovie();
                    return;
                }
                throw new SystemException();
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 1 or 2");
            }
        }

        private void SearchByCineplex()
        {
            Console.WriteLine("Search by Cineplex");
            Console.Write("\nEnter a cineplex name: ");

            List<Cineplex> cineplexs = CinemaModel.Instance.SearchCinplex(Console.ReadLine());

            DisplayCineplexList.DisplayCineplexs(cineplexs);
        }

        private void SearchByMovie()
        {
            Console.WriteLine("Search by Movie");
            Console.Write("\nEnter a movie name: ");

            List<Session> sessions = SessionModel.Instance.SearchMovie(Console.ReadLine());
            DisplayCineplexList.DisplaySchedules(sessions, true);
        }
    }
}
