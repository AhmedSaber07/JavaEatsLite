using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class RemoveFromShoppingCartDto
    {
        public int itemId { get; set; }
        public int restaurantId { get; set; }
        public int? quantity { get; set; }
    }
}
