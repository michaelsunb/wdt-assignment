﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartB.Models
{
    /// <summary>Struct of Cineplex to store cinemaName and totalSeats.
    /// Cineplex has 1 to many relation with Session.</summary>
    struct Cineplex
    {
        public int CineplexID { set; get; }
        public string Location { set; get; }
        public string ShortDecription { set; get; }
        public string LongDecription { set; get; }
        public string ImageUrl { set; get; }
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
                return GetCineplex();
            }
        }
        public List<Cineplex> GetCineplex()
        {
            if (cineplexs.Count > 0)
            {
                return cineplexs;
            }

            SqlConnection conn = null;
            SqlCommand cmd = null;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "select * from [master].[dbo].[Cineplex]";
                    cmd = new SqlCommand(sql, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    cineplexs = new List<Cineplex>();
                    while(rdr.Read())
                    {
                        Cineplex cineplex = new Cineplex();
                        cineplex.CineplexID = (int)rdr[0];
                        cineplex.Location = (string)rdr[1];
                        cineplex.ShortDecription = (string)rdr[2];
                        cineplex.LongDecription = (string)rdr[3];
                        cineplex.ImageUrl = (string)rdr[3];

                        cineplexs.Add(cineplex);
                    }
                    return cineplexs;
                }
                catch (Exception ex)
                {
                    return cineplexs;
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
        public Cineplex getCineplexByID(int cineplexID)
        {
            cineplexs = GetCineplex();
            for (int i = 0; i < cineplexs.Count; i++)
            {
                if (cineplexs[i].CineplexID.Equals(cineplexID))
                    return cineplexs[i];
            }

            throw new CustomCouldntFindException("Could not find the movie: " + cineplexID);
        }

        /// <summary>Method to add a movie. If similar movie then it will not add.</summary>
        /// <param name="cineplexName"> parameter takes a string for cineplex name.</param>
        /// <param name="totalSeats"> parameter takes a total number of seats.
        /// Default is 20.</param>
        /// <returns>Returns cineplex that has been added or found.</returns>
        public Cineplex AddCinplex(string location, string shortDescription, string longDescription,
            string imageUrl)
        {
            int cineplexIndex = SearchCinplexIndex(location,shortDescription,longDescription,imageUrl);
            if (cineplexIndex != DID_NOT_FIND_CINEPLEX_INDEX) return cineplexs[cineplexIndex];

            Cineplex cineplex = new Cineplex();
            cineplex.Location = location;
            cineplex.ShortDecription = shortDescription;
            cineplex.LongDecription = longDescription;
            cineplex.ImageUrl = imageUrl;

            cineplexs.Add(cineplex);

            return cineplex;
        }

        /// <summary>Iterates through the list of movies to find if there is same 
        /// as parameters. Returns the index if found, otherwise -1 representing 
        /// did not find.</summary>
        /// <param name="cineplexName"> parameter takes a string for cineplex name.</param>
        /// <param name="totalSeats"> parameter takes a total number of seats.</param>
        /// <returns>Returns index of cineplex found or -1 representing not found.</returns>
        public int SearchCinplexIndex(string location, string shortDescription, string longDescription,
            string imageUrl)
        {
            for (int i = 0; i < cineplexs.Count; i++ )
            {
                if (cineplexs[i].Location.Equals(location) &&
                    cineplexs[i].ShortDecription.Equals(shortDescription) &&
                    cineplexs[i].LongDecription.Equals(longDescription) &&
                    cineplexs[i].ImageUrl.Equals(imageUrl))
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
            if (cineplexs.Exists(x => regEx.IsMatch(x.Location.ToLower())))
                return cineplexs.Where(s => regEx.IsMatch(s.Location.ToLower())).ToList();

            throw new CustomCouldntFindException("Could not find the cineplex: " + cineplexName);
        }
    }
}
