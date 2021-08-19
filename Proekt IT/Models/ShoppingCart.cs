using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proekt_IT.Models
{
    public class ShoppingCart
    {
        [Key]
        public int id { get; set; }

        public int productId { get; set; }
    }
}