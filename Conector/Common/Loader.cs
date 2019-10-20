using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    [DebuggerStepThrough]
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
            try
            {
                return reader.GetString(GetId(name));
            }
            catch (Exception ex)
            {
                throw new Exception(name, ex);
            }
        }
        public int GetInt(string name)
        {
            try
            {
                return reader.GetInt32(GetId(name));
            }
            catch (Exception ex)
            {
                throw new Exception(name, ex);
            }
        }
        public bool GetBoolen(string name)
        {
            try
            {
                return reader.GetBoolean(GetId(name));
            }
            catch (Exception ex)
            {
                throw new Exception(name, ex);
            }
        }
        public DateTime GetDate(string name)
        {
            try
            {
                return reader.GetDateTime(GetId(name));
            }
            catch (Exception ex)
            {
                throw new Exception(name, ex);
            }
        }
        public int? GetIntNullable(string name)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(name, ex);
            }
        }
    }
}
