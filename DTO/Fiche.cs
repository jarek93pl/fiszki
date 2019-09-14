using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Enums;
namespace DTO
{
    public class Fiche
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string NameType { get; set; }
        public ContentType TypePrompt { get; set; }
        public string NameTypePrompt { get; set; }
        public int Id { get; set; }
        public List<FicheResponse> FicheResponses { get; set; }
}
}
