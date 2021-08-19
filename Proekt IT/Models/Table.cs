using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proekt_IT.Models
{
    public class Table
    {
        private RestaurantContext db = new RestaurantContext();

        [Key]
        public int id { get; set; }

        [DisplayName("Нарачка")]
        public string orders { get; set; }

        [DisplayName("Број на гости")]
        public int brojNaGosti { get; set; }

        [DisplayName("Моментална сума за плаќање")]
        public int sum { get; set; }

        public Table()
        {
        }
    }
}