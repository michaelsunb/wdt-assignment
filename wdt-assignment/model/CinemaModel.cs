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
            int cineplexIndex = SearchCinplexIndex(cinemaName, totalSeats);
            if (cineplexIndex != -1) return cineplexs[cineplexIndex];

            Cineplex cineplex = new Cineplex();
            cineplex.cinemaName = cinemaName;
            cineplex.totalSeats = totalSeats;
            cineplexs.Add(cineplex);

            return cineplex;
        }

        public int SearchCinplexIndex(string cinemaName, int totalSeats)
        {
            for (int i = 0; i < cineplexs.Count; i++ )
            {
                if (cineplexs[i].cinemaName.Equals(cinemaName) &&
                    cineplexs[i].totalSeats.Equals(totalSeats))
                    return i;
            }
            return -1;
        }

        public IEnumerable<Cineplex> SearchCinplex(string cinemaName)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(cinemaName.ToLower());
            if (cineplexs.Exists(x => regEx.IsMatch(x.cinemaName.ToLower())))
                return cineplexs.Where(s => regEx.IsMatch(s.cinemaName.ToLower()));
            return null;
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
