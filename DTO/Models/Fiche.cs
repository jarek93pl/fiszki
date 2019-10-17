using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Enums;
namespace DTO.Models
{
    public class Fiche
    {
        [DisplayName("Podpowiedź")]
        public string Prompt { get; set; }

        [DisplayName("Odpowiedź")]
        public string Response { get; set; }

        [DisplayName("Rodzaj podpowiedzi")]
        public ContentType TypePrompt { get; set; }
        public int IntTypePrompt {
            get
            {
                return (int)TypePrompt;
            }
            set
            {
                TypePrompt = (ContentType)value;
            }
        }
        public string TypePromptString
        {
            get
            {
                return TypePrompt.ToString();
            }
            set
            { 
            }
        }
        [DisplayName("Rodzaj podpowiedzi")]
        public string NameTypePrompt { get; set; }
        public int Id { get; set; }

        [DisplayName("Wgrywany plik")]
        public int? IdPromptFile { get; set; }
        public int IdFicheSet { get; set; }
        public FicheResponse[] FicheResponses { get; set; }
    }
}
