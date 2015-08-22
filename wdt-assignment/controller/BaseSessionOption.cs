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
        public void DisplayCineplexs(List<Cineplex> cineplexs)
        {
            Console.WriteLine();

            int i = 0;
            foreach (Cineplex cineplex in cineplexs)
            {
                Console.WriteLine(++i + " - {0}", cineplex.cinemaName);
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

        private void DisplayDays(Cineplex cineplex)
        {
            for (int i = 0; i < SessionModel.Days.Length; i++)
            {
                Console.WriteLine(i + " - {0}", SessionModel.Days[i]);
            }

            try
            {
                int index = Program.EnterOption();
                List<Session> sessions = SessionModel.Instance.GetSessionsByCineplexDay(cineplex, index);
                DisplaySchedules(sessions, false);
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 0 to " + (SessionModel.Days.Length - 1));
            }
        }

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
                        session.cineplexId.cinemaName,
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

        public void AddSeats(Session session)
        {
            Console.WriteLine("Movie - {0} on {1} at {2} in {3}",
                session.movieId.title,
                session.dayOfWeek,
                session.movieId.time,
                session.cineplexId.cinemaName);
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
                    int sessionIndex = SessionModel.Instance.SearchMovieDetailIndex(
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
