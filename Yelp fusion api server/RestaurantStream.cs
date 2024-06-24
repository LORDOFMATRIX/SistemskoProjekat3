using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Subjects;


namespace Yelp_fusion_api_server
{
    public class RestaurantStream : IObservable<Restaurant>
    {
        private readonly Subject<Restaurant> restaurantsSubject = new Subject<Restaurant>();
        private readonly RestaurantsService restaurantsService = new RestaurantsService();
        public async Task FetchRestaurantsAsync(string location, string[] parameters)
        {
            try
            {

                var restaurants = await restaurantsService.GetRestaurantsAsync(location,parameters);
                foreach (var restaurant in restaurants)
                {
                    restaurantsSubject.OnNext(restaurant);
                }
                restaurantsSubject.OnCompleted();
                return;

            }
            catch (Exception ex)
            {
                restaurantsSubject.OnError(ex);
            }
        }



        public IDisposable Subscribe(IObserver<Restaurant> observer)
        {
            return restaurantsSubject.Subscribe(observer);
        }

    }
}
