using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public int restaurantID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public List<Item> Items { get; set; }
    }
}
