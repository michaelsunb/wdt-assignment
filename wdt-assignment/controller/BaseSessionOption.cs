using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdt_assignment.model;

namespace wdt_assignment.Option
{
    abstract class BaseSessionOption
    {
        /// <summary>Selected method to display functionality.
        /// Displays list of cineplex from parameter.</summary>
        /// <param name="cineplexs"> parameter takes a struct Cineplex.</param>
        public void DisplayCineplexs(List<Cineplex> cineplexs)
        {
            Console.WriteLine();

            int i = 0;
            foreach (Cineplex cineplex in cineplexs)
            {
                Console.WriteLine(++i + " - {0}", cineplex.cineplexName);
            }
            int index = 0;
            Cineplex newCineplex;
            try
            {
                index = Program.EnterOption();
                newCineplex = cineplexs[--index];
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 1 to " + i);
            }
            DisplayDays(newCineplex);
        }

        /// <summary>Selected method to display functionality.
        /// Displays list of days.</summary>
        /// <param name="cineplexs"> parameter takes a struct Cineplex.</param>
        private void DisplayDays(Cineplex cineplex)
        {
            for (int i = 0; i < SessionModel.Days.Length; i++)
            {
                Console.WriteLine(i + " - {0}", SessionModel.Days[i]);
            }

            try
            {
                int index = Program.EnterOption();
                if (index > SessionModel.Days.Length)
                    throw new SystemException();
                List<Session> sessions = SessionModel.Instance.GetSessionsByCineplexDay(cineplex, index);
                DisplaySchedules(sessions, false);
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 0 to " + (SessionModel.Days.Length - 1));
            }
        }

        /// <summary>Selected method to display functionality.
        /// Displays list of sessions and option to show day of week or not.</summary>
        /// <param name="cineplexs"> parameter takes a list of Session struct.</param>
        /// <param name="showDayOfWeek"> parameter takes boolean option to
        /// display whether or not to show days of week.</param>
        public void DisplaySchedules(List<Session> sessions, bool showDayOfWeek)
        {
            int i = 0;
            foreach (Session session in sessions)
            {
                if (!showDayOfWeek)
                {
                    Console.WriteLine(i + " -\t{0,5} {1,25} \t ${2} \t {3}/{4}",
                        session.movieId.time,
                        session.movieId.title,
                        session.movieId.price,
                        session.seatsOccupied,
                        session.cineplexId.totalSeats);
                }
                else
                {
                    Console.WriteLine("{0,3} - {1,13}  {2,9}  {3,4}  {4,23}   ${5}   {6}/{7}",
                        i,
                        session.cineplexId.cineplexName,
                        session.dayOfWeek,
                        session.movieId.time,
                        session.movieId.title,
                        session.movieId.price,
                        session.seatsOccupied,
                        session.cineplexId.totalSeats);
                }
                i++;
            }

            try
            {
                int index = Program.EnterOption();
                AddSeats(sessions[index]);
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 0 to " + (i - 1));
            }
        }

        /// <summary>Selected method to display functionality.
        /// Displays option to add x amount of seats.</summary>
        /// <param name="session"> parameter takes a Session struct.</param>
        public void AddSeats(Session session)
        {
            Console.WriteLine("Movie - {0} on {1} at {2} in {3}",
                session.movieId.title,
                session.dayOfWeek,
                session.movieId.time,
                session.cineplexId.cineplexName);
            Console.WriteLine("Seats: {0}/{1}",
                session.seatsOccupied,
                session.cineplexId.totalSeats);

            int seatAvailable = session.cineplexId.totalSeats - session.seatsOccupied;
            Console.Write("\nEnter how many seats you want to book ({0} seats are available): ",
                seatAvailable);
            try
            {
                int newSeats = int.Parse(Console.ReadLine());

                if (newSeats > seatAvailable)
                    throw new SystemException();
                ConfirmAdd(session, newSeats);
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("There is only " + seatAvailable + " available.");
            }
        }

        /// <summary>Selected method to display functionality.
        /// Displays confirmation to enter new number of seats.</summary>
        /// <param name="session"> parameter takes a Session struct.</param>
        /// <param name="newSeats"> parameter takes a new number of seats.</param>
        private void ConfirmAdd(Session session, int newSeats)
        {
            Console.WriteLine("{0} seats will cost ${1}): ",
                newSeats, (session.movieId.price * newSeats));

            string confirm = "";
            while (!confirm.ToLower().Equals("n") &&
                !confirm.ToLower().Equals("no") &&
                !confirm.ToLower().Equals("y") &&
                !confirm.ToLower().Equals("yes"))
            {
                Console.Write("Confirm you want to add {0} seats (Yes/No): ", newSeats);
                confirm = Console.ReadLine();
                if (confirm.ToLower().Equals("y") ||
                confirm.ToLower().Equals("yes"))
                {
                    int sessionIndex = SessionModel.Instance.SearchSessionIndex(
                        session.cineplexId,
                        session.movieId,
                        session.dayOfWeek,
                        session.seatsOccupied);
                    if (sessionIndex == -1)
                        return;

                    session.seatsOccupied += newSeats;
                    SessionModel.Instance.Sessions[sessionIndex] = session;
                    Console.WriteLine("{0} seats have been added.", newSeats);
                    break;
                }
                else if (confirm.ToLower().Equals("n") ||
                confirm.ToLower().Equals("no"))
                {
                    Console.WriteLine("Nothing was added.");
                }
                else
                {
                    Console.WriteLine("Please enter yes or no.");
                }
            }
        }
    }
}
