using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Option
{
    class MovieName : IOption
    {
        public string GetOption()
        {
            return "Search by Cineplex OR movie ";
        }
        public void Selected()
        {
            Console.WriteLine("Selected B");
        }
    }
}
