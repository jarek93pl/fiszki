using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TeachBag
    {
        public TypeAnswear TypeAnswear { get; set; }

        public int TypeAnswearInt
        {
            get
            {
                return (int)TypeAnswear;
            }
            set
            {
                TypeAnswear = (TypeAnswear)value;
            }
        }
        public TimeSpan PeriodTime { get; set; }
        public string TypeAnswearText
        {
            get
            {
                switch (TypeAnswear)
                {
                    case TypeAnswear.UserChose:
                        return "urzytkownik decyduje";
                    case TypeAnswear.WriteText:
                        return "sprawdzanie poprawności tekstu";
                    case TypeAnswear.ChoseOption:
                        return "Wybieranie z odpowiedzi";
                    case TypeAnswear.WriteTextUserChose:
                        return "urzytkownik decyduje ale może pisać text";
                    case TypeAnswear.Hangman:
                        return "wisielec";
                    default: return "";
                }
            }
            set
            {

            }
        }
        public bool IsLimitTime { get; set; }
        public int LimitTimeInSek { get; set; }
        public int Id { get; set; }
    }
}
