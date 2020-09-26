using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CentreAppBlazor.Shared.Attributes
{
   public static class GetEpplusDisplay
    {
        public static Dictionary<string, string> Get(Type type)
        {
            Dictionary<string, string> _dict = new Dictionary<string, string>();

            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is EpplusIgnore) continue;
                    EpplusDisplay authAttr = attr as EpplusDisplay;
                    if (authAttr != null)
                    {
                        string propName = prop.Name;
                        string auth = authAttr.Name;

                        _dict.Add(propName, auth);
                    }
                    else
                    {
                        string propName = prop.Name;
                        string auth = prop.Name;

                        _dict.Add(propName, auth);
                    }
                }
            }

            return _dict;
        }
    }
}
