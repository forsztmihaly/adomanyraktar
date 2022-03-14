using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raktar.Models
{
    public class Kereses
    {
        public string ElnevKeres { get; set; }
        public string KategoriaKeres { get; set; }

        public SelectList KategoriaLista { get; set; }
        public List<Adomany> AdomanyLista { get; set; }
    }
}
