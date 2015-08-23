using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.model
{
    /// <summary>Struct of Cineplex to store cinemaName and totalSeats.
    /// Cineplex has 1 to many relation with Session.</summary>
    struct Cineplex
    {
        public string cineplexName;
        public int totalSeats;
    };
    class CineplexModel
    {
        private const int DEFAULT_TOTAL_NUMBER_SEATS = 20;
        private const int DID_NOT_FIND_CINEPLEX_INDEX = -1;
        private List<Cineplex> cineplexs = new List<Cineplex>();
        private static CineplexModel instance;

        /// <summary>Private constructor for the singleton pattern.
        /// It is set to private so we cannot instantiate the class
        /// with new.</summary>
        private CineplexModel() {}

        /// <summary>Getter to get a single and same instance of Cineplex Model.</summary>
        /// <returns>Returns a saved instance of Cineplex Model.</returns>
        public static CineplexModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CineplexModel();
                }
                return instance;
            }
        }

        /// <summary>Getter to get a list of Cineplex.</summary>
        /// <returns>Returns list of Cineplexs.</returns>
        public List<Cineplex> Cineplex
        {
            get
            {
                return cineplexs;
            }
        }

        /// <summary>Method to add a movie. If similar movie then it will not add.</summary>
        /// <param name="cineplexName"> parameter takes a string for cineplex name.</param>
        /// <param name="totalSeats"> parameter takes a total number of seats.
        /// Default is 20.</param>
        /// <returns>Returns cineplex that has been added or found.</returns>
        public Cineplex AddCinplex(string cineplexName, int totalSeats = DEFAULT_TOTAL_NUMBER_SEATS)
        {
            int cineplexIndex = SearchCinplexIndex(cineplexName, totalSeats);
            if (cineplexIndex != DID_NOT_FIND_CINEPLEX_INDEX) return cineplexs[cineplexIndex];

            Cineplex cineplex = new Cineplex();
            cineplex.cineplexName = cineplexName;
            cineplex.totalSeats = totalSeats;
            cineplexs.Add(cineplex);

            return cineplex;
        }

        /// <summary>Iterates through the list of movies to find if there is same 
        /// as parameters. Returns the index if found, otherwise -1 representing 
        /// did not find.</summary>
        /// <param name="cineplexName"> parameter takes a string for cineplex name.</param>
        /// <param name="totalSeats"> parameter takes a total number of seats.</param>
        /// <returns>Returns index of cineplex found or -1 representing not found.</returns>
        public int SearchCinplexIndex(string cineplexName, int totalSeats)
        {
            for (int i = 0; i < cineplexs.Count; i++ )
            {
                if (cineplexs[i].cineplexName.Equals(cineplexName) &&
                    cineplexs[i].totalSeats.Equals(totalSeats))
                    return i;
            }
            return DID_NOT_FIND_CINEPLEX_INDEX;
        }

        /// <summary>Searches for cineplex by cineplex name</summary>
        /// <param name="cineplexName"> parameter takes a string of a cineplex name to search.</param>
        /// <returns>Returns list of sessions otherwise throws custom exception saying
        /// cineplex name could not be found.</returns>
        public List<Cineplex> SearchCinplex(string cineplexName)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(cineplexName.ToLower());
            if (cineplexs.Exists(x => regEx.IsMatch(x.cineplexName.ToLower())))
                return cineplexs.Where(s => regEx.IsMatch(s.cineplexName.ToLower())).ToList();

            throw new CustomCouldntFindException("Could not find the cineplex: " + cineplexName);
        }
    }
}
