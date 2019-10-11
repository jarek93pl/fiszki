using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class Game : BaseConector
    {
        public int NextFiche(int idTeachSet)
        {
            return LoadInt("NextFicheToTeach", new Dictionary<string, object>() { { "IdTeachSet", idTeachSet } });
        }

        public int SendAnswear(int idTeachSet,int IdFiche,bool IsCorrect)
        {
            return LoadInt("SendAnswear", new Dictionary<string, object>() {
                { "IdTeachSet", idTeachSet },
                { "IdFiche", IdFiche },
                { "IsCorrect", IsCorrect }
            });
        }
    }
}
