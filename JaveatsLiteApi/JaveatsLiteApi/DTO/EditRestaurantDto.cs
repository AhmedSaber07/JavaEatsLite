using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class EditRestaurantDto
    {
        [Required]
        [MinLength(10), MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [MinLength(10), MaxLength(200)]
        public string Location { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
