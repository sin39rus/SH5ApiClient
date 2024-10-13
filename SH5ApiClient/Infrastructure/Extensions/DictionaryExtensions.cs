using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Infrastructure.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static string GetValueOrDefault(this Dictionary<string, string> dictionary, string key)
        {
            if(!dictionary.ContainsKey(key))
                return null;
            return dictionary[key];
        }
    }
}
