using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class EditOrderDto
    {
        //[Required]
        //public int ID { get; set; }
        //[Required]
        //public string UserID { get; set; }
        //[Required]
        //public int RestaurantID { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public decimal OrderTotal { get; set; }
        [Required]
        public DateTime Updated_at { get; set; }
    }
}
