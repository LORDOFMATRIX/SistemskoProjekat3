using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Yelp_fusion_api_server
{
    public class RestaurantsObserver : IObserver<Restaurant>
    {
        private readonly string name;
        private List<Restaurant> restaurants = new List<Restaurant>();

        public RestaurantsObserver(string name)
        {
            this.name = name;
        }
        public void OnNext(Restaurant restaurant)
        {
            Console.WriteLine($"{name} Pribavio sam podatke: Restoran: {restaurant.Name}, Rejting: {restaurant.Rating}," +
                $" Cena: {restaurant.Price}, Otvoren: {restaurant.OpenNow}, BrojRecenzija: {restaurant.ReviewCount} " +
                $"na niti: {Environment.CurrentManagedThreadId}");
            if (!restaurants.Contains(restaurant))
                restaurants.Add(restaurant);
            Console.Out.Flush();
        }

        public void OnError(Exception e)
        {
            Console.WriteLine($"{name}: Došlo je do greške!: {e.Message}");
            
        }

        public void OnCompleted()
        {
            Console.WriteLine($"{name}: Svi restorani su pribavljeni na niti: {Environment.CurrentManagedThreadId}");
            restaurants.Sort();
            foreach (var restaurant in restaurants)
            {
                if(restaurant.Price.Equals("$$") || restaurant.Price.Equals("$$$"))
                {
                Console.WriteLine($"{name}: Sortirani podaci su: Restoran: {restaurant.Name}, Rejting: {restaurant.Rating}," +
                    $" Cena: {restaurant.Price}, Otvoren: {restaurant.OpenNow}, BrojRecenzija: {restaurant.ReviewCount} " +
                $"na niti: {Environment.CurrentManagedThreadId}");
                }
            }
            Console.Out.Flush();
            
        }
    }
}
