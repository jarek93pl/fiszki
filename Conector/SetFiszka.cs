using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class SetFiszka : BaseConector
    {
        public int AddSetFiszka(string name)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("@Name", name);
            string returned=LoadDecimal("AddSetFiszka", keyValuePairs).ToString();
            return Convert.ToInt32(returned);
        }
    }
}



