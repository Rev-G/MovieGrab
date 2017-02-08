using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;

namespace MovieGrab
{
    class Program
    {
        static void Main(string[] args)
        {
      try {
                //http://www.boxofficemojo.com/data/js/moviegross.php?id=starwars3.htm&shortgross=0
                string MovieName = "";
                string MovieID = "";
                string MovieURL = "";
                WebClient client = new WebClient();
                string regExPat = @"(?!<b>)(\$.*)(?=<\/b>)";
                Regex regEx = new Regex(regExPat);
                SqlConnection SqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);
                SqlConn.Open();

                foreach (var key in ConfigurationManager.AppSettings.AllKeys)
                {
                    MovieName = key;
                    MovieID = ConfigurationManager.AppSettings[key];
                    MovieURL = String.Format("http://www.boxofficemojo.com/data/js/moviegross.php?id={0}.htm&shortgross=0", MovieID);
                    string MovieData = client.DownloadString(MovieURL);
                    SqlCommand SqlCmd = new SqlCommand(String.Format("SELECT * FROM MovieInfo WHERE (MovieName = '{0}')", MovieName),SqlConn);
                    SqlDataReader SqlDR = SqlCmd.ExecuteReader();

                    if (SqlDR.HasRows)
                     {
                        SqlDR.Close();
                        SqlCmd.CommandText = String.Format("UPDATE MovieInfo SET GrossAmount = '{1}' WHERE MovieName = '{0}'", MovieName , regEx.Match(MovieData).Value);
                        SqlCmd.Connection = SqlConn;
                        SqlCmd.ExecuteNonQuery();
                        SqlCmd.Dispose();

                     }
                     else
                     {
                        SqlDR.Close();
                        SqlCmd.CommandText = String.Format("INSERT INTO MovieInfo (MovieName, GrossAmount) values ('{0}','{1}')", MovieName, regEx.Match(MovieData).Value);
                        SqlCmd.Connection = SqlConn;
                        SqlCmd.ExecuteNonQuery();
                        SqlCmd.Dispose();
                    }

                }

                SqlConn.Close();

            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
