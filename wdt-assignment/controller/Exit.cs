using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Option
{
    class Exit : IOption
    {
        /// <summary>Returns a string "Exit"</summary>
        /// <returns>Returns string "Exit"</returns>
        public string GetOption()
        {
            return "Exit";
        }

        /// <summary>Displays "Terminating..."
        /// and exits the program.</summary>
        public void Selected()
        {
            Console.Beep();
            Console.WriteLine("Terminating...");
            System.Environment.Exit(-1);
        }
    }
}
