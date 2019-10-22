using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NaukaFiszek.Logic
{
    public class Extension
    {
        public static string EventContentText(object obj)
        {
            var sb = new StringBuilder();
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var serializedObject = ser.Serialize(obj);
            sb.AppendFormat("data: {0}\n\n", serializedObject);
            return sb.ToString();

        }
    }
}