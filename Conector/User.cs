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
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("@Login", name);
            keyValuePairs.Add("@password", password);
            return LoadInt("AutorizeUser", keyValuePairs);
        }
        public int AddUser(string name, string password)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("@Login", name);
            keyValuePairs.Add("@password", password);
            return Convert.ToInt32(LoadDecimal("AddUser", keyValuePairs));
        }
    }
}
