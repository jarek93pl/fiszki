using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class User : BaseConector
    {
        public int Autorize(string name, string password)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("@Login", name);
            keyValuePairs.Add("@password", password);
            return LoadInt("AutorizeUser", keyValuePairs);
        }
        public int AddUser(string name, string password)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("@Login", name);
            keyValuePairs.Add("@password", password);
            return Convert.ToInt32(LoadDecimal("AddUser", keyValuePairs));
        }
    }
}
