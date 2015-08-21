using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.model
{
    struct Sessions
    {
        public Cineplex cineplexId;
        public Movie movieId;
        public string dayOfWeek;
        public int seatsAvailable;
    };
    class SessionModel
    {
        private List<Sessions> sessions = new List<Sessions>();
        private static SessionModel instance;

        private SessionModel() { }

        public static SessionModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionModel();
                }
                return instance;
            }
        }

        public List<Sessions> Sessions
        {
            get
            {
                return sessions;
            }
        }

        public Sessions AddSession(Cineplex cineplexId,Movie movieId,
            string dayOfWeek, int seatsAvailable)
        {
            int sessionIndex = SearchMovieDetailIndex(cineplexId,movieId,
                dayOfWeek, seatsAvailable);
            if (sessionIndex != -1) return sessions[sessionIndex];

            Sessions session = new Sessions();
            session.cineplexId = cineplexId;
            session.movieId = movieId;
            session.dayOfWeek = dayOfWeek;
            session.seatsAvailable = seatsAvailable;
            sessions.Add(session);

            return session;
        }

        public int SearchMovieDetailIndex(Cineplex cineplexId,Movie movieId,
            string dayOfWeek, int seatsAvailable)
        {
            for (int i = 0; i < sessions.Count; i++)
            {
                if (sessions[i].cineplexId.Equals(cineplexId) &&
                    sessions[i].movieId.Equals(movieId) &&
                    sessions[i].dayOfWeek.Equals(dayOfWeek) &&
                    sessions[i].seatsAvailable.Equals(seatsAvailable))
                    return i;
            }
            return -1;
        }

        public IEnumerable<Sessions> SearchCinplex(string cinemaName)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(cinemaName.ToLower());
            if (sessions.Exists(x => regEx.IsMatch(x.cineplexId.cinemaName.ToLower())))
                return sessions.Where(s => regEx.IsMatch(s.cineplexId.cinemaName.ToLower()));
            return null;
        }

        public IEnumerable<Sessions> SearchMovie(string title)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(title.ToLower());
            if (sessions.Exists(x => regEx.IsMatch(x.movieId.title.ToLower())))
                return sessions.Where(s => regEx.IsMatch(s.movieId.title.ToLower()));
            return null;
        }
    }
}
