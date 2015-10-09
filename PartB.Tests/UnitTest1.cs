using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace PartB.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateHashMethod()
        {

            String pw = "qqq";
            String debugPw = PasswordHash.PasswordHash.CreateHash(pw);

            Debug.WriteLine(debugPw);

            Assert.AreNotEqual(pw, debugPw);

            masterEntities db = new masterEntities();

            var t = new UserCredential //Make sure you have a table called test in DB
            {
                uname = "michael",
                pword = debugPw,
                utype = 1,
                name = "michael",
                email = "s3110401@student.rmit.edu.au"
            };

            //db.UserCredentials.Add(t);
            //db.SaveChanges();
        }
        [TestMethod]
        public void TestValidatePasswordMethod()
        {
            String un = "michael";
            String pw = "qqq";
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            Assert.IsNotNull("connStr should not be null", connStr);
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "select pword from UserCredentials where uname = @email";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@email", un);
                    string pwd = (string)cmd.ExecuteScalar();

                    Assert.IsTrue(PasswordHash.PasswordHash.ValidatePassword(pw, pwd));
                }
                catch (Exception ex)
                {
                    
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
