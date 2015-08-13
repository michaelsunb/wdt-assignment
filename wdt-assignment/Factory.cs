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
        public IOption OptionA()
        {
            return new Option.MovieSession();
        }
        public IOption OptionB()
        {
            return new Option.MovieSearch();
        }
        public IOption OptionC()
        {
            return new Option.MovieBook();
        }
        public IOption Exit()
        {
            return new Option.Exit();
        }
    }
}
