using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class AddToShoppingCartDto
    {
        public int itemId { get; set; }
        public int menuId { get; set; }
        public int quantity { get; set; }
    }
}
