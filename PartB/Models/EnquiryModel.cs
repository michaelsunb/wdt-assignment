using System;
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
    struct Enquiry
    {
        public int EnquiryID { set; get; }
        public string Email { set; get; }
        public string Message { set; get; }
        public int Status { set; get; }
    };
    class EnquiryModel
    {
        private const int DEFAULT_TOTAL_NUMBER_SEATS = 20;
        private const int DID_NOT_FIND_ENQUIRY_INDEX = -1;
        private List<Enquiry> enquries = new List<Enquiry>();
        private static EnquiryModel instance;

        /// <summary>Private constructor for the singleton pattern.
        /// It is set to private so we cannot instantiate the class
        /// with new.</summary>
        private EnquiryModel() { }

        /// <summary>Getter to get a single and same instance of Cineplex Model.</summary>
        /// <returns>Returns a saved instance of Cineplex Model.</returns>
        public static EnquiryModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnquiryModel();
                }
                return instance;
            }
        }

        /// <summary>Getter to get a list of Cineplex.</summary>
        /// <returns>Returns list of Cineplexs.</returns>
        public List<Enquiry> Cineplex
        {
            get
            {
                return GetCineplex();
            }
        }
        public List<Enquiry> GetCineplex()
        {
            if (enquries.Count > 0)
            {
                return enquries;
            }

            SqlConnection conn = null;
            SqlCommand cmd = null;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "select * from [master].[dbo].[Enquiry]";
                    cmd = new SqlCommand(sql, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    enquries = new List<Enquiry>();
                    while(rdr.Read())
                    {
                        Enquiry enquiry = new Enquiry();
                        enquiry.EnquiryID = (int)rdr[0];
                        enquiry.Email = (string)rdr[1];
                        enquiry.Message = (string)rdr[2];

                        enquries.Add(enquiry);
                    }
                    return enquries;
                }
                catch (Exception ex)
                {
                    return enquries;
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
        public Enquiry getEnquiryByID(int cineplexID)
        {
            enquries = GetCineplex();
            for (int i = 0; i < enquries.Count; i++)
            {
                if (enquries[i].EnquiryID.Equals(cineplexID))
                    return enquries[i];
            }

            throw new CustomCouldntFindException("Could not find the movie: " + cineplexID);
        }

        /// <summary>Method to add a movie. If similar movie then it will not add.</summary>
        /// <param name="cineplexName"> parameter takes a string for cineplex name.</param>
        /// <param name="totalSeats"> parameter takes a total number of seats.
        /// Default is 20.</param>
        /// <returns>Returns cineplex that has been added or found.</returns>
        public Enquiry AddCinplex(string email, string message, int status)
        {
            int cineplexIndex = SearchEnquiryIndex(email, message);
            if (cineplexIndex != DID_NOT_FIND_ENQUIRY_INDEX) return enquries[cineplexIndex];

            Enquiry enquiry = new Enquiry();
            enquiry.Email = email;
            enquiry.Message = message;
            enquiry.Status = status;

            enquries.Add(enquiry);

            return enquiry;
        }

        /// <summary>Iterates through the list of movies to find if there is same 
        /// as parameters. Returns the index if found, otherwise -1 representing 
        /// did not find.</summary>
        /// <param name="cineplexName"> parameter takes a string for cineplex name.</param>
        /// <param name="totalSeats"> parameter takes a total number of seats.</param>
        /// <returns>Returns index of cineplex found or -1 representing not found.</returns>
        public int SearchEnquiryIndex(string email, string message)
        {
            for (int i = 0; i < enquries.Count; i++ )
            {
                if (enquries[i].Email.Equals(email) &&
                    enquries[i].Message.Equals(message))
                    return i;
            }
            return DID_NOT_FIND_ENQUIRY_INDEX;
        }
    }
}
