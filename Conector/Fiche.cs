using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class Fiche : BaseConector
    {
        public List<DTO.Fiche> SearchFiches(int SetFicheId)
        {
            return LoadList<DTO.Fiche>("SearchFiches", new Dictionary<string, string>() { { "@SetFicheId", SetFicheId.ToString() } }, Reader);
        }
        private DTO.Fiche Reader(Loader arg)
        {
            DTO.Fiche returned = new DTO.Fiche();
            returned.Prompt = arg.GetString("Prompt");
            returned.Response = arg.GetString("Response");
            returned.NameTypePrompt = arg.GetString("NameTypePrompt");
            returned.Id = arg.GetInt("Id");
            returned.TypePrompt =(ContentType) arg.GetInt("TypePrompt");
            return returned;
        }
    }
}
