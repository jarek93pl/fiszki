using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class NextFiche
    {
        public int LimitTimeSek { get; set; }
        public int IdFiche { get; set; }
        public int IdTeachSet { get; set; }
        public TypeAnswer TypeAnswer { get; set; }


    }
}
