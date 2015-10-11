using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartB.Models
{
    /// <summary>Struct of Session to link between cineplex and movie and
    /// day of the week and number of seats occupied.
    /// Session has many to 1 relation with Movie and Cineplex</summary>
    struct Seating
    {
        public int CineplexMovieID { set; get; }
        public string SeatRow { set; get; }
        public string SeatColumn { set; get; }
        public string extra { set; get; }
    };
    class SeatingModel
    {
        private const int DID_NOT_FIND_SEATING_INDEX = -1;

        private List<Seating> seatings = new List<Seating>();
        private static SeatingModel instance;

        /// <summary>Private constructor for the singleton pattern.
        /// It is set to private so we cannot instantiate the class
        /// with new.</summary>
        private SeatingModel() { }

        /// <summary>Getter to get a single and same instance of Session Model.</summary>
        /// <returns>Returns a saved instance of Session Model.</returns>
        public static SeatingModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SeatingModel();
                }
                return instance;
            }
        }

        /// <summary>Getter to get a list of Session.</summary>
        /// <returns>Returns list of sessions.</returns>
        public List<Seating> Seatings
        {
            get
            {
                return seatings;
            }
        }
        public List<Seating> GetSeating()
        {
            if (seatings.Count > 0)
            {
                return seatings;
            }

            SqlConnection conn = null;
            SqlCommand cmd = null;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "select * from [master].[dbo].[Seating]";
                    cmd = new SqlCommand(sql, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    seatings = new List<Seating>();
                    while (rdr.Read())
                    {
                        Seating seating = new Seating();
                        seating.CineplexMovieID = (int)rdr[0];
                        seating.SeatRow = (string)rdr[1];
                        seating.SeatColumn = (string)rdr[2];
                        seating.extra = (string)rdr[3];

                        seatings.Add(seating);
                    }
                    return seatings;
                }
                catch (Exception ex)
                {
                    return seatings;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                    if (conn != null)
                    {
                        conn.Dispose();
                    }
                }
            }
        }

        /// <summary>Method to add a session. If similar session then it will not add.</summary>
        /// <param name="cineplexId"> parameter takes a struct Cineplex.</param>
        /// <param name="movieId"> parameter takes a struct Movie.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <returns>Returns session that has been added or found.</returns>
        public Seating AddSeating(int cineplexMovieID, string SeatRow, string SeatColumn, string extra)
        {
            int sessionIndex = SearchSeatingIndex(cineplexMovieID, SeatRow,
                SeatColumn, extra);
            if (sessionIndex != DID_NOT_FIND_SEATING_INDEX) return seatings[sessionIndex];

            Seating seating = new Seating();
            seating.CineplexMovieID = cineplexMovieID;
            seating.SeatRow = SeatRow;
            seating.SeatColumn = SeatColumn;
            seating.extra = extra;

            seatings.Add(seating);

            return seating;
        }

        /// <summary>Iterates through the list of session to find if there is same 
        /// as parameters. Returns the index if found, otherwise -1 representing 
        /// did not find.</summary>
        /// <param name="cineplexId"> parameter takes a struct Cineplex.</param>
        /// <param name="movieId"> parameter takes a struct Movie.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <returns>Returns index of session found or -1 representing not found.</returns>
        public int SearchSeatingIndex(int cineplexMovieID, string SeatRow,
            string SeatColumn, string extra)
        {
            for (int i = 0; i < seatings.Count; i++)
            {
                if (seatings[i].CineplexMovieID.Equals(cineplexMovieID) &&
                    seatings[i].SeatRow.Equals(SeatRow) &&
                    seatings[i].SeatColumn.Equals(SeatColumn) &&
                    seatings[i].extra.Equals(extra))
                    return i;
            }
            return DID_NOT_FIND_SEATING_INDEX;
        }
    }
}
