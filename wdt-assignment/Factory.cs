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
        public IOption OptionC()
        {
            return new Option.MovieBooking();
        }
        public IOption OptionA()
        {
            return new Option.MovieSession();
        }
        public IOption OptionB()
        {
            return new Option.MovieName();
        }


        public IOption Exit()
        {
            return new ExitOption();
        }
        class ExitOption : IOption
        {
            public string GetOption()
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
