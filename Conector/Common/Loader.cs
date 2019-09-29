using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class Loader
    {
        Dictionary<string, int> cash = new Dictionary<string, int>();
        public SqlDataReader reader;
        private int GetId(string value)
        {
            if (cash.TryGetValue(value, out int index))
            {
                return index;
            }
            else
            {
                int id = reader.GetOrdinal(value);
                cash.Add(value, id);
                return id;
            }
        }
        public Loader(SqlDataReader reader)
        {
            this.reader = reader;
        }
        public string GetString(string name)
        {
            return reader.GetString(GetId(name));
        }
        public int GetInt(string name)
        {
            return reader.GetInt32(GetId(name));
        }
        public int? GetIntNullable(string name)
        {
            if (!reader.IsDBNull(GetId(name)))
            {
                return reader.GetInt32(GetId(name));
            }
            else
            {
                return null;
            }
        }



    }
}
