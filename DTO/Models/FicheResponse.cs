using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class FicheResponse
    {
        public string Name { get; set; }
        public ContentType TypePrompt { get; set; }
        public int? IdFile { get; set; }
        public int Id { get; set; }
        public bool IsCorect { get; set; }

        public string ContentTypeToDispley
        {
            get
            {
                return ((int)TypePrompt).ToString();
            }
            set
            {

                TypePrompt = (ContentType)Convert.ToInt32(value);
            }
        }
    }
}
