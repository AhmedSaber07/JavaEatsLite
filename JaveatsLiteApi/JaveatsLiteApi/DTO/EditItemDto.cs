using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class EditItemDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPreferred { get; set; }
        public int MenuID { get; set; }
    }
}
