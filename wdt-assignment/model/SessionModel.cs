using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.model
{
    struct Session
    {
        public Cineplex cineplexId;
        public Movie movieId;
        public string dayOfWeek;
        public int seatsOccupied;
    };
    class SessionModel
    {
        private string[] days = new String[7] {
            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
            };

        public static string[] Days
        {
            get
            {
                return SessionModel.Instance.days;
            }
        }

        private List<Session> sessions = new List<Session>();
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

        public List<Session> Sessions
        {
            get
            {
                return sessions;
            }
        }

        public Session AddSession(Cineplex cineplexId,Movie movieId,
            string dayOfWeek, int seatsAvailable)
        {
            int sessionIndex = SearchMovieDetailIndex(cineplexId,movieId,
                dayOfWeek, seatsAvailable);
            if (sessionIndex != -1) return sessions[sessionIndex];

            Session session = new Session();
            session.cineplexId = cineplexId;
            session.movieId = movieId;
            session.dayOfWeek = dayOfWeek;
            session.seatsOccupied = seatsAvailable;
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
                    sessions[i].seatsOccupied.Equals(seatsAvailable))
                    return i;
            }
            return -1;
        }

        public List<Session> GetSessionsByCineplexDay(Cineplex cineplexId, int dayIndex)
        {
            List<Session> newSessions = new List<Session>();
            for (int i = 0; i < sessions.Count; i++)
            {
                if (sessions[i].cineplexId.Equals(cineplexId) &&
                    sessions[i].dayOfWeek.Equals(days[dayIndex]))
                    newSessions.Add(sessions[i]);
            }
            return newSessions;
        }

        public List<Session> SearchCinplex(string cinemaName)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(cinemaName.ToLower());
            if (sessions.Exists(x => regEx.IsMatch(x.cineplexId.cinemaName.ToLower())))
                return sessions.Where(s => regEx.IsMatch(s.cineplexId.cinemaName.ToLower())).ToList();

            throw new CustomCouldntFindException("Could not find the cineplex: " + cinemaName);
        }

        public List<Session> SearchMovie(string title)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(title.ToLower());
            if (sessions.Exists(x => regEx.IsMatch(x.movieId.title.ToLower())))
                return sessions.Where(s => regEx.IsMatch(s.movieId.title.ToLower())).ToList();

            throw new CustomCouldntFindException("Could not find the movie: " + title);
        }
    }
}
