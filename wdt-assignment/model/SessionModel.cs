using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.model
{
    /// <summary>Struct of Session to link between cineplex and movie and
    /// day of the week and number of seats occupied.
    /// Session has many to 1 relation with Movie and Cineplex</summary>
    struct Session
    {
        public Cineplex cineplexId;
        public Movie movieId;
        public string dayOfWeek;
        public int seatsOccupied;
    };
    class SessionModel
    {
        private const int DID_NOT_FIND_SESSION_INDEX = -1;
        private string[] days = new String[7] {
            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
            };

        /// <summary>Getter to get the Days of the week.</summary>
        /// <returns>Returns an array of days.</returns>
        public static string[] Days
        {
            get
            {
                return SessionModel.Instance.days;
            }
        }

        private List<Session> sessions = new List<Session>();
        private static SessionModel instance;

        /// <summary>Private constructor for the singleton pattern.
        /// It is set to private so we cannot instantiate the class
        /// with new.</summary>
        private SessionModel() { }

        /// <summary>Getter to get a single and same instance of Session Model.</summary>
        /// <returns>Returns a saved instance of Session Model.</returns>
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

        /// <summary>Getter to get a list of Session.</summary>
        /// <returns>Returns list of sessions.</returns>
        public List<Session> Sessions
        {
            get
            {
                return sessions;
            }
        }

        /// <summary>Method to add a session. If similar session then it will not add.</summary>
        /// <param name="cineplexId"> parameter takes a struct Cineplex.</param>
        /// <param name="movieId"> parameter takes a struct Movie.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <returns>Returns session that has been added or found.</returns>
        public Session AddSession(Cineplex cineplexId,Movie movieId,
            string dayOfWeek, int seatsOccupied)
        {
            int sessionIndex = SearchSessionIndex(cineplexId,movieId,
                dayOfWeek, seatsOccupied);
            if (sessionIndex != DID_NOT_FIND_SESSION_INDEX) return sessions[sessionIndex];

            Session session = new Session();
            session.cineplexId = cineplexId;
            session.movieId = movieId;
            session.dayOfWeek = dayOfWeek;
            session.seatsOccupied = seatsOccupied;
            sessions.Add(session);

            return session;
        }

        /// <summary>Iterates through the list of session to find if there is same 
        /// as parameters. Returns the index if found, otherwise -1 representing 
        /// did not find.</summary>
        /// <param name="cineplexId"> parameter takes a struct Cineplex.</param>
        /// <param name="movieId"> parameter takes a struct Movie.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <returns>Returns index of session found or -1 representing not found.</returns>
        public int SearchSessionIndex(Cineplex cineplexId,Movie movieId,
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
            return DID_NOT_FIND_SESSION_INDEX;
        }

        /// <summary>Get a list of session by cineplex and the day of week.</summary>
        /// <param name="cineplexId"> parameter takes a struct Cineplex.</param>
        /// <param name="dayIndex"> parameter takes an integer index for day.</param>
        /// <returns>Returns ;ist of session.</returns>
        public List<Session> GetSessionsByCineplexDay(Cineplex cineplexId, int dayIndex)
        {
            List<Session> newSessions = new List<Session>();
            if (dayIndex > days.Length)
                return newSessions;

            for (int i = 0; i < sessions.Count; i++)
            {
                if (sessions[i].cineplexId.Equals(cineplexId) &&
                    sessions[i].dayOfWeek.Equals(days[dayIndex]))
                    newSessions.Add(sessions[i]);
            }
            return newSessions;
        }

        /// <summary>Searches for Movie by title</summary>
        /// <param name="title"> parameter takes a string of a title to search.</param>
        /// <returns>Returns list of sessions otherwise throws custom exception saying
        /// cineplex name could not be found.</returns>
        public List<Session> SearchMovie(string title)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(title.ToLower());
            if (sessions.Exists(x => regEx.IsMatch(x.movieId.title.ToLower())))
                return sessions.Where(s => regEx.IsMatch(s.movieId.title.ToLower())).ToList();

            throw new CustomCouldntFindException("Could not find the movie: " + title);
        }
    }
}
