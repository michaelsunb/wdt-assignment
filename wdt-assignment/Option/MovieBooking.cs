using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Option
{
    class MovieBooking : IOption
    {
        public string GetOption()
        {
            return "Edit/delete a current booking";
        }
        public void Selected()
        {
            Console.WriteLine("Selected C");
        }
    }
}
