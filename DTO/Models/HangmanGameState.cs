using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class HangmanGameState : GameState
    {
        public HangmanGameState()
        {

        }
        public HangmanGameState(GameState game) : base(game)
        {
            base.Fiche.Response = base.Fiche.Response.ToLower().Replace(' ', '_');
        }
        public bool IsHangman
        {
            get
            {
                return Fiche.Response.All(X => Alphabet.Any(Y => X == Y));
            }
        }
        public static readonly IReadOnlyCollection<char> Alphabet = new List<char>() { 'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'r', 's', 'ś', 't', 'u', 'w', 'y', 'z', 'ź', 'ż', '_' }.AsReadOnly();

    }
}
