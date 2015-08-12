using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Options
{
    class MovieName : IOption
    {
        public override string ToString()
        {
            return "Search by Cineplex OR movie ";
        }
        public void Selected()
        {
            Console.WriteLine("Selected B");
        }
    }
}
