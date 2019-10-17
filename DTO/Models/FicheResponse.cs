using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class FicheResponse
    {
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [DisplayName("Rodzaj zawartości")]
        public ContentType TypePrompt { get; set; }
        public int? IdFile { get; set; }
        public int Id { get; set; }

        [DisplayName("Jest poprawną")]
        public bool IsCorect { get; set; }

        [DisplayName("Rodzaj zawartości")]
        public string ContentTypeToDispley
        {
            get => TypePrompt switch
            {
                (ContentType.Image) => "Obraz",
                ContentType.Movie => "Film",
                ContentType.Sound => "Muzyka",
                ContentType.Text => "Tekst",
                _ => ""
            };
            set
            {
                TypePrompt = value switch
                {
                    "Obraz" => ContentType.Image,
                    "Film" => ContentType.Movie,
                    "Muzyka" => ContentType.Sound,
                    "Tekst" => ContentType.Text
                };
            }
        }
    }
}
