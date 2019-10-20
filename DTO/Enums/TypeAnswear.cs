using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Enums
{
    public enum TypeAnswer
    {
        [Display(Name = "Urzytkownik określa")]
        UserChose = 0,
        [Display(Name = "Napisz odpowiedź")]
        WriteText = 1,
        [Display(Name = "Wybierz odpowiedź")]
        ChoseOption = 2,
        [Display(Name = "Napisz odpowiedź,i określ")]
        WriteTextUserChose = 3,
        [Display(Name = "Wisielec")]
        Hangman = 4


    }
}
