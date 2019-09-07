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
        public int AddSetFiszka(string name, int userId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("@Name", name);
            keyValuePairs.Add("@UserId", userId.ToString());
            return Convert.ToInt32(LoadInt("AddSetFiszka", keyValuePairs).ToString());
        }
        public List<DTO.SetFiszka> SearchSetsFiszka(int userId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("@UserId", userId.ToString());
            return LoadList<DTO.SetFiszka>("SearchSetsFiszka", keyValuePairs, ReaderListSetsFiszka);
        }

        private DTO.SetFiszka ReaderListSetsFiszka(Loader arg)
        {
            DTO.SetFiszka set = new DTO.SetFiszka();
            set.Name = arg.GetString("Name");
            set.UserName = arg.GetString("UserName");
            set.UserId = arg.GetInt("UserId");
            set.Id = arg.GetInt("id");
            return set;
        }
        public int Remove( int elementId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("@elementId", elementId.ToString());
            return Convert.ToInt32(LoadInt("RemoveSetFiszka", keyValuePairs).ToString());
        }
    }
}



