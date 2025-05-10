using System.Text.RegularExpressions;

namespace WeatherLib
{
    public static class WeatherHelper
    {
        // Returns true if ZIP is exactly 5 digits
        public static bool IsValidZip(string zip)
        {
            return Regex.IsMatch(zip, @"^\d{5}$");
        }
    }
}
