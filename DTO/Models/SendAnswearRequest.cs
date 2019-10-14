using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO.Models
{
    public class SendAnswearRequest
    {
        public int idTeachSet { get; set; }
        public int IdFiche { get; set; }
        public bool IsCorrect { get; set; }
    }
}