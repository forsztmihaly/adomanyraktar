using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace raktar.Models
{
    public class Adomany
    {
        public int Id { get; set; }

        [Display(Name = "Elnevezés")]
        [StringLength(60)]
        public string Elnevezes { get; set; }

        [Display(Name = "Kategória")]
        [StringLength(60)]
        public string Kategoria { get; set; }

        [Display(Name = "Csomagolási Egység")]
        [StringLength(30)]
        public string CsomEgyseg { get; set; }

        [Display(Name = "Darabszám")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Darab { get; set; }
    }
}
