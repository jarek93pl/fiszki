using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO.Models
{
    public class SetFicheDetails
    {
        public int IdSetFiche { get; set; }
        public string Name { get; set; }
        public List<Fiche> Fiches { get; set; }
    }
}