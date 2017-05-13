using System;
using System.Net.Http;

namespace ConsoleApplication
{
    public class MojoGrab
    {
        //set our client to be reused
        private static HttpClient Client = new HttpClient();
        public static void Main(string[] args)
        {
            var movieresult = Client.GetStringAsync("http://www.boxofficemojo.com/data/js/moviegross.php?id=starwars3.htm&shortgross=0").Result;
            Console.WriteLine(movieresult);
            
        }
    }
}