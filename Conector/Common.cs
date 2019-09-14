using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class Common : BaseConector
    {
        public int SaveFile(string extension)
        {
            return LoadInt("SaveFile", new Dictionary<string, string>() { { "FileExtension", extension } });

        }
    }
}
