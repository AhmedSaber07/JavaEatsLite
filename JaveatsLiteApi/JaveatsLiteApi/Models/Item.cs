using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPreferred { get; set; }
        public int MenuID { get; set; }
    }
}
