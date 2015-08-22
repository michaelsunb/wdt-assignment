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
        public string GetOption()
        {
            return "Display Cineplex list";
        }

        public void Selected()
        {
            List<Cineplex> cineplexs = CinemaModel.Instance.Cineplex;

            DisplayCineplexs(cineplexs);
        }
    }
}
