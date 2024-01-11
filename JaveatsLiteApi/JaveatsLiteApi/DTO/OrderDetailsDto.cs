using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class OrderDetailsDto
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime Date { get; set; }
    }
}
