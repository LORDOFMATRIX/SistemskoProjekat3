using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yelp_fusion_api_server
{
    public class Restaurant : IComparable<Restaurant>
    {
        public string Name { get; set; }

        public int ReviewCount { get; set; }

        public float Rating { get; set; }

        public string Price { get; set; }

        public bool OpenNow { get; set; }

        public int CompareTo(Restaurant x)
        {
            return this.Price.CompareTo(x.Price);
        }
        //static List<Restaurant> sortRestaurants(List<Restaurant> restaurants)
        //{
        //    foreach(var restaurant in restaurants)
        //    {

        //    }
        //}
    }
}
