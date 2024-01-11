using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int? RestaurantId { get; set; }

        public string ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public  Item Item { get; set; }
    }
}
