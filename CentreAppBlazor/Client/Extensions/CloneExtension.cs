using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Extensions
{
    public static class CloneExtension
    {
        public static T Clone<T>(T item)
        {
            var newPerson = Activator.CreateInstance<T>();
            var fields = newPerson.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var value = field.GetValue(item);
                field.SetValue(newPerson, value);
            }
            return newPerson;
        }
    }
}
