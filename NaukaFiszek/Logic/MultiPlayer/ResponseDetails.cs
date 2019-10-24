using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Logic.MultiPlayer
{
    public class ResponseDetails
    {
        public int TimeAnsweringMilisecond { get; set; }
        public bool IsCorrect { get; set; }
        public int IdFiche { get; set; }
        public override int GetHashCode()
        {
            return IdFiche;
        }
        public override bool Equals(object obj)
        {
            if (obj is ResponseDetails responseDetailsOther)
            {
                return responseDetailsOther.IdFiche == IdFiche;
            }
            return false;
        }
    }
}