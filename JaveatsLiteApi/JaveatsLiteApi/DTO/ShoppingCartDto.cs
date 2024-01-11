using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JaveatsLiteApi.Models;
namespace JaveatsLiteApi.DTO
{
    public class ShoppingCartDto
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
