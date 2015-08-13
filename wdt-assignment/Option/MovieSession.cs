using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Option
{
    class MovieSession : IOption
    {
        public string GetOption()
        {
            return "Display Cineplex list";
        }
        public void Selected()
        {
            Console.WriteLine("Selected A");
        }
    }
}
