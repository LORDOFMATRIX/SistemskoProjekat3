using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Concurrency;
using System.Net;

namespace Yelp_fusion_api_server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var restaurantStream = new RestaurantStream();
            var frank = new RestaurantsObserver("frank");
            var marco = new RestaurantsObserver("marco");
            var mainFilter = restaurantStream
                .ObserveOn(NewThreadScheduler.Default)
                .Where(restaurant => restaurant.Rating < 3 && restaurant.OpenNow == false && restaurant.ReviewCount > 1000);

            //var filter = restaurantStream
            //    .ObserveOn(TaskPoolScheduler.Default)
            //    .Where(restaurant => restaurant.Rating > 4.3);






            while (true)
            {
                Console.WriteLine("Unesite lokaciju");
                string location = Console.ReadLine();
                Console.WriteLine("Unesite kategorije odvojene zapetama (primer: french,italian)");
                string categories = Console.ReadLine();
                string[] categoriesSplit = categories.Split(',');
                mainFilter.Subscribe(frank);
                restaurantStream.Subscribe(marco);
                await restaurantStream.FetchRestaurantsAsync(location, categoriesSplit);
                Thread.Sleep(1000);
            }


            //var subcsription1 = mainFilter
            //    .Subscribe(observer2);

        }
    }
}
