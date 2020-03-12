using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cw01
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var url = Console.ReadLine();
            if (url.Equals(""))
                throw new ArgumentNullException();
            if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                throw new ArgumentException();
            var httpClient = new HttpClient();
            try
            {
                var content = await httpClient.GetStringAsync(url);
                string result = string.Join("\n", ExtractEmails(content));
                var uniqueValues = result.AsEnumerable().ToHashSet();
                if(uniqueValues.Count==0)
                {
                    Console.WriteLine("Nie znaleziono adresów email");
                }
                else
                {
                    Console.WriteLine(string.Join("\n",uniqueValues.ToArray()));
                }
                httpClient.Dispose();
            }
            catch(Exception e)
            {
                Console.WriteLine("Błąd w trakcie pobierania strony");
            }
            

          
        }

        public static string[] ExtractEmails(string str)
        {
            string RegexPattern = @"\b[A-Z0-9._-]+@[A-Z0-9][A-Z0-9.-]{0,61}[A-Z0-9]\.[A-Z.]{2,6}\b";
            System.Text.RegularExpressions.MatchCollection matches =
                System.Text.RegularExpressions.Regex.Matches(str, RegexPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            string[] MatchList = new string[matches.Count];

            int c = 0;
            foreach(System.Text.RegularExpressions.Match match in matches)
            {
                MatchList[c++] = match.ToString();
            }

            return MatchList;
        }
    }
}
