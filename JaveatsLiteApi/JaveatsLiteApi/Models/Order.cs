using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Status { get; set; }
        public int RestaurantID { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
