using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Options
{
    class MovieSession : IOption
    {
        public override string ToString()
        {
            return "Display Cineplex list";
        }
        public void Selected()
        {
            Console.WriteLine("Selected A");
        }
    }
}
