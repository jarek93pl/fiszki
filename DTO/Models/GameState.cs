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
            TypeAnswer = game.TypeAnswer;
            LimitTimeSek = game.LimitTimeSek;
        }

        public bool IsMultiPlayer { get; set; }
        public int IdTeachSet { get; set; }
        public Fiche Fiche { get; set; }
        public TypeAnswer TypeAnswer { get; set; }
        public int LimitTimeSek { get; set; }
        public int IntTypeAnswer
        {
            get
            {
                return (int)TypeAnswer;
            }
            set
            {
                TypeAnswer = (TypeAnswer)value;
            }
        }

    }
}