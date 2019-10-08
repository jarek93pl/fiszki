using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;

namespace Conector
{
    [DebuggerStepThrough]
    public class BaseConector : IDisposable
    {
        SqlConnection conection;
        public BaseConector()
        {
            conection = new SqlConnection(conectionString);
            conection.Open();

        }
        public static string conectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
            }
        }
        protected void BaseFunction(string name, Dictionary<string, object> parameters, Action<SqlDataReader> functionBody)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(name, conection);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }
                SqlDataReader dr = cmd.ExecuteReader();
                functionBody(dr);
                dr.Close();
            }
            catch (SqlException e)
            {
                Debug.WriteLine("Error Generated. Details: " + e.ToString());
                throw new Exception($"funkcja {name}",e);
            }
            finally
            {
            }
        }
        protected int LoadInt(string name, Dictionary<string, object> parameters)
        {
            int value = 0;
            BaseFunction(name, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    value = reader.GetInt32(0);
                }
            });
            return value;
        }
        protected void NonReturned(string name, Dictionary<string, object> parameters) => BaseFunction(name, parameters, (reader) => { });
        protected decimal LoadDecimal(string name, Dictionary<string, object> parameters)
        {
            decimal value = 0;
            BaseFunction(name, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    value = reader.GetDecimal(0);
                }
            });
            return value;
        }
        protected string LoadString(string name, Dictionary<string, object> parameters)
        {
            string value = "";
            BaseFunction(name, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    value = reader.GetString(0);
                }
            });
            return value;
        }
        protected List<T> LoadList<T>(string name, Dictionary<string, object> parameters, Func<Loader, T> func)
        {
            List<T> returnedData = new List<T>();
            BaseFunction(name, parameters, (reader) =>
            {
                Loader loader = new Loader(reader);
                while (reader.Read())
                {
                    returnedData.Add(func(loader));
                }
            });
            return returnedData;
        }
        public void Dispose()
        {
            conection.Dispose();
        }
    }
}
