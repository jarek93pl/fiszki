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
        public bool AnySetsFicheExist(int UserId)
        {
            return LoadInt("AnySetsFicheExist", new Dictionary<string, object>() { { "UserId", UserId } }) == 1;
        }
        public int AddSetFiche(string name, int userId)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("@Name", name);
            keyValuePairs.Add("@UserId", userId.ToString());
            return Convert.ToInt32(LoadInt("AddSetFiche", keyValuePairs).ToString());
        }
        public List<DTO.Models.SetFiche> SearchSetsFiche(int? userId, int? id)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            if (userId.HasValue)
            {
                keyValuePairs.Add("@UserId", userId.ToString());
            }
            if (id.HasValue)
            {
                keyValuePairs.Add("@SetFicheId", id.ToString());
            }
            return LoadList<DTO.Models.SetFiche>("SearchSetsFiche", keyValuePairs, ReaderListSetsFiche);
        }

        private DTO.Models.SetFiche ReaderListSetsFiche(Loader arg)
        {
            DTO.Models.SetFiche set = new DTO.Models.SetFiche();
            set.Name = arg.GetString("Name");
            set.UserName = arg.GetString("UserName");
            set.UserId = arg.GetInt("UserId");
            set.Id = arg.GetInt("id");
            return set;
        }
        public int Remove(int elementId)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("@elementId", elementId.ToString());
            return Convert.ToInt32(LoadInt("RemoveSetFiche", keyValuePairs).ToString());
        }
    }
}



