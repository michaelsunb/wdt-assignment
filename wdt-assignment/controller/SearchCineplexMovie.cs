using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdt_assignment.model;

namespace wdt_assignment.Option
{
    class SearchCineplexMovie : BaseSessionOption, IOption
    {
        /// <summary>Returns a string "Search by Cineplex OR movie"</summary>
        /// <returns>Returns string "Search by Cineplex OR movie"</returns>
        public string GetOption()
        {
            return "Search by Cineplex OR movie ";
        }

        /// <summary>Selected method to display functionality.
        /// Displays 2 options. Search by Cineplex or by Movie.</summary>
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

        /// <summary>Method to display functionality.
        /// Displays search by Cineplex name.</summary>
        private void SearchByCineplex()
        {
            Console.WriteLine("Search by Cineplex");
            Console.Write("\nEnter a cineplex name: ");

            List<Cineplex> cineplexs = CineplexModel.Instance.SearchCinplex(Console.ReadLine());

            DisplayCineplexs(cineplexs);
        }

        /// <summary>Method to display functionality.
        /// Displays search by Movie title.</summary>
        private void SearchByMovie()
        {
            Console.WriteLine("Search by Movie");
            Console.Write("\nEnter a movie title: ");

            List<Session> sessions = SessionModel.Instance.SearchMovie(Console.ReadLine());
            DisplaySchedules(sessions, true);
        }
    }
}
