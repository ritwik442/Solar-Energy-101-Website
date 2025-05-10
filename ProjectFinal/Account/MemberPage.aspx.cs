using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using WeatherLib;

namespace ProjectFinal
{
    public partial class MemberPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Empty for now
        }

        // Handle solar intensity calculation
        protected void btnGetSolarIntensity_Click(object sender, EventArgs e)
        {
            decimal latitude, longitude;
            if (decimal.TryParse(txtLatitude.Text, out latitude) && decimal.TryParse(txtLongitude.Text, out longitude))
            {
                // Validate coordinate ranges
                if (latitude < -90m || latitude > 90m)
                {
                    lblSolarResult.Text = "Latitude must be between -90 and 90 degrees.";
                    return;
                }

                if (longitude < -180m || longitude > 180m)
                {
                    lblSolarResult.Text = "Longitude must be between -180 and 180 degrees.";
                    return;
                }

                decimal avgIntensity = SolarIntensity(latitude, longitude);
                if (avgIntensity != -1)
                    lblSolarResult.Text = $"Average Daily Solar Intensity: {avgIntensity} kWh/m²/day";
                else
                    lblSolarResult.Text = "No valid solar data available.";
            }
            else
            {
                lblSolarResult.Text = "Invalid input. Please enter numeric values.";
            }
        }

        // Calculate solar intensity from NASA API or fallback
        public decimal SolarIntensity(decimal latitude, decimal longitude)
        {
            try
            {
                // Solar Data API Endpoint
                // The start date and end date basically span the year 2023
                string apiUrl = $"https://power.larc.nasa.gov/api/temporal/daily/point?parameters=ALLSKY_SFC_SW_DWN&community=RE&longitude={longitude}&latitude={latitude}&start=20230101&end=20231231&format=JSON";

                using (WebClient client = new WebClient())
                {
                    string jsonData = client.DownloadString(apiUrl);
                    var data = JObject.Parse(jsonData);

                    decimal total = 0;
                    int count = 0;
                    
                    // Extracting daily values
                    var dailyValues = data["properties"]["parameter"]["ALLSKY_SFC_SW_DWN"]; // All Sky Surface Shortwave Downward Irradiance 

                    foreach (var day in dailyValues)
                    {
                        if (day is JProperty dayProp)
                        {
                            decimal value = dayProp.Value.Value<decimal>();
                            if (value != -999) // Skip missing data (NASA uses this convention)
                            {
                                total += value;
                                count++;
                            }
                        }
                    }

                    return count > 0 ? Math.Round(total / count, 2) : -1; // -1 if no valid data
                }
            }
            catch
            {
                // Simplified calculations as a fallback
                // To check if this function is always being fallbacked onto, you can replace the return value with some number and see if the answer in the web page is always that number
                // I have verified that the fallback is not used for correct input
                decimal baseIntensity = 4.5m;
                decimal latitudeFactor = 1 - (decimal)(Math.Abs((double)latitude) / 90 * 0.5);
                return Math.Round(baseIntensity * latitudeFactor, 2);
            }
        }

        // Get weather forecast for ZIP code
        protected void btnWeatherTryIt_Click(object sender, EventArgs e)
        {
            string zip = txtZip.Text.Trim();
            List<string> forecast = GetForecastByZip(zip);

            if (forecast.Count > 0)
            {
                lblSolarResult.Text = "Weather Forecast:<br/>" + string.Join("<br/>", forecast);
            }
            else
            {
                lblSolarResult.Text = "No forecast available.";
            }
        }

        // Fetch forecast with caching
        public List<string> GetForecastByZip(string zip)
        {
            if (!WeatherHelper.IsValidZip(zip))
                return new List<string> { "Invalid ZIP Code." };

            // Check application cache first
            var cache = Context.Application["WeatherCache"] as Dictionary<string, List<string>>;
            if (cache == null)
            {
                cache = new Dictionary<string, List<string>>();
                Context.Application["WeatherCache"] = cache;
            }

            if (cache.ContainsKey(zip))
                return cache[zip];

            try
            {
                string apiKey = "9e990aa178c7ce5b48daed30a63d2433";
                var url = $"https://api.openweathermap.org/data/2.5/forecast?zip={zip},us&appid={apiKey}&units=imperial";

                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    var json = JObject.Parse(response);
                    var result = new List<string>();
                    var dates = new HashSet<string>();

                    // Parse 5-day forecast
                    foreach (var item in json["list"])
                    {
                        var dt_txt = item["dt_txt"]?.ToString();
                        if (dt_txt == null) continue;

                        var date = dt_txt.Split(' ')[0];
                        if (dates.Contains(date)) continue;

                        var temp = item["main"]?["temp"]?.ToString();
                        var desc = item["weather"]?[0]?["description"]?.ToString();

                        if (temp != null && desc != null)
                        {
                            result.Add($"{date}: {desc}, {temp}°F");
                            dates.Add(date);
                        }

                        if (result.Count >= 5)
                            break;
                    }

                    if (result.Count == 0)
                        result.Add("No forecast data available.");

                    // Cache the result
                    cache[zip] = result;
                    Context.Application["WeatherCache"] = cache;
                    return result;
                }
            }
            catch (WebException ex)
            {
                return new List<string> { "Web Error: " + ex.Message };
            }
            catch (Exception ex)
            {
                return new List<string> { "Error: " + ex.Message };
            }
        }

        // Async version of weather forecast
        private async Task<List<string>> GetOpenWeatherForecast(string zip)
        {
            string apiKey = "bd5e378503939ddaee76f12ad7a97608";
            var url = $"https://api.openweathermap.org/data/2.5/forecast?zip={zip},us&appid={apiKey}&units=imperial";

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);
                var json = JObject.Parse(response);
                var result = new List<string>();
                var dates = new HashSet<string>();

                // Similar parsing as synchronous version
                foreach (var item in json["list"])
                {
                    var dt_txt = item["dt_txt"]?.ToString();
                    if (dt_txt == null) continue;

                    var date = dt_txt.Split(' ')[0];
                    if (dates.Contains(date)) continue;

                    var temp = item["main"]?["temp"]?.ToString();
                    var desc = item["weather"]?[0]?["description"]?.ToString();

                    if (temp != null && desc != null)
                    {
                        result.Add($"{date}: {desc}, {temp}°F");
                        dates.Add(date);
                    }

                    if (result.Count >= 5)
                        break;
                }

                return result.Count > 0 ? result : new List<string> { "No forecast data available." };
            }
        }

        // Fetch news for given topic
        protected void btnGetNews_Click(object sender, EventArgs e)
        {
            string topic = txtTopic.Text;
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["NewsApiKey"];
            string url = $"https://newsdata.io/api/1/news?apikey={apiKey}&q={topic}&language=en";

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    var serializer = new JavaScriptSerializer();
                    var result = serializer.Deserialize<Dictionary<string, object>>(response);

                    // Build HTML list of news titles
                    if (result.ContainsKey("results"))
                    {
                        var newsList = (ArrayList)result["results"];
                        string output = "<ul>";

                        foreach (var item in newsList)
                        {
                            var news = (Dictionary<string, object>)item;
                            if (news.ContainsKey("title"))
                            {
                                output += $"<li>{news["title"]}</li>";
                            }
                        }

                        output += "</ul>";
                        lblSolarResult.Text = output;
                    }
                    else
                    {
                        lblSolarResult.Text = "No news found.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSolarResult.Text = $"Error: {ex.Message}";
            }
        }

        // Validate ZIP code using DLL
        protected void btnZipTryIt_Click(object sender, EventArgs e)
        {
            string zip = txtZip.Text.Trim();
            bool isValid = WeatherHelper.IsValidZip(zip);

            lblSolarResult.Text = isValid ? "ZIP code is valid." : "Invalid ZIP code.";
        }

        // Handle user logout
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
}