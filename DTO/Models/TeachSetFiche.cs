using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class TeachSetFiche
    {

        [DisplayName("Początkowy rodzaj gry")]
        public TypeAnswer FirstTypeAnswer { get; set; }

        public int FirstTypeAnswerInt
        {
            get
            {
                return (int)FirstTypeAnswer;
            }
            set
            {
                FirstTypeAnswer = (TypeAnswer)value;
            }
        }
        public int Id { get; set; }

        [DisplayName("Zestaw z fiszkami")]
        public int IdSetFiche { get; set; }

        [DisplayName("Nazwa uczenia")]
        public string Name { get; set; }

        [DisplayName("Nazwa zestawu")]
        public string NameSet { get; set; }

        [DisplayName("Limit czasu s")]
        public int LimitTimeInSekSet { get; set; }

        [DisplayName("Limit czasu")]
        public bool IsLimitTimeSet { get; set; }

        [DisplayName("Data stworzenia")]
        public DateTime DataCreated { get; set; }
        public List<TeachBag> teachBags { get; set; } = new List<TeachBag>();
        public List<SetFiche> AvailableSetFiches { get; set; } = new List<SetFiche>();
    }
}

