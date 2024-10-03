using System;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Catalog.Shared.Extensions
{
	public static class Helpers
	{
        public static string ToDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static T Deserialize<T>(this string jsonString) where T : new()
        {
            return !string.IsNullOrWhiteSpace(jsonString) ? JsonConvert.DeserializeObject<T>(jsonString) : new T();
        }

        public static string Serialize(this object @object)
        {
            return @object != null ? JsonConvert.SerializeObject(@object) : string.Empty;
        }
    }
}

