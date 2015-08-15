using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Option
{
    class Exit : IOption
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
