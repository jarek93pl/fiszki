using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Enums
{
    public enum TypeAnswerMultiGame
    {
        [Display(Name = "Wybierz wartość")]
        None = 0,
        [Display(Name = "Napisz odpowiedź")]
        WriteText = TypeAnswer.WriteText,
        [Display(Name = "Wybierz odpowiedź")]
        ChoseOption = TypeAnswer.ChoseOption
    }
}
