using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Enums;
namespace DTO.Models
{
    public class Fiche
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
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
        public string NameTypePrompt { get; set; }
        public int Id { get; set; }
        public int? IdPromptFile { get; set; }
        public int IdFicheSet { get; set; }
        public FicheResponse[] FicheResponses { get; set; }
    }
}
