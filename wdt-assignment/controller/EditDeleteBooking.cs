using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdt_assignment.model;

namespace wdt_assignment.Option
{
    class EditDeleteBooking : IOption
    {
        public string GetOption()
        {
            return "Edit/delete a current booking";
        }

        public void Selected()
        {
            Console.WriteLine("Edit/Delete a current booking\n");
            int i = 0;
            foreach (Session session in SessionModel.Instance.Sessions)
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
                i++;
            }
            int index = 0;
            try
            {
                index = Program.EnterOption();
                AddDeleteSeat(SessionModel.Instance.Sessions[index]);
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 0 to " + i);
            }
        }

        public void AddDeleteSeat(Session session)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Add Seats");
            Console.WriteLine("2 - Remove Seats");
            int index = 0;
            try
            {
                index = Program.EnterOption();
                if (index == 1)
                {
                    DisplayCineplexList.AddSeats(session);
                    return;
                }
                if (index == 2)
                {
                    RemoveSeats(session);
                    return;
                }
                throw new SystemException();
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 1 or 2");
            }
        }

        private void RemoveSeats(Session session)
        {
            Console.WriteLine("Movie - {0} on {1} at {2} in {3}",
                session.movieId.title,
                session.dayOfWeek,
                session.movieId.time,
                session.cineplexId.cinemaName);
            Console.WriteLine("Seats: {0}/{1}",
                session.seatsOccupied,
                session.cineplexId.totalSeats);

            Console.Write("\nEnter how many seats you want to remove ({0} seats are booked): ",
                session.seatsOccupied);
            try
            {
                int newSeats = int.Parse(Console.ReadLine());

                if (newSeats > session.seatsOccupied)
                    throw new SystemException();
                ConfirmRemoval(session, newSeats);
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("There is only " + session.seatsOccupied + " booked.");
            }
        }

        private void ConfirmRemoval(Session session, int newSeats)
        {
            Console.WriteLine();

            string confirm = "";
            while (!confirm.ToLower().Equals("n") &&
                !confirm.ToLower().Equals("no") &&
                !confirm.ToLower().Equals("y") &&
                !confirm.ToLower().Equals("yes"))
            {
                Console.Write("Confirm you want to remove {0} seats (Yes/No): ", newSeats);
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

                    session.seatsOccupied -= newSeats;
                    SessionModel.Instance.Sessions[sessionIndex] = session;
                    Console.WriteLine("{0} seats have been removed.", newSeats);
                    break;
                }
                else if (confirm.ToLower().Equals("n") ||
                confirm.ToLower().Equals("no"))
                {
                    Console.WriteLine("Nothing was removed.");
                }
                else
                {
                    Console.WriteLine("Please enter yes or no.");
                }
            }
        }
    }
}
