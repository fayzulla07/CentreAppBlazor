using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Extensions
{
    public static class ReflectionExten
    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static T GetNewObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
