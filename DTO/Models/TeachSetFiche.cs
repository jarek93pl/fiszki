﻿using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class TeachSetFiche
    {
        public TypeAnswear FirstTypeAnswear { get; set; }

        public int FirstTypeAnswearInt
        {
            get
            {
                return (int)FirstTypeAnswear;
            }
            set
            {
                FirstTypeAnswear = (TypeAnswear)value;
            }
        }
        public int Id { get; set; }
        public int IdSetFiche { get; set; }
        public string Name { get; set; }
        public string NameSet { get; set; }
        public int LimitTimeInSekSet { get; set; }
        public bool IsLimitTimeSet { get; set; }
        public DateTime DataCreated { get; set; }
        public List<TeachBag> teachBags { get; set; } = new List<TeachBag>();
        public List<SetFiche> AvailableSetFiches { get; set; } = new List<SetFiche>();
    }
}

