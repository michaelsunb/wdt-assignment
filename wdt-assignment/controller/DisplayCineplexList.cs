using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdt_assignment.model;

namespace wdt_assignment.Option
{
    class DisplayCineplexList : BaseSessionOption, IOption
    {
        /// <summary>Returns a string "Display Cineplex list"</summary>
        /// <returns>Returns string "Display Cineplex list"</returns>
        public string GetOption()
        {
            return "Display Cineplex list";
        }

        /// <summary>Selected method to display functionality.</summary>
        public void Selected()
        {
            List<Cineplex> cineplexs = CineplexModel.Instance.Cineplex;

            DisplayCineplexs(cineplexs);
        }
    }
}
