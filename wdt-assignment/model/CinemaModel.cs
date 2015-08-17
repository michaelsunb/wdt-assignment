using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Model
{
    class CinemaModel
    {
        public struct Cineplex
        {
            public string cinemaName;
            public int totalSeats;
        };
        private List<Cineplex> cineplexs = new List<Cineplex>();
        public void AddCinplex(string cinemaName, int totalSeats = 20)
        {
            if (SearchCinplex(cinemaName)) return;

            Cineplex cineplex = new Cineplex();
            cineplex.cinemaName = cinemaName;
            cineplex.totalSeats = totalSeats;
            cineplexs.Add(cineplex);
        }
        public bool SearchCinplex(string cinemaName)
        {
            foreach (Cineplex cineplex in cineplexs)
                if (cineplex.cinemaName.Equals(cinemaName))
                    return true;
            return false;
        }
        public List<Cineplex> getCineplex()
        {
            return cineplexs;
        }
    }
}
