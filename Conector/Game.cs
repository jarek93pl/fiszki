using DTO;
using DTO.Enums;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class Game : BaseConector
    {
        public NextFiche NextFiche(int idTeachSet)
        {
            return LoadList("NextFicheToTeach", new Dictionary<string, object>() { { "IdTeachSet", idTeachSet }, }, ReaderNextFiche).First();
        }

        private NextFiche ReaderNextFiche(Loader arg) => new NextFiche() { IdFiche = arg.GetInt("IdFiche"),IdTeachSet = arg.GetInt("IdTeachSet"),TypeAnswear =(TypeAnswear) arg.GetInt("TypeAnswear") };

        public int SendAnswear(int idTeachSet, int IdFiche, bool IsCorrect)
        {
            return LoadInt("SendAnswear", new Dictionary<string, object>() {
                { "IdTeachSet", idTeachSet },
                { "IdFiche", IdFiche },
                { "IsCorrect", IsCorrect }
            });
        }
    }
}
