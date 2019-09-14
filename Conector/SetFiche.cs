using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class SetFiche : BaseConector
    {
        public int AddSetFiche(string name, int userId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("@Name", name);
            keyValuePairs.Add("@UserId", userId.ToString());
            return Convert.ToInt32(LoadInt("AddSetFiche", keyValuePairs).ToString());
        }
        public List<DTO.SetFiche> SearchSetsFiche(int? userId, int? id)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            if (userId.HasValue)
            {
                keyValuePairs.Add("@UserId", userId.ToString());
            }
            if (id.HasValue)
            {
                keyValuePairs.Add("@SetFicheId", id.ToString());
            }
            return LoadList<DTO.SetFiche>("SearchSetsFiche", keyValuePairs, ReaderListSetsFiche);
        }

        private DTO.SetFiche ReaderListSetsFiche(Loader arg)
        {
            DTO.SetFiche set = new DTO.SetFiche();
            set.Name = arg.GetString("Name");
            set.UserName = arg.GetString("UserName");
            set.UserId = arg.GetInt("UserId");
            set.Id = arg.GetInt("id");
            return set;
        }
        public int Remove(int elementId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("@elementId", elementId.ToString());
            return Convert.ToInt32(LoadInt("RemoveSetFiche", keyValuePairs).ToString());
        }
    }
}



