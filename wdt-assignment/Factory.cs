using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment
{
    // Factory Pattern
    class Factory
    {
        /// <summary>OptionA getter returns the factory method to display Cineplex functionality.</summary>
        /// <returns>Returns option Display Cineplex List.</returns>
        public IOption OptionA
        {
            get
            {
                return new Option.DisplayCineplexList();
            }
        }

        /// <summary>OptionB getter returns the factory method to display search Cineplex or Movie functionality.</summary>
        /// <returns>Returns option Display search Cineplex or Movie.</returns>
        public IOption OptionB
        {
            get
            {
                return new Option.SearchCineplexMovie();
            }
        }

        /// <summary>OptionC getter returns the factory method to display Add or Delete booking functionality.</summary>
        /// <returns>Returns option Display add or delete booking.</returns>
        public IOption OptionC
        {
            get
            {
                return new Option.EditDeleteBooking();
            }
        }

        /// <summary>Displays the option to exit.</summary>
        /// <returns>Returns a console display of Terminating....</returns>
        public IOption Exit
        {
            get
            {
                return new Option.Exit();
            }
        }
    }
}
