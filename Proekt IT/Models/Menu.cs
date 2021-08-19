using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proekt_IT.Models
{
    public class Menu
    {
        [Key]
        public int id { get; set; }

        [DisplayName("Име на производ")]
        public string imeNaJadenje { get; set; }

        [DisplayName("Состојќи")]
        public string sostojki { get; set; }

        [DisplayName("Цена")]
        public int cena { get; set; }
    }
}