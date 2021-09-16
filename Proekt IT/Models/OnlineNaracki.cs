using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proekt_IT.Models
{
    public class OnlineNaracki
    {
        [Key]
        public int id { get; set; }

        public string naracka { get; set; }

        public string adresa { get; set; }

        public int cena { get; set; }

        public OnlineNaracki()
        {
        }

        public OnlineNaracki(string naracka, string adresa, int cena)
        {
            this.naracka = naracka;
            this.adresa = adresa;
            this.cena = cena;
        }
        
    }
}