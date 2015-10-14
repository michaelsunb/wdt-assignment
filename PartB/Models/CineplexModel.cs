using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartB.Models
{
    /// <summary>Struct of Cineplex to store cinemaName and totalSeats.
    /// Cineplex has 1 to many relation with Session.</summary>
    public struct Cineplex
    {
        public int CineplexID { get; set; }
        public string Location { get; set; }
        public string ShortDecription { get; set; }
        public string LongDecription { get; set; }
        public string ImageUrl { get; set; }
        public int status { get; set; }
    };
    class CineplexModel
    {
        private static string CONNECTION_STRING =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
                // TODO
                //return cineplexs;
            }

            SqlConnection conn = null;
            SqlCommand cmd = null;
            using (conn = new SqlConnection(CONNECTION_STRING))
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
                        cineplex.CineplexID = (int)rdr["CineplexID"];
                        cineplex.Location = (string)rdr["Location"];
                        cineplex.ShortDecription = rdr["ShortDescription"].ToString();
                        cineplex.LongDecription = (string)rdr["LongDescription"];
                        cineplex.ImageUrl = (string)rdr["ImageUrl"];
                        cineplex.status = int.Parse(rdr["Status"].ToString());

                        cineplexs.Add(cineplex);
                    }
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
        public Cineplex SoftDelete(Cineplex oriCineplex)
        {
            int cineplexIndex =
                SearchCinplexIndex(
                oriCineplex.Location,
                oriCineplex.ShortDecription,
                oriCineplex.LongDecription,
                oriCineplex.ImageUrl);
            if (cineplexIndex == DID_NOT_FIND_CINEPLEX_INDEX)
                throw new CustomCouldntFindException("Failed to add coming soon movie!");

            Update(oriCineplex.CineplexID,
                oriCineplex.Location,
                oriCineplex.ShortDecription,
                oriCineplex.LongDecription,
                oriCineplex.ImageUrl,
                0);

            Cineplex cineplex = new Cineplex();
            cineplex.CineplexID = oriCineplex.CineplexID;
            cineplex.Location = oriCineplex.Location;
            cineplex.ShortDecription = oriCineplex.ShortDecription;
            cineplex.LongDecription = oriCineplex.LongDecription;
            cineplex.ImageUrl = oriCineplex.ImageUrl;
            cineplex.status = 0;

            cineplexs[cineplexIndex] = cineplex;

            return cineplex;
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

            cineplexIndex = InsertGetId(location, shortDescription, longDescription, imageUrl);

            Cineplex cineplex = new Cineplex();
            cineplex.CineplexID = cineplexIndex;
            cineplex.Location = location;
            cineplex.ShortDecription = shortDescription;
            cineplex.LongDecription = longDescription;
            cineplex.ImageUrl = imageUrl;

            cineplexs.Add(cineplex);

            return cineplex;
        }
        private int InsertGetId(string location, string shortDescription,
            string longDescription, string imageUrl)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            using (conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO [master].[dbo].[Cineplex]" +
                        "(Location,ShortDescription,LongDescription,ImageUrl,Status) VALUES" +
                        "(@Location,@ShortDescription,@LongDescription,@ImageUrl,@Status)";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = location;
                    cmd.Parameters.Add("@ShortDescription", SqlDbType.VarChar).Value = shortDescription;
                    cmd.Parameters.Add("@LongDescription", SqlDbType.VarChar).Value = longDescription;
                    cmd.Parameters.Add("@ImageUrl", SqlDbType.VarChar).Value = imageUrl;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 1;

                    return Convert.ToInt32(cmd.ExecuteScalar());
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
        /// <summary>Iterates through the list of movies to find if there is same 
        /// as parameters. Returns the index if found, otherwise -1 representing 
        /// did not find.</summary>
        /// <param name="cineplexName"> parameter takes a string for cineplex name.</param>
        /// <param name="totalSeats"> parameter takes a total number of seats.</param>
        /// <returns>Returns index of cineplex found or -1 representing not found.</returns>
        public int SearchCinplexIndex(string location, string shortDescription, string longDescription,
            string imageUrl, int status = 1)
        {
            for (int i = 0; i < cineplexs.Count; i++ )
            {
                if (cineplexs[i].Location.Equals(location) &&
                    cineplexs[i].ShortDecription.Equals(shortDescription) &&
                    cineplexs[i].LongDecription.Equals(longDescription) &&
                    cineplexs[i].ImageUrl.Equals(imageUrl) &&
                    cineplexs[i].status.Equals(status))
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
        public Cineplex EditCinplex(Cineplex oriCineplex, string location, string shortDescription,
            string longDescription, string imageUrl,int status)
        {
            int cineplexIndex = SearchCinplexIndex(
                oriCineplex.Location,
                oriCineplex.ShortDecription,
                oriCineplex.LongDecription,
                oriCineplex.ImageUrl,
                oriCineplex.status);
            if (cineplexIndex == DID_NOT_FIND_CINEPLEX_INDEX)
                throw new CustomCouldntFindException("Failed to add coming soon movie!");

            Update(oriCineplex.CineplexID, location, shortDescription, longDescription, imageUrl,status);

            Cineplex cineplex = new Cineplex();
            cineplex.CineplexID = oriCineplex.CineplexID;
            cineplex.Location = location;
            cineplex.ShortDecription = shortDescription;
            cineplex.LongDecription = longDescription;
            cineplex.ImageUrl = imageUrl;
            cineplex.status = status;

            cineplexs[cineplexIndex] = cineplex;

            return cineplex;
        }
        private void Update(int cineplexID,string location, string shortDescription,
            string longDescription, string imageUrl, int status)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            using (conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE [master].[dbo].[Cineplex] SET " +
                        "Location = @Location," +
                        "ShortDescription = @ShortDescription," +
                        "LongDescription = @LongDescription," +
                        "ImageUrl = @ImageUrl," +
                        "Status = @Status " +
                        "WHERE CineplexID = @CineplexID";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = location;
                    cmd.Parameters.Add("@ShortDescription", SqlDbType.VarChar).Value = shortDescription;
                    cmd.Parameters.Add("@LongDescription", SqlDbType.VarChar).Value = longDescription;
                    cmd.Parameters.Add("@ImageUrl", SqlDbType.VarChar).Value = imageUrl;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;

                    cmd.Parameters.Add("@CineplexID", SqlDbType.Int).Value = cineplexID;

                    cmd.ExecuteNonQuery();
                    return;
                }
                catch (Exception ex)
                {
                    throw new CustomCouldntFindException(ex.StackTrace);
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
    }
}
