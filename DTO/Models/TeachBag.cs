using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class TeachBag
    {
        public TypeAnswer TypeAnswer { get; set; }

        public int TypeAnswerInt
        {
            get
            {
                return (int)TypeAnswer;
            }
            set
            {
                TypeAnswer = (TypeAnswer)value;
            }
        }

        [DisplayName("Czas do następnego powtórzenia (hh:mm:ss)")]
        public TimeSpan PeriodTime { get; set; }

        [DisplayName("Rodzaj gry")]
        public string TypeAnswerText
        {
            get
            {
                switch (TypeAnswer)
                {
                    case TypeAnswer.UserChose:
                        return "urzytkownik decyduje";
                    case TypeAnswer.WriteText:
                        return "sprawdzanie poprawności tekstu";
                    case TypeAnswer.ChoseOption:
                        return "Wybieranie z odpowiedzi";
                    case TypeAnswer.WriteTextUserChose:
                        return "urzytkownik decyduje ale może pisać text";
                    case TypeAnswer.Hangman:
                        return "wisielec";
                    default: return "";
                }
            }
            set
            {

            }
        }

        [DisplayName("Limit czasu")]
        public bool IsLimitTime { get; set; }

        [DisplayName("Limit czasu s")]
        public int LimitTimeInSek { get; set; }
        public int Id { get; set; }
    }
}
