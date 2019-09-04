using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;

namespace Conector
{
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
        public void BaseFunction(string name, Dictionary<string, string> parameters, Action<SqlDataReader> functionBody)
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


            }
            catch (SqlException e)
            {
                Debug.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
            }
        }
        public int LoadInt(string name, Dictionary<string, string> parameters)
        {
            return Convert.ToInt32(LoadDecimal(name, parameters));
        }
        public decimal LoadDecimal(string name, Dictionary<string, string> parameters)
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
        public List<T> LoadList<T>(string name, Dictionary<string, string> parameters, Func<Loader, T> func)
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
