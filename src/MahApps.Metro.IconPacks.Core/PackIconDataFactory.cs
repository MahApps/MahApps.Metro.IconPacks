using System;
using System.Collections.Generic;
using MahApps.Metro.IconPacks.Utils;

namespace MahApps.Metro.IconPacks
{
    public static class PackIconDataFactory<TEnum> where TEnum : struct, Enum
    {
        public static Lazy<IDictionary<TEnum, string>> DataIndex { get; }

        static PackIconDataFactory()
        {
            DataIndex = new Lazy<IDictionary<TEnum, string>>(Create);
        }

        public static IDictionary<TEnum, string> Create()
        {
            var json = System.Reflection.Assembly.GetAssembly(typeof(TEnum))?.ReadFile("Resources.Icons.json");
            if (string.IsNullOrEmpty(json))
            {
                return new Dictionary<TEnum, string>();
            }
#if NETFRAMEWORK
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer
            {
                MaxJsonLength = json.Length
            };
            var dictionaryWithStringKey = serializer.Deserialize<Dictionary<string, string>>(json);
            var dictionary = new Dictionary<TEnum, string>(dictionaryWithStringKey.Count);

            foreach (var kvp in dictionaryWithStringKey)
            {
                if (Enum.TryParse<TEnum>(kvp.Key, out var key))
                {
                    dictionary.Add(key, kvp.Value);
                }
            }

            return dictionary;
#else
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<TEnum, string>>(json);
#endif
        }
    }
}