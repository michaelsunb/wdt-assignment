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
            return new Options.MovieSession();
        }
        public IOption OptionB()
        {
            return new Options.MovieName();
        }
        public IOption OptionC()
        {
            return new Options.MovieBooking();
        }


        public IOption Exit()
        {
            return new ExitOption();
        }
        class ExitOption : IOption
        {
            public override string ToString()
            {
                return "Exit";
            }
            public void Selected()
            {
                Console.WriteLine("Exit...");
                System.Environment.Exit(-1);
            }
        }
    }
}
