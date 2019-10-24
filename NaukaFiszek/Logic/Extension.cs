using DTO.Enums;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NaukaFiszek.Logic
{
    public static class Extension
    {
        public static string EventContentText(object obj)
        {
            var sb = new StringBuilder();
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var serializedObject = ser.Serialize(obj);
            sb.AppendFormat("data: {0}\n\n", serializedObject);
            return sb.ToString();

        }
        public static bool Can(this TypeAnswer typeAnswer, Fiche fiche)
        {
            switch (typeAnswer)
            {
                case TypeAnswer.WriteTextUserChose:
                case TypeAnswer.UserChose:
                case TypeAnswer.WriteText:
                    return true;
                case TypeAnswer.ChoseOption:
                    return fiche.Response.Length > 0;
                case TypeAnswer.Hangman:
                    return fiche.Response.All(X => HangmanGameState.Alphabet.Any(Y => X == Y));
                default: return true;
            }
        }

    }
}