using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class AddMenuDto
    {
        [Required]
        public int restaurantID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Created_at { get; set; }
    }
}
