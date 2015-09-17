using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdt_assignment.model;

namespace wdt_assignment.Option
{
    class EditDeleteBooking : BaseSessionOption, IOption
    {
        private const int ADD_SEATS = 1;
        private const int REMOVE_SEATS = 2;
        private Session session;
        /// <summary>Returns a string "Edit/delete a current booking"</summary>
        /// <returns>Returns string "Edit/delete a current booking"</returns>
        public string GetOption()
        {
            return "Edit/delete a current booking";
        }

        /// <summary>Selected method to display functionality.
        /// Displays entire list of sessions and pick one of them.</summary>
        public void Selected()
        {
            Console.WriteLine("Edit/Delete a current booking\n");
            int i = 0;
            foreach (Session session in SessionModel.Instance.Sessions)
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
                i++;
            }
            int index = 0;
            try
            {
                index = Program.EnterOption();
                session = SessionModel.Instance.Sessions[index];
                AddDeleteSeat();
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 0 to " + (i-1));
            }
        }

        /// <summary>Selected method to display functionality.
        /// Displays 2 options. Add or remove seats.</summary>
        /// <param name="session"> parameter takes a struct Session.</param>
        private void AddDeleteSeat()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Add Seats");
            Console.WriteLine("2 - Remove Seats");
            int index = 0;
            try
            {
                index = Program.EnterOption();
                if (index == ADD_SEATS)
                {
                    AddSeats(session);
                    return;
                }
                if (index == REMOVE_SEATS)
                {
                    RemoveSeats();
                    return;
                }
                throw new SystemException();
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("Select only from 1 or 2");
            }
        }

        /// <summary>Selected method to display functionality.
        /// Displays option to remove x amount of seats.</summary>
        private void RemoveSeats()
        {
            Console.WriteLine("Movie - {0} on {1} at {2} in {3}",
                session.movieId.title,
                session.dayOfWeek,
                session.movieId.time,
                session.cineplexId.cineplexName);
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
                ConfirmRemoval(newSeats);
            }
            catch (SystemException)
            {
                throw new CustomCouldntFindException("There is only " + session.seatsOccupied + " booked.");
            }
        }

        /// <summary>Selected method to display functionality.
        /// Displays confirmation to enter new number of seats.</summary>
        /// <param name="newSeats"> parameter takes a new number of seats.</param>
        private void ConfirmRemoval(int newSeats)
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
                    int sessionIndex = SessionModel.Instance.SearchSessionIndex(
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
