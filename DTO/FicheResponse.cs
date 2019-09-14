using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FicheResponse
    {
        public string Name { get; set; }
        public ContentType TypePrompt { get; set; }
        public byte[] Data { get; set; }
    }
}
