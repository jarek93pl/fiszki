using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO.Models
{
    public class GameState
    {
        public GameState()
        {
        }
        public GameState(GameState game)
        {
            Fiche = game.Fiche;
            IdTeachSet = game.IdTeachSet;
            TypeAnswear = game.TypeAnswear;
            LimitTimeSek = game.LimitTimeSek;
        }

        public bool IsMultiPlayer
        {
            get
            {
                return false;
            }
        }
        public int IdTeachSet { get; set; }
        public Fiche Fiche { get; set; }
        public TypeAnswear TypeAnswear { get; set; }
        public int LimitTimeSek { get; set; }
        public int IntTypeAnswear
        {
            get
            {
                return (int)TypeAnswear;
            }
            set
            {
                TypeAnswear = (TypeAnswear)value;
            }
        }

    }
}