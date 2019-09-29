using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Models
{
    public class SetFicheDetails
    {
        public int IdSetFiche { get; set; }
        public string Name { get; set; }
        public List<DTO.Fiche> Fiches { get; set; }
    }
}