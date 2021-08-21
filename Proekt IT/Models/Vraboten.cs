using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proekt_IT.Models
{
    public class Vraboten
    {
        [Key]
        public string email { get; set; }

        public int promet { get; set; }

        public TimeSpan najava { get; set; }

        public TimeSpan odjava { get; set; }

        public TimeSpan raboteno { get; set; }

        public Vraboten()
        {
        }

        public Vraboten(string email, TimeSpan najava, TimeSpan odjava, TimeSpan raboteno, int promet)
        {
            this.raboteno = raboteno;
            this.odjava = odjava;
            this.najava = najava;
            this.email = email;
            this.promet = promet;
        }

        public void dodajPromet(int kolku) {
            this.promet += kolku;
        }

    }
}