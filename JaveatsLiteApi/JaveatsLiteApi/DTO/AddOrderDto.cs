﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class AddOrderDto
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public int RestaurantID { get; set; }
    }
}
