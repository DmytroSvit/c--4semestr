using System;
using System.Text.RegularExpressions;

namespace Praktyka1
{
    class Validation
    {
        public static string ID(string id)
        {
            Regex regex = new Regex(@"^\d{8}$");

            if (regex.IsMatch(id))
                return id;
            else
                return null;
           
        }

        public static string Url(string url)
        {
            if (!CheckURLValid(url))
                return null;
            else
                return url;
        }

        public static double Price(double price)
        {
            if (price >= 0)
                return price;
            else
                return 0;
        }

        public static string TransactionNumber(string number)
        {
            Regex regex = new Regex(@"^[A-Z]{2}-\d{3}-[A-Z]{2}/\d{2}$");

            if (regex.IsMatch(number))
                return number;
            else
                return null;
        }

        public static bool CheckURLValid( string source) => Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;
    }
}
