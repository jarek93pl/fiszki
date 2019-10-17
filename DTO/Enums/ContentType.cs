using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTO.Enums
{
    public enum ContentType
    {

        [Display(Name = "tekst")]
        Text = 0,
        [Display(Name = "obraz")]
        Image = 1,
        [Display(Name = "dźwiek")]
        Sound = 2,
        [Display(Name = "film")]
        Movie = 3
    }
}