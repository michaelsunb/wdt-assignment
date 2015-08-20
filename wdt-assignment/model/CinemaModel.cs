using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.model
{
    struct Cineplex
    {
        public string cinemaName;
        public int totalSeats;
    };
    class CinemaModel
    {
        private List<Cineplex> cineplexs = new List<Cineplex>();
        private static CinemaModel instance;

        private CinemaModel() {}

        public static CinemaModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CinemaModel();
                }
                return instance;
            }
        }

        public Cineplex AddCinplex(string cinemaName, int totalSeats = 20)
        {
            int cineplexIndex = SearchCinplexIndex(cinemaName);
            if (cineplexIndex != -1) return cineplexs[cineplexIndex];

            Cineplex cineplex = new Cineplex();
            cineplex.cinemaName = cinemaName;
            cineplex.totalSeats = totalSeats;
            cineplexs.Add(cineplex);

            return cineplex;
        }

        public int SearchCinplexIndex(string cinemaName)
        {
            for (int i = 0; i < cineplexs.Count; i++ )
            {
                if (cineplexs[i].cinemaName.Equals(cinemaName))
                    return i;
            }
            return -1;
        }

        public List<Cineplex> Cineplex
        {
            get
            {
                return cineplexs;
            }
        }
    }
}
