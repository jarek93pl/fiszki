using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class Comon : BaseConector
    {
        public int SaveFile(string extension)
        {
            return LoadInt("SaveFile", new Dictionary<string, object>() { { "FileExtension", extension } });

        }
        public string GetExtension(int id)
        {
            return LoadString("GetExtension", new Dictionary<string, object>() { { "id", id.ToString() } });
        }
    }
}
