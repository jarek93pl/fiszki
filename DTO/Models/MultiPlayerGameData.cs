using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class MultiPlayerGameData
    {
        [DisplayName("Rodzaj gry")]
        public TypeAnswerMultiGame TypeAnswer { get; set; }

        [DisplayName("Limit czasu")]
        public bool IsLimitTime { get; set; }

        [DisplayName("Limit czasu s")]
        public int LimitTimeInSek { get; set; }

        [DisplayName("Rodzaj gry")]
        public int TypeAnswerInt
        {
            get
            {
                return (int)TypeAnswer;
            }
            set
            {
                TypeAnswer = (TypeAnswerMultiGame)value;
            }
        }

        [DisplayName("Zestaw z fiszkami")]
        public int IdSetFiche { get; set; }


        [DisplayName("Zestaw fiszek")]
        public List<SetFiche> AvailableSetFiches { get; set; } = new List<SetFiche>();
    }
}
