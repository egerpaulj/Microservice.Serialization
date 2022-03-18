//      Microservice Core Libraries for .Net C#                                                                                                                                       
//      Copyright (C) 2021  Paul Eger                                                                                                                                                                     

//      This program is free software: you can redistribute it and/or modify                                                                                                                                          
//      it under the terms of the GNU General Public License as published by                                                                                                                                          
//      the Free Software Foundation, either version 3 of the License, or                                                                                                                                             
//      (at your option) any later version.                                                                                                                                                                           

//      This program is distributed in the hope that it will be useful,                                                                                                                                               
//      but WITHOUT ANY WARRANTY; without even the implied warranty of                                                                                                                                                
//      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                                                                                                                                                 
//      GNU General Public License for more details.                                                                                                                                                                  

//      You should have received a copy of the GNU General Public License                                                                                                                                             
//      along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using Newtonsoft.Json;


namespace Microservice.Serialization
{
    /// <summary>
    /// Provides capabilities to define custom Serialization/Deserialization logic when working with JSONs.
    /// <summary/>
    public interface IJsonConverterProvider
    {
         JsonConverter[] GetJsonConverters();
         public const string DateTimeFormat = "hh:MM:dd:mm.ss.fff";
    }

    public class EmptyJsonConverterProvider : IJsonConverterProvider
    {
        JsonConverter[] IJsonConverterProvider.GetJsonConverters() => null;
    }

    public static class JsonExtensions
    {
        public static string Serialize(this IJsonConverterProvider converter, object data) => JsonConvert.SerializeObject(data, converter.GetJsonConverters());
        
        public static T Deserialize<T>(this IJsonConverterProvider converter, string valueStr) => (T) converter.DeserializeObject<T>(valueStr);

        private static object DeserializeObject<T>(this IJsonConverterProvider converter, string valueStr)
        {
            var type = typeof(T);

            if (type.IsValueType)
            {
                if (type == typeof(string))
                    return valueStr;

                if (type == typeof(bool))
                    return bool.Parse(valueStr);

                if (type == typeof(int))
                    return int.Parse(valueStr);

                if (type == typeof(double))
                    return double.Parse(valueStr);

                if (type == typeof(float))
                    return float.Parse(valueStr);

                if (type == typeof(DateTime))
                    return DateTime.Parse(valueStr.Trim('"'));
            }

            return JsonConvert.DeserializeObject(valueStr, typeof(T), converter.GetJsonConverters());
        }

        private static string SerializeObject(this IJsonConverterProvider converter, object data)
        {
            var type = data.GetType();

            if (type.IsValueType)
            {
                if (type == typeof(DateTime))
                    return ((DateTime) data).ToString(IJsonConverterProvider.DateTimeFormat);
                else
                    return data.ToString();
            }

            return JsonConvert.SerializeObject(data, converter.GetJsonConverters());
        }
    }
}