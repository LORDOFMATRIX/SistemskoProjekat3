using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;



namespace Yelp_fusion_api_server
{
    public class RestaurantsService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiKey = "NMwX04Tiz8CmQk3urrFlxsM2rQ1nDNWGlQBHVoaBeOSSYMKHKHj2FeCejNY8zIRILET23ItFM4Ynw_ghDs5jrIWjp1x0pq7l1p0QdSeo4QvgU0RXA6ufcd5ynkh1ZnYx";
        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string location, string[] categories)
        {
            string editedCategories = "";
            foreach(var category in categories)
            {
                editedCategories += ("categories="+category+"&");
            }
            var response = new HttpResponseMessage();
            using (var requestMessage =
            new HttpRequestMessage(HttpMethod.Get,
            $"https://api.yelp.com/v3/businesses/search?location={location}&term=restaurants&radius=10000&{editedCategories}open_now=false&sort_by=best_match&limit=50&offset=0"))
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiKey);
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = await client.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(content);
                var restaurantsJson = jsonResponse["businesses"];
                if (restaurantsJson == null)
                {
                    return Enumerable.Empty<Restaurant>();
                }
                return restaurantsJson.Select(restaurant => new Restaurant
                {

                    Name = (string)restaurant["name"],
                    ReviewCount = (int)restaurant["review_count"],
                    Rating = (float)restaurant["rating"],
                    Price = (string)restaurant["price"] == null ? " " : (string)restaurant["price"],
                    OpenNow = (bool)restaurant["is_closed"]

                });
            }
        }

        //public static async Task<IEnumerable<Restaurant>> Sort
    }
}
