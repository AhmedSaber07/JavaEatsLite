using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int Quantity{ get; set; }
        public decimal Price { get; set; }
        public decimal OrderTotal { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
