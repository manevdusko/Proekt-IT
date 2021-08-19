using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proekt_IT.Models
{
    public class Naracka
    {
        public Table table { get; set; }
        public List<Menu>  menu { get; set; }
        public Naracka()
        {
            menu = new List<Menu>();
        }
    }
}