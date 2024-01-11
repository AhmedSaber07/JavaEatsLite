using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
