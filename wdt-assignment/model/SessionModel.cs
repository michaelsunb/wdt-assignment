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

        public static SessionModel GetInstance
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

        public List<Sessions> GetSessions()
        {
            return sessions;
        }

        public Sessions AddSession(Cineplex cineplexId,Movie movieId,
            string dayOfWeek, int seatsAvailable)
        {
            Sessions session = new Sessions();
            session.cineplexId = cineplexId;
            session.movieId = movieId;
            session.dayOfWeek = dayOfWeek;
            session.seatsAvailable = seatsAvailable;
            sessions.Add(session);

            return session;
        }

        public int SearchMovieDetailIndex(string dayOfWeek, int seatsAvailable)
        {
            for (int i = 0; i < sessions.Count; i++)
            {
                if (sessions[i].dayOfWeek.Equals(dayOfWeek) &&
                    sessions[i].seatsAvailable.Equals(seatsAvailable))
                    return i;
            }
            return -1;
        }
    }
}
