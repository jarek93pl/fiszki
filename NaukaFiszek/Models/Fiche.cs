using NaukaFiszek.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Models
{
    public class Fiche
    {
        public string Prompt { get; set; }
        public string Answer { get; set; }
        public int Id { get; set; }
        public ContentType TypePrompt { get; set; }

    }
}