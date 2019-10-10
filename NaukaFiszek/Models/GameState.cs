using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Models
{
    public class GameState
    {
        public bool IsMultiPlayer { get; set; }
        public DTO.Fiche Fiche { get; set; }
    }
}