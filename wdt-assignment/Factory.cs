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
        public IOption OptionA
        {
            get
            {
                return new Option.DisplayCineplexList();
            }
        }
        public IOption OptionB
        {
            get
            {
                return new Option.MovieSearch();
            }
        }
        public IOption OptionC
        {
            get
            {
                return new Option.MovieBook();
            }
        }
        public IOption Exit
        {
            get
            {
                return new Option.Exit();
            }
        }
    }
}
